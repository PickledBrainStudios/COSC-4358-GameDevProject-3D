using UnityEngine;
using TMPro;


public class Plank : MonoBehaviour, IInteractable
{
    public bool hasCrowbar = false;
    public GameObject[] toActivate;
    public GameObject[] toUnlock;
    public float timer = 3f;
    public string noToolDialogue = "Maybe there's a tool around here I can use to take this down.";
    public AudioClip removeSound;
    public AudioClip blockedClip;

    private LevelManager02 levelManager;
    private TextMeshProUGUI dialogueText;
    private float timerT;
    private bool activeDialogue = false;
    private bool toolCheck = false;

    private AudioSource playerSFX;

    private void Start()
    {
        dialogueText = GameObject.FindWithTag("UI_DialogueBox").GetComponent<TextMeshProUGUI>();
        playerSFX = GameObject.FindGameObjectWithTag("Player_AudioSource_02").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (activeDialogue) { 
            if (timerT > 0)
            {
                timerT -= Time.deltaTime;
                dialogueText.text = noToolDialogue;
            }
            else
            {
                dialogueText.text = "";
                activeDialogue = false;
            }
        }
    }


    public void Interact()
    {
        try 
        { 
            levelManager = GameObject.FindGameObjectWithTag("LevelManager02").GetComponent<LevelManager02>();
            toolCheck = levelManager.hasCrowbar;
        }
        catch 
        { 
            levelManager = null;
            toolCheck = false;
        }
        if (hasCrowbar || toolCheck)
        {
            playerSFX.Stop();
            try { levelManager.PlankRemoved(); }
            catch { }
            
            playerSFX.PlayOneShot(removeSound);
            foreach (GameObject obj in toUnlock)
            {
                obj.GetComponent<PhysicalDoor>().UnlockDoor();
            }
            foreach (GameObject obj in toActivate)
            {
                obj.SetActive(true);
            }
            Destroy(gameObject);
        }
        else 
        {
            playerSFX.Stop();
            playerSFX.PlayOneShot(blockedClip);
            timerT = timer;
            activeDialogue = true;
        }
    }
}