using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour, IInteractable
{
    public LevelManager03 levelManager;
    public AudioClip audioClip;
    private AudioSource audioSource;
    private GameObject[] graveTwinkles;
    // Start is called before the first frame update

    private void Start()
    {
           
       graveTwinkles = GameObject.FindGameObjectsWithTag("GT");
        foreach (GameObject obj in graveTwinkles)
        {
            obj.SetActive(false);
            Debug.Log(obj);
        }

        audioSource = GameObject.FindGameObjectWithTag("Player_AudioSource_02").GetComponent<AudioSource>();
    }

    public void Interact() {
        levelManager.PickUpShovel();
        audioSource.PlayOneShot(audioClip);
        foreach (GameObject obj in graveTwinkles)
        {
            obj.SetActive(true);
            Debug.Log(obj);
        }
        Destroy(gameObject);
    }
}
