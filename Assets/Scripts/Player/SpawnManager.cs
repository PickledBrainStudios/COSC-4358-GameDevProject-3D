using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public string[] objectsToDestroy;
    public float fadeSpeed = 0.5f;
    public bool changeSky = false;
    public bool solidSky = false;
    public bool skyBox = false;
    public Color color = Color.black;
    public Material newSky;

    private GameObject player;
    private PlayerManager playerManager;
    private GameObject spawnLocation;
    private RawImage fade;
    private TextMeshProUGUI dialogueText;
    private bool postitionPlayer = true;
    private bool unFreeze = false;
    private bool unFrozen = false;
    private bool fadeIn = false;
    private float realValue = 1f;


    private void Start()
    {
        Time.timeScale = 1;
        //Debug.Log("SpawnManager Active");
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
        spawnLocation = GameObject.Find(playerManager.spawnName);
        fade = GameObject.FindWithTag("UI_Fade").GetComponent<RawImage>();
        fade.color = new Color(0f, 0f, 0f, 1f);
        dialogueText = GameObject.FindWithTag("UI_DialogueBox").GetComponent<TextMeshProUGUI>();
        dialogueText.text = "";
        playerManager.DeactivateControl();

        if (solidSky) {
            Camera.main.clearFlags = CameraClearFlags.SolidColor;
            Camera.main.backgroundColor = color;
        }
        if (skyBox) {
            Camera.main.clearFlags = CameraClearFlags.Skybox;
        }
        else if (changeSky)
        {
            Camera.main.clearFlags = CameraClearFlags.Skybox;
            RenderSettings.skybox = newSky;
        }
        if (objectsToDestroy.Length > 0)
        {
            foreach (string obj in objectsToDestroy)
            {
                try { Destroy(GameObject.Find(obj)); }
                catch { }
            }
        }
    }
    private void Update()
    {
        if (player.transform.position != spawnLocation.transform.position && postitionPlayer)
        {
            player.transform.localEulerAngles = spawnLocation.transform.localEulerAngles;
            Camera.main.transform.localEulerAngles = spawnLocation.transform.forward;
            player.transform.position = spawnLocation.transform.position;

            //fadeIn = true;
            //postitionPlayer = false;
            //Debug.Log("Fade in on");
        }
        else
        {
            unFreeze = true;
            fadeIn = true;
            postitionPlayer = false;
            
        }
        if (unFreeze && !unFrozen)
        {
            playerManager.ActivateControl();
            unFreeze = false;
            unFrozen = true;
        }
        if (fadeIn)
        {
            if (realValue > 0f)
            {
                //Debug.Log(realValue);
                realValue -= Time.deltaTime * fadeSpeed;
                fade.color = new Color(0f, 0f, 0f, realValue);
                //Debug.Log(fade.color);
            }
            else
            {
                fadeIn = false;
                Destroy(gameObject);
            }
        }
    }
}
