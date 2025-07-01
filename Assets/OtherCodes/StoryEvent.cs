using UnityEngine;

[System.Serializable]
public class StoryEvent
{
    public string eventID;  // e.g. "FirstCustomerServed", "OutOfIngredients"
    public bool triggered = false;
    public DialogueScript dialogue;
}

