using UnityEngine;

public class FlashLight : MonoBehaviour, IInteractable
{
    private GameObject player;
    private FlashLightController flashLight;
    public GameObject[] objectsToDestroy;
    public void Interact()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        flashLight = player.GetComponent<FlashLightController>();
        flashLight.ActivateFlashLight();
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj);
        }
        Destroy(this.gameObject);
    }
}
