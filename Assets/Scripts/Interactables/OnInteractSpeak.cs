using UnityEngine;
using TMPro;

public class OnInteractSpeak : MonoBehaviour, IInteractable
{
    public string[] dialogueLines;
    public float timer = 3f;
    public AudioClip audioClip;
    public bool destroyOnComplete = false;


    private float timerT;
    private TextMeshProUGUI dialogueText;
    private int currentLine = 0;
    private bool activeDialogue = false;
    private AudioSource audioSource;

    private void Start()
    {
        dialogueText = GameObject.FindWithTag("UI_DialogueBox").GetComponent<TextMeshProUGUI>();
        audioSource = GameObject.Find("SFX-1").GetComponent<AudioSource>();
    }

    public void Interact()
    {
        //Debug.Log("Activate Dialogue");
        activeDialogue = true;
        currentLine = 0; // Reset dialog
        timerT = timer;
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
        if (destroyOnComplete) {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
    private void Update()
    {
        if (activeDialogue)
        {
            if (currentLine < dialogueLines.Length && timerT > 0)
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
            else if (timerT <= 0)
            {
                dialogueText.text = "";
                activeDialogue = false;
                if (destroyOnComplete) Destroy(gameObject);
            }
        }
    }
}