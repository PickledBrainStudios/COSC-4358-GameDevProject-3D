using UnityEngine.UI;
using UnityEngine;

public class RadioPuzzle : MonoBehaviour, IInteractable
{

    //player will roll two dials to a specific tuning to get the radio to sound off

    public AudioSource radio;
    public int targetLeftDial;
    public int targetRightDial;
    public Slider leftDialSlider;
    public Slider rightDialSlider;
    public RectTransform leftKnob;
    public RectTransform rightKnob;
    public GameObject radioUI;
    public AudioClip radioClicks;

    public GameObject[] activate;
    public GameObject[] destroy;

    

    private PlayerManager playerManager;
    private bool usingRadio = false;
    private int leftDialValue;
    private int rightDialValue;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        audioSource = GameObject.FindGameObjectWithTag("Player_AudioSource_02").GetComponent<AudioSource>();
        leftDialValue = (int)leftDialSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        if (usingRadio && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) && !Input.GetKeyDown(KeyCode.Escape)))
        {
            radioUI.SetActive(false);
            usingRadio = false;
            Cursor.lockState = CursorLockMode.Locked;
            playerManager.ActivateControl();
        }

        if (leftDialValue == targetLeftDial) {
            Debug.Log("LEFT CORRECT VALUE!");
        }
        if (rightDialValue == targetRightDial)
        {
            Debug.Log("RIGHT CORRECT VALUE!");
        }
        if (leftDialValue == targetLeftDial && rightDialValue == targetRightDial) {
            CompletePuzzle();
        }
    }

    public void Interact() {
        //activate UI
        playerManager.DeactivateControl();
        radioUI.SetActive(true);
        usingRadio = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ChangeDial01()
    {
        
        audioSource.Stop();
        audioSource.PlayOneShot(radioClicks);
        leftDialValue = (int)leftDialSlider.value;
        float mag = Mathf.Abs(targetLeftDial - leftDialValue) * 0.008f;
        radio.pitch = 1 - mag;
        leftKnob.rotation = Quaternion.Euler(0, 0, -leftDialValue * 8.65f + 124f);
        Debug.Log(leftDialValue);
    }
    public void ChangeDial02()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(radioClicks);
        rightDialValue = (int)rightDialSlider.value;
        float mag = Mathf.Abs(targetRightDial - rightDialValue) *.008f;
        radio.pitch = 1 + mag;
        rightKnob.rotation = Quaternion.Euler(0, 0, -rightDialValue * 8.65f + 124f);
        Debug.Log(rightDialValue);
    }

    private void CompletePuzzle() {
        Debug.Log("Puzzle Complete!");
        try
        {
            foreach (GameObject obj in destroy)
            {
                Destroy(obj);
            }
        }
        catch { }
        try
        {
            foreach (GameObject obj in activate)
            {
                obj.SetActive(true);
            }
        }
        catch { }

        radioUI.SetActive(false);
        usingRadio = false;
        Cursor.lockState = CursorLockMode.Locked;
        playerManager.ActivateControl();

        Destroy(gameObject);
    }
}
