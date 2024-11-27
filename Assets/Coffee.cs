using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : MonoBehaviour
{
    private bool isPickedUp = false;
    private bool canBePickedUp = true;

    public bool CanBePickedUp()
    {
        return canBePickedUp;
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        isPickedUp = true;
        canBePickedUp = false;
    }

    public void Show()
    {
        gameObject.SetActive(true);
        isPickedUp = false;
    }

    public override string ToString()
    {
        return "Coffee";
    }
}
