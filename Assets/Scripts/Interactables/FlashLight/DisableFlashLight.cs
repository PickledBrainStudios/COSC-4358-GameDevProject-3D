using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableFlashLight : MonoBehaviour
{

    private GameObject player;
    private FlashLightController flashLight;
    private AudioSource fAudio;
    public AudioClip breakClip;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        flashLight = player.GetComponent<FlashLightController>();
        fAudio = GameObject.Find("FlashLightClick").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        flashLight.DeactivateLight();
        fAudio.Stop();
        fAudio.PlayOneShot(breakClip);
        //Destroy(gameObject);
    }
}
