using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public List<string> arrivalDialogue;
    public List<string> waitingDialogue;
    public List<string> successDialogue;
    public List<string> failedDialogue;

    [TextArea(3, 10)]
    public string backstory;
    public CustomerProfile profile;

    public enum Mood { Happy, Neutral, Sad, Angrey, Frightened };
    public Mood customerMood;
    public List<DreamTag> dreamTags = new List<DreamTag>();
    public string desiredFoodName;
    public DreamPastry desiredPastry;


    public Sprite baseSprite;
    public Sprite dreamAcomplishedSprite;
    public Sprite impatientSprite;
    public Image spriteImage;

    public bool IsShowing { get; private set; } = false;

    public Vector3 waitingRoomPosition;
    public float waitingTimer = 0f;
    public bool hasLeft = false;

    public int timesVisited = 0;
    //public CustomerManager customerManagerReference;

    private DialogueUI dialogueUI;

    private void Awake()
    {
        dialogueUI = FindFirstObjectByType<DialogueUI>();
    }
    public void SetupFromProfile(CustomerProfile data)
    {
        profile = data;

        baseSprite = data.baseSprite;
        dreamAcomplishedSprite = data.happySprite;
        impatientSprite = data.angrySprite;

        arrivalDialogue = new List<string>(data.arrivalDialogue);
        waitingDialogue = new List<string>(data.waitingDialogue);
        successDialogue = new List<string>(data.successDialogue);
        failedDialogue = new List<string>(data.failedDialogue);
        backstory = data.backstory;


        spriteImage.sprite = baseSprite;
        customerMood = Mood.Neutral;

        if (spriteImage.sprite != null)
        {
            float aspectRatio = spriteImage.sprite.rect.width / spriteImage.sprite.rect.height;
            RectTransform rt = spriteImage.GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(rt.sizeDelta.y * aspectRatio, rt.sizeDelta.y);
        }
    }
    public void AssignRandomPastry(RecipeDatabase recipeDB)
    {
        if (recipeDB == null || recipeDB.recipes == null || recipeDB.recipes.Count == 0)
        {
            Debug.LogError("🚨 No recipes in database to assign!");
            return;
        }

        spriteImage.sprite = baseSprite;
        customerMood = Mood.Neutral;

        int index = Random.Range(0, recipeDB.recipes.Count);
        Recipe chosenRecipe = recipeDB.recipes[index];

        desiredPastry = chosenRecipe.pastryAsset;
        desiredFoodName = desiredPastry.GetPastryName();
        dreamTags = new List<DreamTag>(desiredPastry.dreamTags);

        // 🟢 Auto-generate order line BEFORE using it
        string tagNames = string.Join(", ", dreamTags.ConvertAll(t => t.name));
        string line = $"Hi! I'd like a {desiredFoodName} with: {tagNames}.";
        arrivalDialogue = new List<string> { line };

        if (dialogueUI == null)
            dialogueUI = FindFirstObjectByType<DialogueUI>();

        if (dialogueUI != null)
        {
            dialogueUI.StartDialogue(arrivalDialogue);
        }

        Debug.Log($"🧁 Customer wants: {desiredFoodName} with tags: {tagNames}");
    }
    public void BeginWaitingDialogue()
    {
        StartCoroutine(WaitingDialogueLoop());
    }
    public void ReceivePastryResult(bool wasSuccessful)
    {
        StopAllCoroutines();
        hasLeft = true;

        if (wasSuccessful)
        {
            customerMood = Mood.Happy;
            spriteImage.sprite = dreamAcomplishedSprite;

            if (successDialogue != null && successDialogue.Count > 0)
                dialogueUI.StartDialogue(successDialogue);
        }
        else
        {
            customerMood = Mood.Sad;
            spriteImage.sprite = impatientSprite;

            if (failedDialogue != null && failedDialogue.Count > 0)
                dialogueUI.StartDialogue(failedDialogue);
        }

        StartCoroutine(WaitForDialogueThenLeave());
    }
    private IEnumerator WaitForDialogueThenLeave()
    {
        // Wait until dialogue UI closes
        while (FindFirstObjectByType<DialogueUI>().IsShowing)
        {
            yield return null;
        }

        yield return new WaitForSeconds(3f);
        Debug.Log("👋 Customer is leaving now.");
        Destroy(gameObject);
    }
    private IEnumerator LeaveAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("👋 Customer is leaving now.");
        Destroy(gameObject);
    }
    public string GenerateHintLine()
    {
        if (dreamTags.Count == 0) return "Hmm... I crave something...";

        DreamTag tag = dreamTags[Random.Range(0, dreamTags.Count)];
        if (tag.associatedPhrases.Count > 0)
        {
            return tag.associatedPhrases[Random.Range(0, tag.associatedPhrases.Count)];
        }

        return $"...Something with {tag.name.ToLower()} sounds nice.";
    }
    private IEnumerator WaitingDialogueLoop()
    {
        while (!hasLeft)
        {
            yield return new WaitForSeconds(Random.Range(10f, 15f));
            if (waitingDialogue != null && waitingDialogue.Count > 0)
            {
                dialogueUI.StartDialogue(new List<string> { waitingDialogue[Random.Range(0, waitingDialogue.Count)] });
            }
        }
    }

}
