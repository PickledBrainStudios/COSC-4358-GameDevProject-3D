using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NoteController : MonoBehaviour, IInteractable
{
    public bool isCharacter = false;
    public bool showImage;
    public Texture2D imageTexture;

    public string introLine;
    public string[] dialogueLines;

    public AudioClip pickUpClip;

    public bool isKey = false;
    public Door door;

    public bool destroyOnComplete = false;
    public bool destroyObj = false;

    private AudioSource audioSource;
    private TextMeshProUGUI dialogueText;
    private RawImage rawImage;
    private int currentLine = 0;
    private GameObject player;
    private PlayerManager playerManager;
    private bool activeNote = false;

    

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");//find player
        playerManager = player.GetComponent<PlayerManager>();//find playerManager Script
        if (isCharacter)
        {
            dialogueText = GameObject.FindWithTag("UI_DialogueBox").GetComponent<TextMeshProUGUI>();
        }
        else
        {
            dialogueText = GameObject.FindWithTag("UI_CenterScreen_Dialogue").GetComponent<TextMeshProUGUI>();
        }
        rawImage = GameObject.FindWithTag("UI_CenterScreen_Image").GetComponent<RawImage>();
        audioSource = GameObject.FindWithTag("Player_AudioSource").GetComponent<AudioSource>();
    }
    public void Interact()
    {
        // Display the note when the player interacts with it

        playerManager.DeactivateControl();
        audioSource.PlayOneShot(pickUpClip);
        dialogueText.text = ""; // Clear text initially
        dialogueText.text = introLine;
        rawImage.texture = imageTexture;
        //gameObject.GetComponent<Renderer>().enabled = false;
        if (showImage) { rawImage.enabled = true; }
        
        //gameObject.SetActive(true);
        currentLine = 0; // Reset dialog
        activeNote = true;
    }
    private void Update()
    {
        if (currentLine < dialogueLines.Length && activeNote)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dialogueText.text = dialogueLines[currentLine];
                currentLine++;
                //Debug.Log(currentLine + " " + dialogueLines.Length);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space) && activeNote)
        {
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
        if (isKey)
        {
            door.UnlockDoor();
        }
        if (destroyOnComplete) 
        { 
            Destroy(this); 
        }
        if (destroyObj)
        {
            Destroy(this.gameObject);
        }
    }
}