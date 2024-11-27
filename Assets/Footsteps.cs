using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Footsteps : MonoBehaviour
{
    [SerializeField] private AudioClip[] audios;
    private bool isPlaying = false;
    private AudioSource audioSource;

    private void Start()
    {
        this.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
    }

    public bool IsPlaying()
    {
        return isPlaying;
    }
    public void Play()
    {
        isPlaying = true;
        StartCoroutine(nameof(PlayCoroutine));
    }
    public IEnumerator PlayCoroutine()
    {
            int randomVal = Random.Range(0, audios.Length);
            AudioClip audioClip = audios[randomVal];
            audioSource.clip = audioClip;
            audioSource.Play();
            
            yield return new WaitUntil(() => audioSource.isPlaying == false);

            if (isPlaying)
            {
                StartCoroutine(nameof(PlayCoroutine));
            }

    }

    public void Stop()
    {
        isPlaying = false;
    }
}
