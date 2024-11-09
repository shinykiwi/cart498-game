using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Letter : MonoBehaviour
{
    [SerializeField] private bool signatureRequired = false;
    [SerializeField] private string sender = "Joe Non";
    [SerializeField] private string recipient = "Joe Non";
    [SerializeField] private Sprite sprite;
     
    private static int id = 1;
    private int letterID = 0;
    private bool pickedUp = false;
    private bool delivered = false;

    public Letter()
    {
        id++;
        letterID = id;
    }

    public int GetID()
    {
        return letterID;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public string GetSender()
    {
        return sender;
    }

    public string GetRecipient()
    {
        return recipient;
    }

    public bool IsSignatureRequired()
    {
        return signatureRequired;
    }

    public override string ToString()
    {
        return "Letter to " + recipient;
    }
    
}
