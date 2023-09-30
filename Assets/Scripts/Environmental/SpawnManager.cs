using System.Collections;
using System.Collections.Generic;
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


    private void Start()
    {
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
        //Debug.Log("SpawnManager Active");
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
        spawnLocation = GameObject.Find(playerManager.spawnName);
        playerManager.ToggleControl();
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
        }
        else
        {
            //Debug.Log("Player Spawned");
            playerManager.ToggleControl();
            Destroy(gameObject);
        }
    }
}
