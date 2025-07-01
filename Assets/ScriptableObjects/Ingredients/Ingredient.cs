using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(fileName = "NewIngredient", menuName = "DreamBakery/Ingredient")]
public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public Sprite icon;
    public Sprite selectedSprite;
    public Sprite plateSprite;
    public List<DreamTag> tags;
    public string description;
}
