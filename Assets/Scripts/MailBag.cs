using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MailBag : MonoBehaviour
{
    private static int letterCapacity;
    private int letterCount = 0;
    private Letter[] letters;

    private Canvas ui;
    private Slot[] slots;
    private Volume post;

    private Slot activeSlot;

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI paragraph;

    private void Start()
    {
        ui = GetComponentInChildren<Canvas>();
        slots = ui.GetComponentsInChildren<Slot>();
        post = GetComponent<Volume>();
        
        letterCapacity = slots.Length;
        letters = new Letter[letterCapacity];
        Hide();
    }

    // Adds a letter to the next available slot in the array
    public void AddLetter(Letter letter)
    {
        // If there are more letters that the player can hold, add letter
        if (letterCount < letterCapacity)
        {
            letters[letterCount] = letter; // add the letter
            letter.gameObject.GetComponent<MeshRenderer>().enabled = false; // make letter invisible
            slots[letterCount].Fill(letter.GetSprite()); // show image in slot
            letterCount++; // increase counter
            
        }
        else
        {
            Debug.Log("Error: The player cannot hold any more letters!");
        }
    }

    // Tries to remove the letter based on ID, returns true if it can, else returns false
    public bool RemoveLetter(Letter letter)
    {
        int letterID = letter.GetID();
        
        for (int i = 0; i < letterCapacity; i++)
        {
            if (letterID == letters[i].GetID())
            {
                letters[i] = null;
                slots[i].Clear();
                return true;
            }
        }

        return false;
    }

    public void Hide()
    {
        ui.enabled = false;
        post.enabled = false;
    }

    public void Show()
    {
        ui.enabled = true;
        post.enabled = true;
    }

    public void Toggle()
    {
        ui.enabled = !ui.enabled;
        post.enabled = !post.enabled;
    }

    public override string ToString()
    {
        return letters[0].ToString();
    }

    void PopulateUI()
    {
        foreach (Letter letter in letters)
        {
            
        }
    }
}
