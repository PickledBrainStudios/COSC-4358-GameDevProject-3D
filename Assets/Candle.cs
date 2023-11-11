using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour, IInteractable
{

    public Material candleOnMat;
    public Material candleOffMat;

    private bool lit = false;

    private Renderer candleRenderer;


    // Start is called before the first frame update
    void Start()
    {
        candleRenderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact() {
        ToggleLight();
        if (lit)
        {
            candleRenderer.material = candleOffMat;
        }
        else {
            candleRenderer.material = candleOnMat;
        }
    }

    public void ToggleLight() {
        lit = !lit;
    }
}
