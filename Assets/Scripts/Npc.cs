using System;
using System.Collections;
using System.Collections.Generic;
using Descant.Components;
using Descant.Runtime;
using UnityEngine;

public enum Location
{
    City, 
    Town,
    Farm
}

public enum Status
{
    Single,
    Married,
    Divorced,
    Widowed
}
public class Npc : MonoBehaviour
{
    private DescantDialogueTrigger dialogueTrigger;
    private DescantDialogueUI dialogueUI;

    [SerializeField] private string name;
    [SerializeField] private int age;
    [SerializeField] private string occupation;
    [SerializeField] private string newName;
    [SerializeField] private int roomNumber;
    [SerializeField] private Location location;
    [SerializeField] private Status status;
    [SerializeField] private Sprite image;
    [SerializeField] private DescantActor descantActor;

    

    public string GetNewName()
    {
        return newName;
    }

    public string GetName()
    {
        return name;
    }

    public int GetRoomNumber()
    {
        return roomNumber;
    }

    public Sprite GetImage()
    {
        return image;}

    public string GetSummary()
    {
        return "Age " + age + ", works as a " + occupation.ToLower() + " in a " + location.ToString().ToLower() + ". They are " + status.ToString().ToLower() + ".";
    }
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
            dialogueUI.GetComponent<Canvas>().enabled = true;
        }
    }

    private void ConvoEnded()
    {
        Debug.Log("convo ended!");
        Debug.Log(descantActor.Stat.ToString());
        OnEnd?.Invoke();
        dialogueUI.GetComponent<Canvas>().enabled = false;
        
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
