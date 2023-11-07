using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomPuzzle : MonoBehaviour
{

    public float timerTarget = 30f;
    private float timer = 0;

    private bool lightsOff = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lightsOff)
        {
            timer += Time.deltaTime;
        }
        else {
            timer = 0f;
        }
        if (timer >= timerTarget) {
            PuzzleComplete();
        }
    }

    public void ToggleLight() {
        lightsOff = !lightsOff;
    }

    private void PuzzleComplete() { 
        
    }
}
