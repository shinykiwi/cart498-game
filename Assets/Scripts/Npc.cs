using System;
using System.Collections;
using System.Collections.Generic;
using Descant.Components;
using Descant.Runtime;
using UnityEngine;

public class Npc : MonoBehaviour
{
    private DescantDialogueTrigger dialogueTrigger;
    private DescantDialogueUI dialogueUI;

    [SerializeField] private string name;
    [SerializeField] private string newName;
    [SerializeField] private int roomNumber;

    [SerializeField] private DescantActor descantActor;
    
    public event Action OnEnd;
    private void Awake()
    {
        dialogueTrigger = GetComponent<DescantDialogueTrigger>();
        dialogueUI = dialogueTrigger.GetComponentInChildren<DescantDialogueUI>();
        descantActor.Stat.AddEntry("progression", 0);
    }

    public void TalkTo()
    {
        if (dialogueTrigger)
        {
            dialogueTrigger.Display();
            dialogueUI.GetComponent<Canvas>().enabled = false;
            dialogueUI.GetComponent<Canvas>().enabled = true;
        }
    }

    private void ConvoEnded()
    {
        Debug.Log("convo ended!");
        Debug.Log(descantActor.Stat.ToString());
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
