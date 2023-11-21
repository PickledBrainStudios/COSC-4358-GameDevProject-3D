using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpFade : MonoBehaviour
{
    public AudioClip trapSound;
    public Texture2D jumpScareImage;
    public float visibleTime = 1f;
    public float fadeSpeed = 3f;
    private float vTimer;

    private RawImage rawImage;
    private AudioSource audioSource;
    private bool active = false;
    private bool isFading = false;
    private float realAlpha;

       //shows up
       //opaque time -- time it stays 100% visible
       //fade time -- public timer

       //sound is immediate

    // Start is called before the first frame update
    void Start()
    {
        rawImage = GameObject.FindWithTag("UI_JumpScare_Raw").GetComponent<RawImage>();
        audioSource = GameObject.FindGameObjectWithTag("Player_AudioSource_02").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active && vTimer > 0)
        {
            vTimer -= Time.deltaTime;
        }
        else if (active && vTimer <= 0) 
        {
            isFading = true;
        }

        if (isFading) 
        {
            realAlpha -= Time.deltaTime * fadeSpeed;
            rawImage.color = new Color(1f,1f,1f,realAlpha);
            if (rawImage.color.a <= 0f) 
            {
                rawImage.enabled = false;
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter()
    {
        vTimer = visibleTime;
        active = true;
        rawImage.texture = jumpScareImage;
        realAlpha = 1f;
        rawImage.color = new Color(1f, 1f, 1f, realAlpha);
        rawImage.enabled = true;
        audioSource.PlayOneShot(trapSound);
    }
}
