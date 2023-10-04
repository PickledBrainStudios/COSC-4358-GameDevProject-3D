using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Door : MonoBehaviour, IInteractable
{
    public string nextScene;
    public string nextSpawnName;
    
    public string key = "key_name";
    public AudioClip openClip;
    
    public AudioClip unlockClip;
    public bool locked = false;
    public string lockedDialogue;
    public AudioClip lockedClip;
    public float dialogueTimer;
    private float timer;
    private bool informPlayer = false;
    private TextMeshProUGUI dialogueText;
    private AudioSource audioSource;
    private GameObject player;
    private PlayerManager playerManager;

    private RawImage fade;
    private bool fadeOut = false;
    public float fadeIncrement = 1f;
    private float realValue = 0f;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");//find player
        playerManager = player.GetComponent<PlayerManager>();//find playerManager Script
        if (locked) {
            dialogueText = GameObject.FindWithTag("UI_DialogueBox").GetComponent<TextMeshProUGUI>();
        }
        audioSource = GameObject.FindWithTag("Player_AudioSource").GetComponent<AudioSource>();
        fade = GameObject.FindWithTag("UI_Fade").GetComponent<RawImage>();
    }
    //On interact
    public void Interact() {
        if (locked)
        {
            //if (playerManager.keyInventory.Contains(key)) //check to see if the player has the key in their inventory
            //{
                //playerManager.spawnName = nextSpawnName;//modify their spawn location, so that when we load the next scene they will spawn in the correct spot
                //audioSource.PlayOneShot(unlockClip);
                //SceneManager.LoadScene(nextScene);//load next scene
            //}
            audioSource.PlayOneShot(lockedClip);
            dialogueText.text = "";
            dialogueText.text = lockedDialogue;
            informPlayer = true;
            timer = dialogueTimer;

            //tell the player the door is locked but don't lock them in place
        }
        else 
        {
            playerManager.spawnName = nextSpawnName;//modify their spawn location, so that when we load the next scene they will spawn in the correct spot
            fadeOut = true;
        }
    }

    private void Update() {
        if (informPlayer) 
        {
            timer -= Time.deltaTime;
            if (timer <= 0) {
                dialogueText.text = "";
                informPlayer = false;
            }
        }
        if (fadeOut)
        {
            if (realValue < 1f)
            {
                Debug.Log(realValue);
                realValue += Time.deltaTime * fadeIncrement;
                fade.color = new Color(0f, 0f, 0f, realValue);
            }
            else 
            {
                fadeOut = false;
                audioSource.PlayOneShot(openClip);
                SceneManager.LoadScene(nextScene);//load next scene
            }
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
