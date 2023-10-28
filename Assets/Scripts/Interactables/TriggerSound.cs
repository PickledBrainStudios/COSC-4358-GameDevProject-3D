using System;
using UnityEngine;

public class TriggerSound : MonoBehaviour
{
    public AudioClip soundClip;
    public AudioSource audioSource;
    public bool playOnce = true;
    private bool played = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = soundClip;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playOnce && played) return; 
        if (audioSource.isPlaying) return;

        audioSource.Play();
        played = true;
    }
}
