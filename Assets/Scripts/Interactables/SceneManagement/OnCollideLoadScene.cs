using UnityEngine;
using UnityEngine.SceneManagement;

public class OnCollideLoadScene : MonoBehaviour
{
    public string nextScene;
    private GameObject player;
    private PlayerManager playerManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");//find player
        playerManager = player.GetComponent<PlayerManager>();//find playerManager Script
    }

    private void OnTriggerEnter(Collider other)
    {
        playerManager.spawnName = "Default_Spawn";
        SceneManager.LoadScene(nextScene);
    }
}
