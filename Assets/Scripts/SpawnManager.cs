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
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
        spawnLocation = GameObject.Find(playerManager.spawnName);
        playerManager.ToggleControl();

    }
    private void Update()
    {
        
        if (player.transform.position != spawnLocation.transform.position)
        {
            player.transform.position = spawnLocation.transform.position;
        }
        else
        {
            playerManager.ToggleControl();
            Destroy(gameObject);
        }
    }
}
