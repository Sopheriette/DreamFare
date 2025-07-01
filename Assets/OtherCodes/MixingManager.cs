using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;


public class MixingManager : MonoBehaviour
{
    public List<Ingredient> currentIngredients = new List<Ingredient>();
    public List<Transform> ingredientSlots = new List<Transform>();
    private List<GameObject> currentIcons = new List<GameObject>();
    public GameObject ingredientIconPrefab;

    public RecipeDatabase recipeDatabase;
    public GameObject pastryResultPrefab;
    public Transform resultSpawnArea;
    private GameObject currentResultPastry;

    public GameObject mixingArea;
    public GameObject ovenDoor;
    public Button bakeButton;

    public Button mixButton;
    public Button clearButton;

    private void Start()
    {
        mixButton.onClick.AddListener(MixIngredients);
        clearButton.onClick.AddListener(ClearBowl);
    }

    public bool TryAddIngredient(Ingredient ingredient)
    {
        if (currentIngredients.Count >= 4 || currentResultPastry != null) return false;

        currentIngredients.Add(ingredient);
        DisplayIngredient(ingredient);
        return true;
    }

    void DisplayIngredient(Ingredient ingredient)
    {
        if (currentIngredients.Count > ingredientSlots.Count) return;

        int slotIndex = currentIngredients.Count - 1;
        Transform slot = ingredientSlots[slotIndex];

        GameObject icon = Instantiate(ingredientIconPrefab, slot);
        icon.transform.localPosition = Vector3.zero;
        icon.GetComponent<Image>().sprite = ingredient.icon;

        currentIcons.Add(icon);
    }

    void MixIngredients()
    {
        if (currentResultPastry != null)
        {
            Debug.Log("⚠️ You must place the previous pastry before baking again!");
            return;
        }

        if (currentIngredients.Count == 0)
        {
            Debug.Log("❌ No ingredients in the bowl.");
            return;
        }

        DreamPastry result = MatchRecipe(currentIngredients, recipeDatabase);
        SpawnResultPastry(result);
        ClearBowl(); // Clear ingredients after baking
    }

    void SpawnResultPastry(DreamPastry pastry)
    {
        currentResultPastry = Instantiate(pastryResultPrefab, resultSpawnArea);
        currentResultPastry.GetComponent<PastryDisplay>().pastryData = pastry;
        Debug.Log("🍞 Pastry baked: " + pastry.GetPastryName());
    }

    void ClearBowl()
    {
        currentIngredients.Clear();

        foreach (var icon in currentIcons)
        {
            Destroy(icon);
        }

        currentIcons.Clear();
    }
    public void NotifyResultPlaced()
    {
        currentResultPastry = null;
        Debug.Log(" Pastry has been plated. Ready to bake again.");
    }

    DreamPastry MatchRecipe(List<Ingredient> input, RecipeDatabase db)
    {
        foreach (var recipe in db.recipes)
        {
            if (IsIngredientMatch(recipe.requiredIngredients, input))
                return recipe.pastryAsset;
        }

        Debug.Log("⚠️ No matching recipe found. Using fallback Oopsiecrust.");
        return db.fallbackPastry;
    }
    bool IsIngredientMatch(List<Ingredient> a, List<Ingredient> b)
    {
        if (a.Count != b.Count) return false;
        var setA = new HashSet<Ingredient>(a);
        var setB = new HashSet<Ingredient>(b);
        return setA.SetEquals(setB);
    }

}
