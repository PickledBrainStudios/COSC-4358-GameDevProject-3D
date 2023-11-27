using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressMe : MonoBehaviour
{
    //public AudioClip trapSound;
    //public Texture2D jumpScareImage;
    //public float timeTarget = 0.5f;

    //public RawImage rawImage;
    //public AudioSource audioSource;
    public GameObject disclaimer;
    public GameObject menu;
    private bool active = false;
    private float timer;

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (active && timer <= 0f)
        {
            rawImage.enabled = false;
            active = false;
        }
    }


    public void OnPress()
    {
        timer = timeTarget;
        active = true;
        rawImage.texture = jumpScareImage;
        rawImage.enabled = true;
        audioSource.PlayOneShot(trapSound);
    }
    */
    public void toggleDisclaimer() {
        menu.SetActive(true);
        disclaimer.SetActive(false);
    }
}
