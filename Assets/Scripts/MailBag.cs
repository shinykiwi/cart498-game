using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBag
{
    private static int letterCapacity = 3;
    private int letterCount = 0;
    private Letter[] letters;
    
    public MailBag()
    {
        letters = new Letter[letterCapacity];
        
    }
    
    // Adds a letter to the next available slot in the array
    public void AddLetter(Letter letter)
    {
        // If there are more letters that the player can hold
        if (letterCount < letterCapacity)
        {
            letters[letterCount] = letter;
            letter.gameObject.GetComponent<MeshRenderer>().enabled = false; // make letter invisible
        }
        else
        {
            Debug.Log("the player cannot hold any more letters!");
        }
    }

    // Tries to remove the letter based on ID, returns true if it can, else returns false
    public bool RemoveLetter(Letter letter)
    {
        for (int i = 0; i < letterCapacity; i++)
        {
            if (letter.GetID() == letters[i].GetID())
            {
                return true;
            }
        }

        return false;
    }

    public override string ToString()
    {

        return letters[0].ToString();
    }
}
