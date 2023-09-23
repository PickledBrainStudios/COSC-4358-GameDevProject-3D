using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Door : MonoBehaviour, IInteractable
{
    public string nextScene;
    public string nextSpawnName;
    public bool locked = false;
    public string key = "key_name";
    public AudioClip openClip;
    public AudioClip lockedClip;
    public AudioClip unlockClip;
    private AudioSource audioSource;
    private GameObject player;
    private PlayerManager playerManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");//find player
        playerManager = player.GetComponent<PlayerManager>();//find playerManager Script
        audioSource = GameObject.FindWithTag("Player_AudioSource").GetComponent<AudioSource>();
    }
    //On interact
    public void Interact() {
        if (locked)
        {
            if (playerManager.keyInventory.Contains(key)) //check to see if the player has the key in their inventory
            {
                playerManager.spawnName = nextSpawnName;//modify their spawn location, so that when we load the next scene they will spawn in the correct spot
                audioSource.PlayOneShot(unlockClip);
                SceneManager.LoadScene(nextScene);//load next scene
            }
            audioSource.PlayOneShot(lockedClip);
            //tell the player the door is locked but don't lock them in place
        }
        else 
        {
            playerManager.spawnName = nextSpawnName;//modify their spawn location, so that when we load the next scene they will spawn in the correct spot
            audioSource.PlayOneShot(openClip);
            SceneManager.LoadScene(nextScene);//load next scene
        }
    }

    public void lockDoor()
    {
        locked = true;
    }

    public void UnlockDoor() {
        locked = false;
    }
}
