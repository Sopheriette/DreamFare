using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(fileName = "NewDreamTag", menuName = "DreamBakery/DreamTag")]
public class DreamTag : ScriptableObject
{
    public string tagName;
    public Sprite tagIcon;
    [TextArea(2, 5)]
    public List<string> associatedPhrases;
}
