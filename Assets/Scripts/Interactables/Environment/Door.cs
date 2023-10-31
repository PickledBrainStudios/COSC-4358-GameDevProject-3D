using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    public string nextScene;
    public string nextSpawnName;
    public AudioClip openClip;
    public float fadeSpeed = 1f;

    public bool locked = false;
    //public string key = "key_name";
    public string lockedDialogue;
    public float dialogueTimer;
    public AudioClip lockedClip;
    public AudioClip unlockClip;

    public string destroyEnemyTag = "Enemy01";
    public GameObject[] toDestory;
    
    private float timer;
    private bool informPlayer = false;
    private bool fadeOut = false;
    private float realValue = 0f;
    private RawImage fade;
    private TextMeshProUGUI dialogueText;
    private AudioSource audioSource;
    private GameObject player;
    private PlayerManager playerManager;
    

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
        try
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag(destroyEnemyTag))
            {
                Destroy(obj);
            }
        }
        catch { }
        try
        {
            foreach (GameObject obj in toDestory)
            {
                Destroy(obj);
            }
        }
        catch { }

        if (locked)
        {
            //if (playerManager.keyInventory.Contains(key)) //check to see if the player has the key in their inventory
            //{
            //playerManager.spawnName = nextSpawnName;//modify their spawn location, so that when we load the next scene they will spawn in the correct spot
            //audioSource.PlayOneShot(unlockClip);
            //SceneManager.LoadScene(nextScene);//load next scene
            //}
            try {
                audioSource.Stop();
                audioSource.PlayOneShot(lockedClip); 
            }
            catch { }
            dialogueText.text = "";
            dialogueText.text = lockedDialogue;
            informPlayer = true;
            timer = dialogueTimer;

            //tell the player the door is locked but don't lock them in place
        }
        else 
        {
            playerManager.DeactivateControl();
            audioSource.PlayOneShot(openClip);
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
                //Debug.Log(realValue);
                realValue += Time.deltaTime * fadeSpeed;
                fade.color = new Color(0f, 0f, 0f, realValue);
            }
            else 
            {
                fadeOut = false;
                //audioSource.PlayOneShot(openClip);
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
