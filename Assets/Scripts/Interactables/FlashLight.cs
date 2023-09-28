using UnityEngine;

public class FlashLight : MonoBehaviour, IInteractable
{
    private GameObject player;
    private FlashLightController flashLight;
    public void Interact()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        flashLight = player.GetComponent<FlashLightController>();
        flashLight.ActivateFlashLight();
        Destroy(this.gameObject);
    }
}
