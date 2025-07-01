using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

[CreateAssetMenu(menuName = "DreamBakery/Dream Pastry")]
public class DreamPastry : ScriptableObject
{
    public List<Ingredient> ingredients;
    public List<DreamTag> dreamTags;
    public Sprite plateSprite;

    public static DreamPastry Create(List<Ingredient> selectedIngredients)
    {
        DreamPastry pastry = CreateInstance<DreamPastry>();
        pastry.ingredients = selectedIngredients;
        pastry.dreamTags = new List<DreamTag>();

        foreach(var ingredient in selectedIngredients)
        {
            if (ingredient.tags.Count > 0)
            {
                DreamTag chosenTag = ingredient.tags[Random.Range(0, ingredient.tags.Count)];
                if (!pastry.dreamTags.Contains(chosenTag))
                    pastry.dreamTags.Add(chosenTag);
            }
        }
        pastry.plateSprite = selectedIngredients[0].plateSprite;
        return pastry;
    }
    public string GetPastryName()
    {
        if (ingredients == null || ingredients.Count == 0)
            return "Oopsiecrust";

        return string.Join("-", ingredients.Select(i => i?.name ?? "??"));
    }
}
