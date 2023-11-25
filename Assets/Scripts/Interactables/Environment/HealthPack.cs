using UnityEngine;

public class HealthPack : MonoBehaviour, IInteractable
{
    public int heal = 10;
    private GameObject player;
    private PlayerManager playerManager;

    public void Interact() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
        playerManager.health += heal;
        if (playerManager.health > 100)
        {
            playerManager.health = 100;
        }
        Destroy(gameObject);
    }
}
