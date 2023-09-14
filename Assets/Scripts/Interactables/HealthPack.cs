using UnityEngine;

public class HealthPack : MonoBehaviour, CConsumable
{
    public int heal = 10;
    private GameObject player;
    private PlayerManager playerManager;

    public void Consume() {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
        Debug.Log(player.name);
        if (playerManager.health < 90)
        {
            playerManager.health += heal;
        }
        else 
        {
            playerManager.health = 100;
        }
        Destroy(this.gameObject);
    }
}
