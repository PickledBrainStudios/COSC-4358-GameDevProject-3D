using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    public string nextScene;
    public string nextSpawnName;
    private GameObject player;
    private PlayerManager playerManager;

    public void Interact() {
        //Debug.Log("LET ME IN!!! LET ME INNNNNN!!!");
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
        playerManager.spawnName = nextSpawnName;
        SceneManager.LoadScene(nextScene);
    }
}
