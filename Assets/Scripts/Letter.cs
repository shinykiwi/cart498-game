using UnityEngine;

/// <summary>
/// A class representing the deliverable letters. 
/// </summary>
public class Letter : MonoBehaviour
{
    [Tooltip("Can the mail be dropped off at a residence without handing it to someone?")]
    [SerializeField] private bool signatureRequired = false;
    
    [Tooltip("The person who sent the mail, first and last name. Leave as null for anonymous sender.")]
    [SerializeField] private string sender = null;

    [Tooltip("The person who is meant to receive the mail, first and last name. Leave as null for unknown.")]
    [SerializeField] private string recipient = null;
    
    [Tooltip("The icon associated with this letter.")]
    [SerializeField] private Sprite sprite;
     
    // ID counter shared across all letters
    private static int id = 1;
    
    /// <summary>
    /// Unique ID for the letter.
    /// </summary>
    private int letterID = 0;
    
    /// <summary>
    /// Indicates whether the letter has been picked up yet.
    /// </summary>
    private bool pickedUp = false;
    
    /// <summary>
    /// Indicates whether the letter has been delivered yet.
    /// </summary>
    private bool delivered = false;

    public Letter()
    {
        id++;
        letterID = id;
    }

    /// <summary>
    /// Gets the unique ID of the letters
    /// </summary>
    /// <returns>int</returns>
    public int GetID()
    {
        return letterID;
    }

    /// <summary>
    /// Gets the sprite image associated with the letter.
    /// </summary>
    /// <returns>Sprite</returns>
    public Sprite GetSprite()
    {
        return sprite;
    }

    /// <summary>
    /// Gets the person who sent the message.
    /// </summary>
    /// <returns>string</returns>
    public string GetSender()
    {
        return sender;
    }

    /// <summary>
    /// Gets the person who is meant to receive the message.
    /// </summary>
    /// <returns>string</returns>
    public string GetRecipient()
    {
        return recipient;
    }

    /// <summary>
    /// Gets whether the letter must be hand delivered or not.
    /// </summary>
    /// <returns>bool</returns>
    public bool IsSignatureRequired()
    {
        return signatureRequired;
    }
    
    /// <summary>
    /// Gets string representation in the format of "Letter to [recipient]".
    /// Ex. Letter to Jocelyn
    /// </summary>
    /// <returns>string</returns>
    public override string ToString()
    {
        return "Letter to " + recipient;
    }
}