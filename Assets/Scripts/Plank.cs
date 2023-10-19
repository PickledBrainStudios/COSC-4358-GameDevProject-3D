using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Plank : MonoBehaviour, IInteractable
{
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

    private AudioSource playerSFX;

    private void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager02").GetComponent<LevelManager02>();
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
        if (levelManager.hasCrowbar)
        {
            playerSFX.Stop();
            levelManager.PlankRemoved();
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