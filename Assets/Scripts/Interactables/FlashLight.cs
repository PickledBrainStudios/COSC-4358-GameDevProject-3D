using UnityEngine;

public class FlashLight : MonoBehaviour, IInteractable
{
    private GameObject player;
    private ControlFlashLight flashLight;
    public void Interact()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        flashLight = player.GetComponent<ControlFlashLight>();
        flashLight.ActivateFlashLight();
        Destroy(this.gameObject);
    }
}
