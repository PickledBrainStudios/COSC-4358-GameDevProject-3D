using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFlashLight : MonoBehaviour
{

    private GameObject player;
    private FlashLightController flashLight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        flashLight = player.GetComponent<FlashLightController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        flashLight.ActivateFlashLight();
        Destroy(gameObject);
    }
}
