using UnityEngine.UI;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public string[] objectsToDestroy;
    public bool changeSky = false;
    public bool solidSky = false;
    public bool skyBox = false;
    public Color color = Color.black;
    public Material newSky;
    private GameObject player;
    private PlayerManager playerManager;
    private GameObject spawnLocation;

    private RawImage fade;
    private bool fadeIn = false;
    public float fadeIncrement = 0.5f;
    private float realValue = 1f;
    private bool playerFrozen = true;

    private void Start()
    {
        //Debug.Log("SpawnManager Active");
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
        spawnLocation = GameObject.Find(playerManager.spawnName);
        fade = GameObject.FindWithTag("UI_Fade").GetComponent<RawImage>();
        fade.color = new Color(0f, 0f, 0f, 1f);
        playerManager.ToggleControl();

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
                Destroy(GameObject.Find(obj));
            }
        }
    }
    private void Update()
    {
        if (player.transform.position != spawnLocation.transform.position)
        {
            player.transform.position = spawnLocation.transform.position;
            fadeIn = true;
            Debug.Log("Fade in on");
        }
        else if (playerFrozen) {
            playerManager.ToggleControl();
            playerFrozen = false;
        }
        if (fadeIn)
        {
            if (realValue > 0f)
            {
                //Debug.Log(realValue);
                realValue -= Time.deltaTime * fadeIncrement;
                fade.color = new Color(0f, 0f, 0f, realValue);
                Debug.Log(fade.color);
            }
            else
            {
                fadeIn = false;
                Destroy(gameObject);
            }
        }
    }
}
