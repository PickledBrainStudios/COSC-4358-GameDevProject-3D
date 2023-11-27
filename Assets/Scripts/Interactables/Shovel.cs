using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour, IInteractable
{
    public LevelManager03 levelManager;
    public AudioClip audioClip;
    private AudioSource audioSource;
    // Start is called before the first frame update

    private void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("Player_AudioSource_02").GetComponent<AudioSource>();
    }

    public void Interact() {
        levelManager.PickUpShovel();
        audioSource.PlayOneShot(audioClip);
        Destroy(gameObject);
    }
}
