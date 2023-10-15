using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnUnlock : MonoBehaviour
{
    public PhysicalDoor door;

    private Light llight;
    private Material mat;
    // Start is called before the first frame update
    void Start()
    {
        llight = GetComponent<Light>();
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (door.locked == false) {
            llight.color = Color.green;
            mat.SetColor("_EmissionColor", Color.green);
            mat.color = Color.green;
            Destroy(this);
        }
    }
}
