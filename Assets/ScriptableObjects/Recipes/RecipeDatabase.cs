using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "DreamBakery/Recipe Database")]
public class RecipeDatabase : ScriptableObject
{
    public List<Recipe> recipes = new List<Recipe>();
    public DreamPastry fallbackPastry;
}


