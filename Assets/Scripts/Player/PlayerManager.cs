using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector]
    public float health = 100;

    //private PlayerManager playerManager;

    public string spawnName = "Default_Spawn";

    private Interactor interactor;
    private Movement move;
    private Look look;
    private Crouch crouch;
    private Jump jump;
    
    private bool isDead = false;
    private bool hasFlashLight = false;

    private GameObject flashLight;

    private TextMeshProUGUI healthUI;
    private TextMeshProUGUI centerText;

    public AudioSource audioSource;
    public AudioClip soundClip;

    // Start is called befosre the first frame update
    void Start()
    {
        //playerManager = this;
        interactor = GetComponent<Interactor>();
        move = GetComponent<Movement>();
        look = GetComponent<Look>();
        crouch = GetComponent<Crouch>();
        jump = GetComponent<Jump>();

        healthUI = GameObject.Find("Health").GetComponent<TextMeshProUGUI>();
        centerText = GameObject.Find("CenterScreen").GetComponent<TextMeshProUGUI>();

        flashLight = GameObject.Find("FFlashLight");
        flashLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthUI.text = "Health: " + health.ToString();

        if (Input.GetKeyDown(KeyCode.F) && hasFlashLight) 
        {
            ToggleLight();
        }

        if (health <= 0 && !isDead) {
            Death();
            isDead = true;
        }
    }

    public void ToggleControl()
    {
        move.enabled = !move.enabled;
        look.enabled = !look.enabled;
        crouch.enabled = !crouch.enabled;
        jump.enabled = !jump.enabled;
        interactor.enabled = !interactor.enabled;
    }

    public void ActivateFlashLight() {
        hasFlashLight = true;
        flashLight.SetActive(true);
        audioSource.PlayOneShot(soundClip);
    }

    public void ToggleLight() {
        audioSource.Stop();
        audioSource.PlayOneShot(soundClip);
        flashLight.SetActive(!flashLight.activeSelf);  
    }

    private void Death() {
        ToggleControl();
        centerText.text = "GAME OVER";
    }
}
