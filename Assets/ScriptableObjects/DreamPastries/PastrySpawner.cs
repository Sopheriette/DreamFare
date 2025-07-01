using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PastrySpawner : MonoBehaviour
{
    public List<Ingredient> availableIngredients;
    public GameObject pastryDisplayPrefab;
    public Transform trayParent;
    public RecipeDatabase recipeDatabase;

    public int trayCapacity = 4;
    private List<GameObject> spawnedPastries = new List<GameObject>();

    private void Start()
    {
        FillTrayWithRandomPastries();
    }

    public void FillTrayWithRandomPastries()
    {
        ClearTray();
        
        for (int i = 0; i < trayCapacity; i++)
        {
            List<Ingredient> selectedIngredients = new List<Ingredient>();
            int ingredientCount = Random.Range(2, 5);

            for (int j = 0; j < ingredientCount; j++)
            {
                Ingredient ingredient = availableIngredients[Random.Range(0, availableIngredients.Count)];
                selectedIngredients.Add(ingredient);
            }

            DreamPastry newPastry = MatchRecipe(selectedIngredients, recipeDatabase);
            SpawnPastryVisual(newPastry, i);
        }
    }
    void SpawnPastryVisual(DreamPastry pastry, int slotIndex)
    {
        Transform targetSlot = trayParent.GetChild(slotIndex);
        GameObject newPastryGO = Instantiate(pastryDisplayPrefab, targetSlot);
        newPastryGO.transform.localPosition = Vector3.zero;
        newPastryGO.transform.localScale = Vector3.one;
        newPastryGO.GetComponent<PastryDisplay>().pastryData = pastry;

        Image icon = newPastryGO.GetComponentInChildren<Image>();
        if (icon != null) icon.sprite = pastry.plateSprite;

        newPastryGO.name = $"{slotIndex}_{pastry.GetPastryName()}";

        //TODO: Add tag icons later

        spawnedPastries.Add(newPastryGO);

        Text debugText = newPastryGO.GetComponentInChildren<Text>();
        if (debugText != null)
        {
            string tagNames = string.Join(", ", pastry.dreamTags.ConvertAll(tag => tag.name));
            debugText.text = $"{pastry.GetPastryName()}\n[{tagNames}]";
        }
    }
    DreamPastry MatchRecipe(List<Ingredient> inputIngredients, RecipeDatabase recipeDB)
    {
        if (recipeDB == null)
        {
            Debug.LogError("🚨 Recipe Database is null.");
            return null;
        }

        if (recipeDB.recipes == null)
        {
            Debug.LogError("🚨 recipeDB.recipes is null.");
        }

        if (recipeDB.fallbackPastry == null)
        {
            Debug.LogError("🚨 Fallback pastry is not set.");
        }

        foreach (var recipe in recipeDB.recipes)
        {
            if (IsIngredientMatch(recipe.requiredIngredients, inputIngredients))
                return recipe.pastryAsset;
        }

        Debug.Log("⚠️ No matching recipe found. Using fallback Oopsiecrust.");
        return recipeDB.fallbackPastry;
    }

    bool IsIngredientMatch(List<Ingredient> a, List<Ingredient> b)
    {
        if (a == null || b == null) return false;
        if (a.Count != b.Count) return false;

        var aSorted = new List<Ingredient>(a.Where(i => i != null));
        var bSorted = new List<Ingredient>(b.Where(i => i != null));

        aSorted.Sort((x, y) => x.name.CompareTo(y.name));
        bSorted.Sort((x, y) => y.name.CompareTo(x.name)); // alphabetical sort

        for (int i = 0; i < aSorted.Count; i++)
        {
            if (aSorted[i] != bSorted[i])
                return false;
        }
        return true;
    }
    void ClearTray()
    {
        foreach (var pastryGO in spawnedPastries)
        {
            Destroy(pastryGO);
        }
        spawnedPastries.Clear();
    }
}
