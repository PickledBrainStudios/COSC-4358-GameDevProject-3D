using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowSoundControl : MonoBehaviour
{
    public float time = 5f;
    public AudioClip audioClip;
    public AudioSource audioSource;
    private float counter;

    // Start is called before the first frame update
    void Start()
    {
        counter = time;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter > 0f)
        {
            counter -= Time.deltaTime;
        }
        else
        {
            audioSource.PlayOneShot(audioClip);
            counter = time + Random.Range(-time * 1.25f, time * 1.25f);
        }
    }
}
