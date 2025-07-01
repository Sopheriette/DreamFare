using System.Collections.Generic;
using TMPro;
using System.Collections;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    public TMP_Text dialogueText;
    public GameObject dialogueBox;
    public bool IsShowing { get; private set; } = false;

    public void StartDialogue(List<string> lines)
    {
        StopAllCoroutines(); // make sure it's fresh
        StartCoroutine(ShowDialogueSequence(lines));
    }

    private IEnumerator ShowDialogueSequence(List<string> lines)
    {
        dialogueBox.SetActive(true);
        IsShowing = true;

        foreach (string line in lines)
        {
            dialogueText.text = line;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0));
            yield return null;
        }

        dialogueBox.SetActive(false);
        dialogueText.text = "";
        IsShowing = false;
    }
}