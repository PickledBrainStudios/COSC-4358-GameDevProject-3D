using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableFlashLight : MonoBehaviour
{

    public AudioClip breakClip;
    public bool onStay = false;

    private GameObject player;
    private FlashLightController flashLight;
    private AudioSource fAudio;




    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        flashLight = player.GetComponent<FlashLightController>();
        fAudio = GameObject.Find("FlashLightClick").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        fAudio.Stop();
        fAudio.PlayOneShot(breakClip);
        flashLight.DeactivateLight();
        Destroy(gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        if (onStay) {
            fAudio.Stop();
            fAudio.PlayOneShot(breakClip);
            flashLight.DeactivateLight();
            Destroy(gameObject);
        }
    }
}
