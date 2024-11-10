using Descant.Components;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MailBag : MonoBehaviour
{
    #region Serialized Fields
    [Tooltip("The title text used for displaying the title of the letter.")]
    [SerializeField] private TextMeshProUGUI title;
    
    [Tooltip("The text used for displaying the body of the letter.")]
    [SerializeField] private TextMeshProUGUI paragraph;
    
    [Tooltip("The descant actor of the player, to change letterCapacity value.")]
    [SerializeField] private DescantActor descantActor;
    #endregion

    #region Variables
    /// <summary>
    /// The amount of letters that can be held in the inventory.
    /// This is decided at runtime based on the amount of cells in the grid of the UI inventory.
    /// </summary>
    private int letterCapacity;
    
    /// <summary>
    /// The current amount of letters in the inventory at any given moment.
    /// </summary>
    private int letterCount = 0;
    
    /// <summary>
    /// The array that holds all the Letter objects currently in inventory.
    /// </summary>
    private Letter[] letters;

    /// <summary>
    /// The canvas of the inventory UI.
    /// </summary>
    private Canvas ui;
    
    /// <summary>
    /// The array that holds all UI Slot objects currently in the grid.
    /// </summary>
    private Slot[] slots;
    
    /// <summary>
    /// The post processing volume to be activated while in the inventory menu.
    /// </summary>
    private Volume post;

    /// <summary>
    /// The currently selected slot that is displaying the contents of the letter.
    /// </summary>
    private Slot activeSlot;
    #endregion

    #region Getters & Setters
    public int GetLetterCapacity()
    {
        return letterCapacity;
    }

    public int GetLetterCount()
    {
        return letterCount;
    }
    #endregion
    
    private void Start()
    {
        ui = GetComponentInChildren<Canvas>();
        slots = ui.GetComponentsInChildren<Slot>(); // Gets all of the slots found
        post = GetComponent<Volume>(); 
        
        letterCapacity = slots.Length;
        letters = new Letter[letterCapacity];
        
        // Hiding the UI to begin with
        Hide();
    }

    /// <summary>
    /// Adds the specified letter to the inventory.
    /// </summary>
    /// <param name="letter"></param>
    public void AddLetter(Letter letter)
    {
        // If there are more letters that the player can hold, add letter
        if (letterCount < letterCapacity)
        {
            letters[letterCount] = letter; // add the letter
            letter.gameObject.GetComponent<MeshRenderer>().enabled = false; // make letter invisible
            letter.gameObject.GetComponent<Collider>().enabled = false;
            slots[letterCount].Fill(letter.GetSprite()); // show image in slot
            letterCount++; // increase counter
            
            UpdateDescantActor();
        }
        else
        {
            Debug.Log("Error: The player cannot hold any more letters!");
        }
    }

    /// <summary>
    /// Tries to remove the letter based on ID, returns true if it can, else returns false.
    /// </summary>
    /// <param name="letter"></param>
    /// <returns>bool</returns>
    public bool RemoveLetter(Letter letter)
    {
        int letterID = letter.GetID();
        
        // Iterate through all of the letters in the inventory
        for (int i = 0; i < letterCount; i++)
        {
            // If an ID matches
            if (letterID == letters[i].GetID())
            {
                letters[i] = null; // deletes the letter in that slot
                slots[i].Clear(); // removes the letter visually
                return true;
            }
        }

        return false; // if the letter could not be found
    }

    public void GiveLetter()
    {
        Debug.Log("giving the letter!");
    }

    private void UpdateDescantActor()
    {
        descantActor.Statistics["letterCapacity"] = letterCapacity;
    }

    /// <summary>
    /// Hides the UI and the post processing.
    /// </summary>
    public void Hide()
    {
        ui.enabled = false;
        post.enabled = false;
    }

    /// <summary>
    /// Shows the UI and the post processing.
    /// </summary>
    public void Show()
    {
        ui.enabled = true;
        post.enabled = true;
    }

    /// <summary>
    /// Toggles between showing and hiding the UI and the post processing.
    /// </summary>
    public void Toggle()
    {
        ui.enabled = !ui.enabled;
        post.enabled = !post.enabled;
    }

    public bool IsFull()
    {
        return letterCount == letterCapacity;
    }
    
    /// <summary>
    /// Returns a string representation of all the letters in the mailbox, ordered in a list.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        string l = "All letters in mailbag: \n";

        if (letterCount > 0)
        {
            foreach (var letter in letters)
            {
                l += letter + "\n";
            }
        }

        return l;
    }
}
