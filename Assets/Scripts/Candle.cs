using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour, IInteractable
{

    public Material candleOnMat;
    public Material candleOffMat;

    private bool lit = false;

    private Renderer candleRenderer;
    new private Light light;

    public RitualPuzzle ritual;


    // Start is called before the first frame update
    void Start()
    {
        candleRenderer = gameObject.GetComponent<Renderer>();
        light = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact() {
        if (ritual.matches > 0)
        {
            if (lit)
            {
                candleRenderer.material = candleOffMat;
                light.enabled = false;
            }
            else
            {
                candleRenderer.material = candleOnMat;
                light.enabled = true;
            }
            ToggleLight();
            ritual.LightCandles();
            Destroy(this);
        }
        else 
        {
            Debug.Log("You dont have enough matches...");
        }
    }

    public void ToggleLight() {
        lit = !lit;
    }
}
