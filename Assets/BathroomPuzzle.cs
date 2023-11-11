using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomPuzzle : MonoBehaviour
{

    public float timerTarget = 30f;
    public PhysicalDoor door;
    private float timer = 0;

    private bool lightsOff = false;
    private bool testStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timer);
        if (lightsOff && testStarted)
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
        Debug.Log("COMPLETE!");
        testStarted = false;
        door.UnlockDoor();
    }
    public void StartTest() {
        testStarted = true;
    }
}
