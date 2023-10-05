using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector]
    public float health = 100;
    private bool isDead = false;

    //This determine the spawn location of the player when they enter a new scene
    //[HideInInspector]
    public string spawnName = "Default_Spawn";

    //public List<string> keyInventory;

    private Interactor interactor;
    private FlashLightController flashLight;
    private Movement move;
    private Look look;
    private Crouch crouch;
    private Jump jump;
    
    private TextMeshProUGUI healthUI;
    private TextMeshProUGUI centerText;

    //private GameObject flashLight;
    //public AudioSource audioSource;
    //public AudioClip soundClip;
    //private bool hasFlashLight = false;

    private bool debugMode = false;

    // Start is called befosre the first frame update
    void Awake()
    {
        interactor = GetComponent<Interactor>();
        flashLight = GetComponent<FlashLightController>();
        move = GetComponent<Movement>();
        look = GetComponent<Look>();
        crouch = GetComponent<Crouch>();
        jump = GetComponent<Jump>();
        
        healthUI = GameObject.Find("Health").GetComponent<TextMeshProUGUI>();
        centerText = GameObject.Find("CenterScreen").GetComponent<TextMeshProUGUI>();

        //flashLight = GameObject.Find("FFlashLight");
        //flashLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Updates health UI with health variable
        if (!isDead) { healthUI.text = "Health: " + health.ToString(); }
        else { healthUI.text = "Dead"; }
        

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

    //We need this fix so the door and spawn manager scene loads work properly***************
    //public void disableControl();
    //public void activeControl();

    //Meant to end game
    private void Death() {
        ToggleControl();
        centerText.text = "GAME OVER";
        this.enabled = false;
    }

    /*
    public void KeyPickup(string key) {
        keyInventory.Add(key);
    }
    */
}
