using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Diary : MonoBehaviour, IInteractable
{
    public float timer = 3f;
    public AudioClip audioClip;
    public bool destroyOnComplete = false;

    public float timerT;
    private TextMeshProUGUI dialogueText;
    private int currentLine = 0;
    private bool activeDialogue = false;
    private AudioSource audioSource;

    private DiaryPuzzle diaryPuzzle;

    private void Start()
    {
        dialogueText = GameObject.FindWithTag("UI_DialogueBox").GetComponent<TextMeshProUGUI>();
        audioSource = GameObject.Find("SFX-1").GetComponent<AudioSource>();
        diaryPuzzle = GameObject.FindWithTag("DiaryPuzzle").GetComponent<DiaryPuzzle>();
    }

    public void Interact()
    {
        //Debug.Log("Activate Dialogue");
        activeDialogue = true;
        currentLine = 0; // Reset dialog
        timerT = timer;
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
        if (destroyOnComplete)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
    private void Update()
    {
        if (activeDialogue)
        {
            if (timerT > 0)
            {
                timerT -= Time.deltaTime;
                if (diaryPuzzle.pages == 0) { dialogueText.text = "There are six remaining pages... Collect them all so that you might know the truth of things."; }
                if (diaryPuzzle.pages == 1) { dialogueText.text = "You have collected one of the six pages. You simply could not know enough. Collect them all to reveal what lies in the dark."; }
                if (diaryPuzzle.pages > 1) { dialogueText.text = "You have collected " + diaryPuzzle.pages.ToString() + " of the six pages... Things are not always what they seem. Gather the rest to past the test."; }
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
