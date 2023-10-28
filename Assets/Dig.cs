using UnityEngine;
using TMPro;

public class Dig : MonoBehaviour, IInteractable
{

    public bool hasShovel = false;
    
    public string noToolDialogue = "A freshly laid grave. I could dig this up if I had a shovel.";
    public float timer = 3f;
    public AudioClip digClip;
    public AudioClip dirtClip;

    private LevelManager03 levelManager;
    private TextMeshProUGUI dialogueText;
    private float timerT;
    private bool activeDialogue = false;
    private bool toolCheck = false;

    private AudioSource playerSFX;


    // Start is called before the first frame update
    void Start()
    {
        dialogueText = GameObject.FindWithTag("UI_DialogueBox").GetComponent<TextMeshProUGUI>();
        playerSFX = GameObject.FindGameObjectWithTag("Player_AudioSource_02").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeDialogue)
        {
            if (timerT > 0)
            {
                timerT -= Time.deltaTime;
                dialogueText.text = noToolDialogue;
            }
            else
            {
                dialogueText.text = "";
                activeDialogue = false;
            }
        }
    }

    public void Interact() {
        try
        {
            levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager03>();
            toolCheck = levelManager.hasShovel;
        }
        catch
        {
            levelManager = null;
            toolCheck = false;
        }
        if (hasShovel || toolCheck)
        {
            playerSFX.Stop();

            playerSFX.PlayOneShot(digClip);

            Destroy(gameObject);
        }
        else
        {
            playerSFX.Stop();
            playerSFX.PlayOneShot(dirtClip);
            timerT = timer;
            activeDialogue = true;
        }
    }
}
