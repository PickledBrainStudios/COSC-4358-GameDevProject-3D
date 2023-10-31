using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Cutscene : MonoBehaviour
{
    private GameObject player;
    private VideoPlayer myVideo;
    private SceneLoader loader;
    private float timer = 3f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);
        myVideo = GetComponent<VideoPlayer>();
        loader = GetComponent<SceneLoader>();
        myVideo.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Debug.Log(myVideo.isPlaying);
            if (!myVideo.isPlaying) {
                player.SetActive(true);
                loader.LoadScene();
            }
        }

    }
}
