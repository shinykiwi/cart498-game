using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Letter : MonoBehaviour
{
    [SerializeField] private bool signatureRequired = false;
    [SerializeField] private string sender = null;
    [SerializeField] private string recipient = null;
     
    private bool pickedUp = false;
    private bool delivered = false;

    public override string ToString()
    {
        if (recipient == null && sender != null)
        {
            return "Letter from " + sender;
            
        }
        return "Letter to " + recipient;
    }
    
}
