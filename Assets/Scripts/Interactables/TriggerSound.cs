using UnityEngine;

// TODO: - bool
// TODO: - timer
public class TriggerSound : MonoBehaviour
{
    public AudioClip soundClip;
    public AudioSource audioSource;

    private void Start()
    {
        //audioSource = GetComponent<AudioSource>();
       // if (audioSource == null)
      //  {
           // audioSource = gameObject.AddComponent<AudioSource>();
          //  audioSource.clip = soundClip;
       // }
    }

    private void OnTriggerEnter(Collider other)
    {
        audioSource.PlayOneShot(soundClip);
    }
}
