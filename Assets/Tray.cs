using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tray : MonoBehaviour
{
    private static Coffee[] coffees = null;
    private bool isEmpty = true;

    [SerializeField] private Npc npc;
    private void Start()
    {
        if (coffees == null)
        {
            coffees = FindObjectsOfType<Coffee>();
        }
    }

    public bool IsEmpty()
    {
        return isEmpty;
    }

    private Coffee FindPickedUpCoffee()
    {
        foreach (Coffee coffee in coffees)
        {
            if (!coffee.CanBePickedUp())
            {
                return coffee;
            }
        }

        return null;
    }
    public void PlaceCoffee()
    {
        if (FindPickedUpCoffee() is {} coffee)
        {
            coffee.transform.SetParent(transform);
            coffee.transform.localPosition = new Vector3(4.814f, 0, 0);
            coffee.Show();
            isEmpty = false;
            
            npc.GiveCoffee();
        }
        else
        {
            Debug.Log("No coffee was found that has been picked up yet.");
        }
    }
}
