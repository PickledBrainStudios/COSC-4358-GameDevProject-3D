using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEnvironment : MonoBehaviour
{
    public bool changeSky = false;
    public Material newSky;
    public bool solidSky = false;
    public Color skyColor = Color.black;
    public bool fog = false;
    public Color fogColor = Color.black;
    public float fogDensity = 0.1f;
    public float rate = 0.1f;

    private bool changeDensity = false;
    private bool increase = false;
    private bool decrease = false;


    private void Update()
    {
        if (changeDensity) {
            if (increase) {
                RenderSettings.fogDensity += Time.deltaTime*rate;
                if (RenderSettings.fogDensity >= fogDensity) {
                    changeDensity = false;
                    increase = false;
                }
            }
            if (decrease)
            {
                RenderSettings.fogDensity -= Time.deltaTime*rate;
                if (RenderSettings.fogDensity <= fogDensity)
                {
                    changeDensity = false;
                    decrease = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (solidSky)
        {
            Camera.main.clearFlags = CameraClearFlags.SolidColor;
            Camera.main.backgroundColor = skyColor;
        }
        else
        {
            Camera.main.clearFlags = CameraClearFlags.Skybox;
            try { RenderSettings.skybox = newSky; }
            catch { }
        }
        RenderSettings.fog = fog;
        RenderSettings.fogColor = fogColor;
        if (RenderSettings.fogDensity != fogDensity) {
            if (RenderSettings.fogDensity < fogDensity)
            {
                increase = true;
                changeDensity = true;
            }
            else 
            {
                decrease = true;
                changeDensity = true;
            }
        }

    }
}
