using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ControlFlashLight : MonoBehaviour
{
    private GameObject flashLight;
    public AudioSource audioSource;
    public AudioClip soundClip;
    private bool hasFlashLight = false;

    // Start is called before the first frame update
    void Start()
    {
        flashLight = GameObject.Find("FFlashLight");
        flashLight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && hasFlashLight)
        {
            ToggleLight();
        }
    }

    public void ToggleLight()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(soundClip);
        flashLight.SetActive(!flashLight.activeSelf);
    }

    //Activates only once when the player picks up the flashlight 
    public void ActivateFlashLight()
    {
        hasFlashLight = true;
        flashLight.SetActive(true);
        audioSource.PlayOneShot(soundClip);
    }

    public void DeactivateLight()
    {
        hasFlashLight = false;
        flashLight.SetActive(false);
        //audioSource.PlayOneShot(soundClip); //will want to add light breaking sound effect
    }
}
