using UnityEngine;

public class HallwayEvent : MonoBehaviour
{
    
    public AudioClip soundClip;
    public GameObject nextSpot;
    public GameObject[] objectsToDestroy;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("Player_AudioSource").GetComponent<AudioSource>();//find player
    }

    private void OnTriggerEnter(Collider other)
    {
        audioSource.PlayOneShot(soundClip);
        if (nextSpot != null) {
            nextSpot.SetActive(true);
        }
        foreach (GameObject obj in objectsToDestroy) { Destroy(obj); }
        Destroy(gameObject);
    }
}
