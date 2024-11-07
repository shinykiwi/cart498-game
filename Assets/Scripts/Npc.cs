using System;
using System.Collections;
using System.Collections.Generic;
using Descant.Runtime;
using UnityEngine;

public class Npc : MonoBehaviour
{
    private DescantDialogueTrigger dialogueTrigger;
    private DescantDialogueUI dialogueUI;

    [SerializeField] private string name = "Joe";
    
    public event Action OnEnd;
    private void Awake()
    {
        dialogueTrigger = GetComponent<DescantDialogueTrigger>();
        dialogueUI = dialogueTrigger.GetComponentInChildren<DescantDialogueUI>();
    }

    public void TalkTo()
    {
        if (dialogueTrigger)
        {
            dialogueTrigger.Display();
        }
    }

    private void ConvoEnded()
    {
        Debug.Log("convo ended!");
        OnEnd?.Invoke();
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

    public override string ToString()
    {
        return "Talk to " + name;
    }
}
