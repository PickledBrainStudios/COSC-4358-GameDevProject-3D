using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    public string nextScene;
    public string nextSpawnName;
    private GameObject player;
    private PlayerManager playerManager;


    //On interact
    public void Interact() {
        //Debug.Log("LET ME IN!!! LET ME INNNNNN!!!");
        player = GameObject.FindGameObjectWithTag("Player");//find player
        playerManager = player.GetComponent<PlayerManager>();//find playerManager Script
        playerManager.spawnName = nextSpawnName;//modify their spawn location, so that when we load the next scene they will spawn in the correct spot
        SceneManager.LoadScene(nextScene);//load next scene
    }
}
