using UnityEngine;
using TMPro;


public class OnCollideSpeak : MonoBehaviour
{
    public string[] dialogueLines;
    public float timer = 3f;
    public bool destroyOnComplete = false;

    private float timerT;

    private TextMeshProUGUI dialogueText;

    private int currentLine = 0;
    private bool activeDialogue = false;
    

    private void Start()
    {
        dialogueText = GameObject.FindWithTag("UI_DialogueBox").GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (currentLine < dialogueLines.Length && activeDialogue && timerT > 0)
        {
            timerT -= Time.deltaTime;
            dialogueText.text = dialogueLines[currentLine];
        }
        else if (timerT <= 0 && currentLine < dialogueLines.Length - 1)
        {
            //Debug.Log(currentLine + " " + dialogueLines.Length);
            currentLine++;
            timerT = timer;
        }
        else if (timerT <= 0) {
            dialogueText.text = "";
            gameObject.GetComponent<BoxCollider>().enabled = true;
            if (destroyOnComplete) Destroy(gameObject);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        activeDialogue = true;
        currentLine = 0; // Reset dialog
        timerT = timer;
    }
}