using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressMe : MonoBehaviour
{
    public AudioClip trapSound;
    public Texture2D jumpScareImage;
    public float timeTarget = 0.5f;
    private float timer;
    public RawImage rawImage;
    public AudioSource audioSource;
    private bool active = false;

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
}
