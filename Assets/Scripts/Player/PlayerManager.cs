using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector]
    public float health = 100;
    private bool isDead = false;

    public string spawnName = "Default_Spawn";
    public bool inControl = true;

    private Interactor interactor;
    private FlashLightController flashLight;
    private Movement move;
    private Look look;
    private Crouch crouch;
    private Jump jump;
    private PauseController pause;
    
    private TextMeshProUGUI centerText;
    private RawImage heart;

    private bool debugMode = false;

    private PlayerInput playerInput;
    private InputActionMap actionMap;


    // Start is called befosre the first frame update
    void Awake()
    {
        interactor = GetComponent<Interactor>();
        flashLight = GetComponent<FlashLightController>();
        move = GetComponent<Movement>();
        look = GetComponent<Look>();
        crouch = GetComponent<Crouch>();
        jump = GetComponent<Jump>();
        pause = GetComponent<PauseController>();
        
        centerText = GameObject.FindGameObjectWithTag("UI_CenterScreen_Dialogue").GetComponent<TextMeshProUGUI>();
        heart = GameObject.FindGameObjectWithTag("UI_Health").GetComponent<RawImage>();

        playerInput = GetComponent<PlayerInput>();
        actionMap = playerInput.actions.FindActionMap("PlayerController");

    }

    // Update is called once per frame
    void Update()
    {
        //Updates health UI with health variable
        //if (!isDead) { healthUI.text = "Health: " + Mathf.Round(health).ToString(); }
        //else { healthUI.text = "Dead"; }
        
        heart.color = new Color(1f, 1f, 1f, (100 - health) * .01f);

        //for debugging in the build mode
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (debugMode)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else 
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        //Check to see if player die, and can only die onces
        if (health <= 0 && !isDead) {
            isDead = true;
            Death();
        }
    }

    /*
    //Toggles player scripts and controls, available to be used in other scripts
    public void ToggleControl()
    {
        //Debug.Log("IM TOGGLING CONTROL IN THE FUNCTION");
        //Debug.Log("move toggle");
        move.enabled = !move.enabled;
        //Debug.Log("look toggle");
        look.enabled = !look.enabled;
        //Debug.Log("crouch toggle");
        crouch.enabled = !crouch.enabled;
        //Debug.Log("jump toggle");
        jump.enabled = !jump.enabled;
        //Debug.Log("interactor toggle");
        interactor.enabled = !interactor.enabled;
        //Debug.Log("flash light toggle");
        flashLight.enabled = !flashLight.enabled;
    }
    */

    public void ActivateControl()
    {
        actionMap.Enable();

        //Debug.Log("IM TOGGLING CONTROL IN THE FUNCTION");
        //Debug.Log("move toggle");
        move.enabled = true;
        //Debug.Log("look toggle");
        look.enabled = true;
        //Debug.Log("crouch toggle");
        crouch.enabled = true;
        //Debug.Log("jump toggle");
        jump.enabled = true;
        //Debug.Log("interactor toggle");
        interactor.enabled = true;
        //Debug.Log("flash light toggle");
        flashLight.enabled = true;

        pause.enabled = true;

        inControl = true;  
    }

    public void DeactivateControl()
    {
        actionMap.Disable();

        //Debug.Log("IM TOGGLING CONTROL IN THE FUNCTION");
        //Debug.Log("move toggle");
        move.enabled = false;
        //Debug.Log("look toggle");
        look.enabled = false;
        //Debug.Log("crouch toggle");
        crouch.enabled = false;
        //Debug.Log("jump toggle");
        jump.enabled = false;
        //Debug.Log("interactor toggle");
        interactor.enabled = false;
        //Debug.Log("flash light toggle");
        flashLight.enabled = false;

        pause.enabled = false;

        inControl = false;
    }

    //We need this fix so the door and spawn manager scene loads work properly***************
    //public void disableControl();
    //public void activeControl();

    //Meant to end game
    private void Death() {
        DeactivateControl();
        centerText.text = "GAME OVER";
        this.enabled = false;
    }

    /*
    public void KeyPickup(string key) {
        keyInventory.Add(key);
    }
    */
}
