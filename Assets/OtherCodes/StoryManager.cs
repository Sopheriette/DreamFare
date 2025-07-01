using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance;
    public DialogueUI dialogueUI;

    public List<StoryEvent> storyEvents;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        dialogueUI = FindFirstObjectByType<DialogueUI>();
    }

    public void TriggerEvent(string eventID)
    {
        StoryEvent story = storyEvents.Find(e => e.eventID == eventID && !e.triggered);
        if (story != null)
        {
            story.triggered = true;
            dialogueUI.StartDialogue(story.dialogue.lines);
        }
    }
}