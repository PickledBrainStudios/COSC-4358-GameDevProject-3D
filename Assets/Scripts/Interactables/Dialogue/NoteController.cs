using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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
    public PhysicalDoor physicalDoor;

    public GameObject[] activate;
    public GameObject[] destroy;

    public bool destroyOnComplete = false;
    public bool destroyObj = false;

    private AudioSource audioSource;
    private TextMeshProUGUI dialogueText;
    private RawImage rawImage;
    private int currentLine = 0;
    private GameObject player;
    private PlayerManager playerManager;
    private PauseController pauseController;
    private bool activeNote = false;
    private bool buttonPress = false;

    private PlayerInput playerInput;
    private InputActionMap actionMap;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");//find player
        playerManager = player.GetComponent<PlayerManager>();//find playerManager Script
        pauseController = player.GetComponent<PauseController>();
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

        playerInput = player.GetComponent<PlayerInput>();
        actionMap = playerInput.actions.FindActionMap("ReadingNote");
        Debug.Log(actionMap);

    }
    public void Interact()
    {
        // Display the note when the player interacts with it
        Time.timeScale = 0;

        playerManager.DeactivateControl();
        playerInput.SwitchCurrentActionMap("ReadingNote");

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
            if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)) && !Input.GetKeyDown(KeyCode.Escape))
            {
                dialogueText.text = dialogueLines[currentLine];
                currentLine++;
                //Debug.Log(currentLine + " " + dialogueLines.Length);
            }
        }
        else if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space)) && activeNote)
        {
            // Close the note when all dialogue is shown
            CloseNote();

            playerInput.SwitchCurrentActionMap("PlayerController");
            //actionMap.Disable();
            playerManager.ActivateControl();

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

        dialogueText.text = "";
        if (isKey)
        {
            try { door.UnlockDoor(); }
            catch { }
            try { physicalDoor.UnlockDoor(); }
            catch { }
        }

        try
        {
            foreach (GameObject obj in activate) {
                obj.SetActive(true);
            }
        }
        catch { }
        try
        {
            foreach (GameObject obj in destroy)
            {
                Destroy(obj);
            }
        }
        catch { }

        pauseController.readingNote = false;
        

        Time.timeScale = 1;
        if (destroyOnComplete)
        {
            playerInput.SwitchCurrentActionMap("PlayerController");
            //actionMap.Disable();
            playerManager.ActivateControl();

            Destroy(this);
        }
        if (destroyObj)
        {
            playerInput.SwitchCurrentActionMap("PlayerController");
            //actionMap.Disable();
            playerManager.ActivateControl();

            Destroy(this.gameObject);
        }
    }

    private void OnContine() {
        buttonPress = true;
        buttonPress = false;
        Debug.Log("FROM NOTE CONTROLLER");
    }

    private void OnTest() {
        Debug.Log("FROM NOTE CONTROLLER");
    }

    private void SwitchActionMap(InputAction.CallbackContext context) {
        playerInput.SwitchCurrentActionMap("ReadingNote");
    }
}