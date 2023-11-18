using UnityEngine;

public class TriggerSoundTest : MonoBehaviour
{
    public AudioSource audioSource;
    public bool playOnce = true;
    private bool hasPlayed = false;

    private void Start()
    {
        audioSource.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {           
        if (hasPlayed && playOnce) return;

        audioSource.PlayOneShot(audioSource.clip);
        hasPlayed = true;
    }
}