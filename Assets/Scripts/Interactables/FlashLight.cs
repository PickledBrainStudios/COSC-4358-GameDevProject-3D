using UnityEngine;

public class FlashLight : MonoBehaviour, IInteractable
{
    private GameObject player;
    private PlayerManager playerManager;
    public void Interact()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
        playerManager.ActivateFlashLight();
        Destroy(this.gameObject);
    }
}
