using UnityEngine;
using TMPro;


public class OnCollideSpeak : MonoBehaviour
{
    public float pause = 0f;
    public string[] dialogueLines;
    public float timer = 3f;
    public bool destroyOnComplete = false;

    private float timerP;
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
        if (timerP > 0) 
        {
            timerP -= Time.deltaTime;
        }
        else {
            if (timerT > 0 && currentLine < dialogueLines.Length && activeDialogue)
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
            else if (timerT <= 0 && activeDialogue) {
                dialogueText.text = "";
                gameObject.GetComponent<BoxCollider>().enabled = true;
                activeDialogue = false;
                if (destroyOnComplete && !activeDialogue) Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        activeDialogue = true;
        currentLine = 0; // Reset dialog
        timerP = pause;
        timerT = timer;
    }
}