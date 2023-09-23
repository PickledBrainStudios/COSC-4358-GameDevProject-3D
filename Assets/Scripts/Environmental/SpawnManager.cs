using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject player;
    public PlayerManager playerManager;
    public GameObject spawnLocation;

    private void Start()
    {
        //Debug.Log("SpawnManager Active");
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
        spawnLocation = GameObject.Find(playerManager.spawnName);
        Debug.Log("line before");
        playerManager.ToggleControl();
        Debug.Log("line after");
    }
    private void Update()
    {
        
        if (player.transform.position != spawnLocation.transform.position)
        {
            player.transform.position = spawnLocation.transform.position;
            Debug.Log("Spawn Player");
        }
        else
        {
            //Debug.Log("Player Spawned");
            playerManager.ToggleControl();
            Debug.Log("PlayerSpawned");
            Destroy(gameObject);
        }
    }
}
