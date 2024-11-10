using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    [Tooltip("The scene that will be loaded into.")]
    [SerializeField] private SceneAsset destination;
    
    [Tooltip("Sound effect that plays when going inside a building or room.")]
    [SerializeField] private AudioSource openDoor;
    
    [Tooltip("Sound effect that plays when leaving a building.")]
    [SerializeField] private AudioSource closeDoor;
    
    [Tooltip("Canvas object that facilitates fading to black.")]
    [SerializeField] private GameObject fadeCanvas;
    
    /// <summary>
    /// The fade image, used for controlling the colour and opacity.
    /// </summary>
    private Image image;
    
    /// <summary>
    /// The duration of the fade animation.
    /// </summary>
    private readonly float length = 0.5f;
    
    /// <summary>
    /// Whether the animation is currently fading. 
    /// </summary>
    private bool isFading = false;
    
    /// <summary>
    /// Called to initiate opening the door process.
    /// </summary>
    public void Open()
    {
        // Creates new canvas
        fadeCanvas = Instantiate(fadeCanvas, transform);
        image = fadeCanvas.GetComponentInChildren<Image>();
        
        // If not already fading and the canvas exists
        if (!isFading && image)
        {
            // Fade
            isFading = true;
            image.DOFade(1.0f, length).OnComplete(FadeComplete);
        }
    }

    /// <summary>
    /// Called when the Dotween fade animation is finished. Initiates playing the sound effects.
    /// </summary>
    private void FadeComplete()
    {
        isFading = false;

        StartCoroutine(PlaySoundEffects());
    }

    /// <summary>
    /// Whether the door is currently being used, must have had an Open() already called for this to be true.
    /// </summary>
    /// <returns>bool</returns>
    public bool InUse()
    {
        return image;
    }

    /// <summary>
    /// Decides which sound effect to play, based on the destination name.
    /// If we are going outside, plays the door closing sound effect.
    /// Otherwise that would be we are going inside, and plays the door opening sound effect.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator PlaySoundEffects()
    {
        // If we are going outside
        if (destination.name == "Overworld")
        {
            // Play the sound effect if not already playing
            if (!closeDoor.isPlaying)
            {
                closeDoor.Play();
                // Wait until the sound effect has finished
                while (closeDoor.isPlaying)
                {
                    yield return null;
                }
            }
        }
        
        // We must be going inside
        else
        {
            // Play the sound effect if not already playing
            if (!openDoor.isPlaying)
            {
                openDoor.Play();
                // Wait until the sound effect has finished
                while (openDoor.isPlaying)
                {
                    yield return null;
                }
            }
                
        }
        LoadScene();
    }

    /// <summary>
    /// Loads the specified scene held in the destination variable.
    /// </summary>
    private void LoadScene()
    {
        SceneManager.LoadScene(destination.name, LoadSceneMode.Single);
        
    }
    
    /// <summary>
    /// Returns the destination name, identifying where the door leads to.
    /// </summary>
    /// <returns>string</returns>
    public override string ToString()
    {
        return destination.name;
    }
    
}
