using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayEvent : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundClip;
    public GameObject nextSpot;
    public GameObject[] objectsToDestroy;

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
