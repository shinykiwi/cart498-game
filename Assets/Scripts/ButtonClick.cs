using System;
using System.Collections;
using System.Collections.Generic;
using Descant.Runtime;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    [SerializeField] private DescantDialogueUI ui;
    private void Start()
    {
        if (ui)
        {
            ui.OnDisplay += Clicked;
        }
        else
        {
            Debug.Log("Error: Cannot do [ui.OnDisplay -= Clicked] because it does not exist.");
        }
       
    }

    public void Clicked()
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (!audio.isPlaying)
        {
            audio.Play();
        }
        
    }

    private void OnDisable()
    {
        if (ui)
        {
            ui.OnDisplay -= Clicked;
        }
        else
        {
            Debug.Log("Error: Cannot do [ui.OnDisplay -= Clicked] because it does not exist.");
        }
        
    }
}
