using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Dialogue dialogue;
    DialogueManager dManager;

    public void TriggerDialogue()
    {
        dManager = FindObjectOfType<DialogueManager>();
        dManager.StartDialogue(dialogue);
    }
}
