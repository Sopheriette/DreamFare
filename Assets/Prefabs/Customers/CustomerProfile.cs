using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Customer/Profile")]
public class CustomerProfile : ScriptableObject
{
    public string customerName;

    [Header("Sprites")]
    public Sprite baseSprite;
    public Sprite happySprite;
    public Sprite angrySprite;

    [Header("Dialogue")]
    public List<string> arrivalDialogue;
    public List<string> waitingDialogue;
    public List<string> successDialogue;
    public List<string> failedDialogue;

    [TextArea(3, 10)]
    public string backstory;
}