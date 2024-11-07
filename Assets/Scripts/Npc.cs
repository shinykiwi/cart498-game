using System;
using System.Collections;
using System.Collections.Generic;
using Descant.Runtime;
using UnityEngine;

public class Npc : MonoBehaviour
{
    private DescantDialogueTrigger dialogueTrigger;
    private DescantDialogueUI dialogueUI;
    private bool isInConversation = false;

    [SerializeField] private string name = "Joe";

    private void Start()
    {
        dialogueTrigger = GetComponent<DescantDialogueTrigger>();
        dialogueUI = dialogueTrigger.GetComponentInChildren<DescantDialogueUI>();
        
    }

    public void TalkTo()
    {
        if (dialogueTrigger)
        {
            dialogueTrigger.Display();
            isInConversation = true;
        }
        
    }

    public bool IsInConversation()
    {
        return isInConversation;
    }

    private void OnEnable()
    {
        if (dialogueUI)
        {
            dialogueUI.OnEnd += ConvoEnded;
        }
    }

    private void OnDisable()
    {
        if (dialogueUI)
        {
            dialogueUI.OnEnd -= ConvoEnded;
        }
    }

    private void ConvoEnded()
    {
        isInConversation = false;
    }
}
