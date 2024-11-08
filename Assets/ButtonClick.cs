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
        ui.OnDisplay += Clicked;
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
        ui.OnDisplay -= Clicked;
    }
}
