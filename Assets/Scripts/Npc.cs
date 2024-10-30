using System;
using System.Collections;
using System.Collections.Generic;
using Descant.Runtime;
using UnityEngine;

public class Npc : MonoBehaviour
{
    private DescantDialogueTrigger dialogueTrigger;
    private bool isInConversation = false;

    [SerializeField] private string name = "Joe";

    private void Start()
    {
        dialogueTrigger = GetComponent<DescantDialogueTrigger>();
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
}
