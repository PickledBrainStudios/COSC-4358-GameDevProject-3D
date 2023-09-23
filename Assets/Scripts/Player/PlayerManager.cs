using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector]
    public float health = 100;
    private bool isDead = false;

    //This determine the spawn location of the player when they enter a new scene
    [HideInInspector]
    public string spawnName = "Default_Spawn";

    public List<string> keyInventory;

    private Interactor interactor;
    private ControlFlashLight flashLight;
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
    void Start()
    {
        interactor = GetComponent<Interactor>();
        flashLight = GetComponent<ControlFlashLight>();
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

        /*
        //Check to see if player presses F to toggle flash light
        if (Input.GetKeyDown(KeyCode.F) && hasFlashLight) 
        {
            ToggleLight();
        }
        */

        //Check to see if player die, and can only die onces
        if (health <= 0 && !isDead) {
            Death();
            isDead = true;
        }
    }

    //Toggles player scripts and controls, available to be used in other scripts
    public void ToggleControl()
    {
        move.enabled = !move.enabled;
        look.enabled = !look.enabled;
        crouch.enabled = !crouch.enabled;
        jump.enabled = !jump.enabled;
        interactor.enabled = !interactor.enabled;
        flashLight.enabled = !flashLight.enabled;
    }

    /*
    //Activates only once when the player picks up the flashlight 
    public void ActivateFlashLight() {
        hasFlashLight = true;
        flashLight.SetActive(true);
        audioSource.PlayOneShot(soundClip);
    }

    //Turns flash light on and off
    public void ToggleLight() {
        audioSource.Stop();
        audioSource.PlayOneShot(soundClip);
        flashLight.SetActive(!flashLight.activeSelf);  
    }
    */

    //Meant to end game
    private void Death() {
        ToggleControl();
        centerText.text = "GAME OVER";
        this.enabled = false;
    }

    public void KeyPickup(string key) {
        keyInventory.Add(key);
    }
}
