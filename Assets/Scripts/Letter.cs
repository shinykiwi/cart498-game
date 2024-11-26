using UnityEngine;

/// <summary>
/// A class representing the deliverable letters. 
/// </summary>
public class Letter : MonoBehaviour
{
    [Tooltip("The NPC associated with this letter.")]
    [SerializeField] private Npc npc;
     
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

    public Npc GetNpc()
    {
        return npc;
    }
    
    /// <summary>
    /// Gets string representation in the format of "Letter to [recipient]".
    /// Ex. Letter to Jocelyn
    /// </summary>
    /// <returns>string</returns>
    public override string ToString()
    {
        if (npc)
        {
            return npc.GetNewName();
        }
        
        return "File";
    }
}