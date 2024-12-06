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
    [Tooltip("The scene that will be loaded into. Leave null if there's no destination to go to.")] 
    
    [SerializeField] private string destinationName = "DefaultDestination";
    
    [Tooltip("Sound effect that plays when going inside a building or room.")]
    [SerializeField] private AudioSource openDoor;
    
    [Tooltip("Sound effect that plays when leaving a building.")]
    [SerializeField] private AudioSource closeDoor;
    
    
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

    private Transform playerTransform;

    private bool entered = false;
    
    /// <summary>
    /// Called to initiate opening the door process.
    /// </summary>
    public void Open(Transform t, GameObject fadeCanvas)
    {
        playerTransform = t;
        // Creates new canvas
        image = fadeCanvas.GetComponentInChildren<Image>();
        
        // If not already fading and the canvas exists
        if (!isFading && image)
        {
            FadeIn();
        }
    }

    void FadeIn()
    {
        isFading = true;
        image.DOFade(1.0f, length).OnComplete(FadeComplete);
    }

    void FadeOut()
    {
        isFading = true;
        image.DOFade(0.0f, length).OnComplete((() => { isFading = false;}));
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
    /// Decides which sound effect to play, based on the destination name.
    /// If we are going outside, plays the door closing sound effect.
    /// Otherwise that would be we are going inside, and plays the door opening sound effect.
    /// </summary>
    /// <returns>IEnumerator</returns>
    private IEnumerator PlaySoundEffects()
    {
        // If we are exiting
        if (!entered)
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
        
        TransportPlayer();
    }

    private void TransportPlayer()
    {
        
        Vector3 doorPos = transform.position;
        Vector3 newPos = new Vector3(doorPos.x + 2, 0, doorPos.z + 2);
        CharacterController controller = playerTransform.gameObject.GetComponent<CharacterController>();

        controller.enabled = false;
        
        // Must be exiting
        if (entered)
        {
            playerTransform.position = newPos;
        }
        // Must be entering
        else
        {
            newPos.z = doorPos.z - 2;
            
            playerTransform.position = newPos;
        }
       
        entered = !entered;

        controller.enabled = true;
        
        FadeOut();
    }
    
    /// <summary>
    /// Returns the destination name, identifying where the door leads to.
    /// </summary>
    /// <returns>string</returns>
    public override string ToString()
    {
        if (entered)
        {
            return "Exit";
        }
        return destinationName;
    }
    
}
