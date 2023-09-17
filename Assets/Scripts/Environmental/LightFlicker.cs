using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [Range(0, 100)]
    public float reliablilty = 75.0f;
    public float operationalTime = 10.0f;//stores baseline operational time

    [Range(0, 100)]
    public float lightLoss = 50.0f;
    public float speed = 15.0f;
    public float irregularity = 2.0f;

    private float functionTimer;//how long the light will operatate properly at
    private float malfunctionTimer;//how long the light will malfunction
    private float baseIntensity;
    private Light llight;
    private float fritzTimer;

    private void Awake()
    {
        functionTimer = operationalTime * reliablilty * 0.01f;
        malfunctionTimer = operationalTime * (100 - reliablilty) * 0.01f;
        fritzTimer = Random.Range(0, irregularity);
        llight = gameObject.GetComponent<Light>();
        baseIntensity = llight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (functionTimer >= 0)//check to see if there is operational/functioning time left 
        {
            functionTimer -= Time.deltaTime;
        }
        else //if there is no operational time left then the light is due to malfunction
        { 
            malfunctionTimer -= Time.deltaTime;
            fritzTimer -= Time.deltaTime * speed;//determines how quickly the glitch occurs
            if (fritzTimer <= 0)
            {
                llight.intensity = (baseIntensity - baseIntensity * Random.Range(0.0f, lightLoss) * 0.01f);
                fritzTimer = Random.Range(0, irregularity);
            }
            if (malfunctionTimer <= 0)//if malfucntion timer runs out then the malfunction stops 
            {
                llight.intensity = baseIntensity;//return light to regular intensity
                functionTimer = Random.Range(0, reliablilty) * 0.01f * operationalTime;//function timer is a function of the reliablity variable. The more reliable the more time
                malfunctionTimer = Random.Range(0, 100 - reliablilty) * 0.01f * operationalTime;//malfunction timer is an inverse function of reliabililty variable. The less reliable the more time spent malfunctioning
            }
        }
    }
}
