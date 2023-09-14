using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector]
    public float health = 100;

    [HideInInspector]
    public PlayerManager playerManager;

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



    // Start is called befosre the first frame update
    void Start()
    {
        playerManager = this;
        interactor = GetComponent<Interactor>();
        move = GetComponent<Movement>();
        look = GetComponent<Look>();
        crouch = GetComponent<Crouch>();
        jump = GetComponent<Jump>();

        healthUI = GameObject.Find("Health").GetComponent<TextMeshProUGUI>();

        centerText = GameObject.Find("CenterScreen").GetComponent<TextMeshProUGUI>();
        centerText.text = "test";

        flashLight = GameObject.Find("FFlashLight");
        flashLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthUI.text = "Health: " + health.ToString();

        if (Input.GetKeyDown(KeyCode.F) && hasFlashLight) {ToggleLight();}

        if (health <= 0 && !isDead) {
            Death();
            isDead = true;
        }
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy01"))
        {
            qte01 = other.GetComponent<QTE01>();
            qte01.StartQTE(playerManager);
        }
        if (other.gameObject.CompareTag("Enemy02"))
        {
            //qteScript.StartQTE();
        }
        if (other.gameObject.CompareTag("Enemy03"))
        {
            //qteScript.StartQTE();
        }
    }*/

    public void ToggleControl()
    {
        Debug.Log("TOGGLE CONTROL");
        move.enabled = !move.enabled;
        look.enabled = !look.enabled;
        crouch.enabled = !crouch.enabled;
        jump.enabled = !jump.enabled;
        interactor.enabled = !interactor.enabled;
    }

    public void ActivateFlashLight() {
        hasFlashLight = true;
        flashLight.SetActive(true);
    }

    public void ToggleLight() {
        flashLight.SetActive(!flashLight.activeSelf);
    }

    private void Death() {
        ToggleControl();
        centerText.text = "GAME OVER";
    }

}
