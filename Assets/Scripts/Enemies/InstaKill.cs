using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaKill : MonoBehaviour
{
    private Transform player;
    private PlayerManager playerManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerManager = player.GetComponent<PlayerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        { 
            playerManager.health = -1f; 
        }
        
    }
}
