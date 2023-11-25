using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpScareTrap : MonoBehaviour
{
    public AudioClip trapSound;
    public Texture2D jumpScareImage;
    public float timeTarget = 0.5f;
    private float timer;
    private RawImage rawImage;
    private AudioSource audioSource;
    private bool active = false;


    // Start is called before the first frame update
    void Start()
    {
        rawImage = GameObject.FindWithTag("UI_JumpScare_Raw").GetComponent<RawImage>();
        audioSource = GameObject.FindGameObjectWithTag("Player_AudioSource_02").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (active && timer <= 0f)
        {
            rawImage.enabled = false;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter()
    {
        timer = timeTarget;
        active = true;
        rawImage.texture = jumpScareImage;
        rawImage.enabled = true;
        audioSource.PlayOneShot(trapSound);
    }
}
