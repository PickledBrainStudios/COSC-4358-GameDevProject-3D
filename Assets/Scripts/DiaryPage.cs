using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DiaryPage : MonoBehaviour, IInteractable
{
    public bool showImage;
    public Texture2D imageTexture;

    public string introLine;
    public string[] dialogueLines;

    public AudioClip pickUpClip;

    public GameObject[] activate;
    public GameObject[] destroy;

    public Diary diary;

    private AudioSource audioSource;
    private TextMeshProUGUI dialogueText;
    private RawImage rawImage;
    private int currentLine = 0;
    private GameObject player;
    private PlayerManager playerManager;
    private PauseController pauseController;
    private bool activeNote = false;

    private DiaryPuzzle diaryPuzzle;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");//find player
        playerManager = player.GetComponent<PlayerManager>();//find playerManager Script
        pauseController = player.GetComponent<PauseController>();
        dialogueText = GameObject.FindWithTag("UI_CenterScreen_Dialogue").GetComponent<TextMeshProUGUI>();
        rawImage = GameObject.FindWithTag("UI_CenterScreen_Image").GetComponent<RawImage>();
        audioSource = GameObject.FindWithTag("Player_AudioSource").GetComponent<AudioSource>();
        diaryPuzzle = GameObject.FindWithTag("DiaryPuzzle").GetComponent<DiaryPuzzle>();
    }
    public void Interact()
    {
        // Display the note when the player interacts with it
        diary.timerT = 0;
        Time.timeScale = 0;
        audioSource.PlayOneShot(pickUpClip);
        dialogueText.text = ""; // Clear text initially
        dialogueText.text = introLine;
        rawImage.texture = imageTexture;
        //gameObject.GetComponent<Renderer>().enabled = false;
        if (showImage) { rawImage.enabled = true; }

        //gameObject.SetActive(true);
        currentLine = 0; // Reset dialog
        activeNote = true;
        pauseController.readingNote = true;

    }
    private void Update()
    {
        if (currentLine < dialogueLines.Length && activeNote)
        {
            playerManager.DeactivateControl();
            if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)) && !Input.GetKeyDown(KeyCode.Escape))
            {
                dialogueText.text = dialogueLines[currentLine];
                currentLine++;
                //Debug.Log(currentLine + " " + dialogueLines.Length);
            }
        }
        else if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)) && !Input.GetKeyDown(KeyCode.Escape) && activeNote)
        {
            playerManager.DeactivateControl();
            // Close the note when all dialogue is shown
            CloseNote();
        }
    }
    private void CloseNote()
    {
        // Hide the note and resume gameplay
        //gameObject.SetActive(false);
        rawImage.texture = null;
        rawImage.enabled = false;
        activeNote = false;
        //gameObject.GetComponent<Renderer>().enabled = true;
        playerManager.ActivateControl();
        dialogueText.text = "";
        pauseController.readingNote = false;
        diaryPuzzle.PickUpPage();
        Time.timeScale = 1;
        try {        
            foreach (GameObject obj in destroy)
            {
                Destroy(obj);
            } 
        }
        catch { }
        try {        
            foreach (GameObject obj in activate)
            {   
                obj.SetActive(true);
            } 
        }
        catch { }

        Destroy(this.gameObject);
    }
}
