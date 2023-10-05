using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OnCollideSpeak : MonoBehaviour
{
    public string[] dialogueLines;

    public float timer = 3f;
    private float timerT;

    private TextMeshProUGUI dialogueText;

    private int currentLine = 0;
    private GameObject player;
    private PlayerManager playerManager;
    private bool activeDialogue = false;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");//find player
        playerManager = player.GetComponent<PlayerManager>();//find playerManager Script
        dialogueText = GameObject.FindWithTag("UI_DialogueBox").GetComponent<TextMeshProUGUI>();
        timerT = timer;
    }
    private void Update()
    {
        if (currentLine < dialogueLines.Length && activeDialogue && timer > 0)
        {
            timer -= Time.deltaTime;
            dialogueText.text = dialogueLines[currentLine];
        }
        else if (timer <= 0 && currentLine < dialogueLines.Length - 1)
        {
            Debug.Log(currentLine + " " + dialogueLines.Length);
            currentLine++;
            timer = timerT;
        }
        else if (timer <= 0) {
            dialogueText.text = "";
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        activeDialogue = true;
        currentLine = 0; // Reset dialog
    }
}