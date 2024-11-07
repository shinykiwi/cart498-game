using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : Interactable
{
    [SerializeField]
    private SceneAsset destination;

    [SerializeField] private AudioSource openDoor;
    [SerializeField] private AudioSource closeDoor;
    [SerializeField] private GameObject fadeCanvas;
    
    private Image image;
    private float length = 0.5f;
    private bool isFading = false;
    
    public void Open()
    {
        fadeCanvas = Instantiate(fadeCanvas, transform);
        image = fadeCanvas.GetComponentInChildren<Image>();
        
        if (!isFading && image)
        {
            isFading = true;
            image.DOFade(1.0f, length).OnComplete(FadeComplete);
        }
    }

    private void FadeIn()
    {
        Debug.Log(fadeCanvas);
        if (fadeCanvas == null)
        {
            fadeCanvas = Instantiate(fadeCanvas, transform);
        }
        
        if (image == null)
        {
            image = fadeCanvas.GetComponentInChildren<Image>();
        }
        
        image.color = Color.black;
        image.DOFade(0f, length);
    }

    private void FadeComplete()
    {
        isFading = false;

        StartCoroutine(PlaySoundEffects());
        
    }

    public bool InUse()
    {
        return image;
    }

    private IEnumerator PlaySoundEffects()
    {
        if (destination.name == "Overworld")
        {
            if (!closeDoor.isPlaying)
            {
                closeDoor.Play();
                while (closeDoor.isPlaying)
                {
                    yield return null;
                }
            }
        }
        else
        {
            if (!openDoor.isPlaying)
            {
                openDoor.Play();
                while (openDoor.isPlaying)
                {
                    yield return null;
                }
            }
                
        }
        
        SceneManager.LoadScene(destination.name, LoadSceneMode.Single);
        
    }

    public void OpenDoor()
    {
        if (!openDoor.isPlaying)
        {
            openDoor.Play();
        }
    }
    public void CloseDoor()
    {
        if (!closeDoor.isPlaying)
        {
            closeDoor.Play();
            
        }
    }
    
    public override string ToString()
    {
        return destination.name;
    }
}
