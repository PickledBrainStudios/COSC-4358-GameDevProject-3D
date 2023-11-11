using UnityEngine.UI;
using UnityEngine;

public class RadioPuzzle : MonoBehaviour, IInteractable
{

    //player will roll two dials to a specific tuning to get the radio to sound off

    public int targetLeftDial;
    public int targetRightDial;
    public Slider leftDialSlider;
    public Slider rightDialSlider;
    public RectTransform leftKnob;
    public RectTransform rightKnob;
    public GameObject radioUI;

    private PlayerManager playerManager;
    private bool usingRadio = false;
    private int leftDialValue;
    private int rightDialValue;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
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
        
        leftDialValue = (int)leftDialSlider.value;
        leftKnob.rotation = Quaternion.Euler(0, 0, -leftDialValue * 8.65f + 124f);
        Debug.Log(leftDialValue);
    }
    public void ChangeDial02()
    {

        rightDialValue = (int)rightDialSlider.value;
        rightKnob.rotation = Quaternion.Euler(0, 0, -rightDialValue * 8.65f + 124f);
        Debug.Log(rightDialValue);
    }

    private void CompletePuzzle() {
        Debug.Log("Puzzle Complete!");
    }
}
