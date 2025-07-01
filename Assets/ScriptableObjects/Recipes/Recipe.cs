using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DreamBakery/Recipe")]
public class Recipe : ScriptableObject
{
    public string pastryName;
    public List<Ingredient> requiredIngredients = new List<Ingredient>();
    public DreamPastry pastryAsset;
}
