using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathroomPuzzle : MonoBehaviour
{
    public float timerTarget = 30f;
    public PhysicalDoor door;

    public GameObject[] activate;
    public GameObject[] destroy;

    private float timer = 0;

    private bool lightsOff = false;
    private bool testStarted = false;

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

        try
        {
            foreach (GameObject obj in destroy)
            {
                Destroy(obj);
            }
        }
        catch { }
        try
        {
            foreach (GameObject obj in activate)
            {
                obj.SetActive(true);
            }
        }
        catch { }

    }
    public void StartTest() {
        testStarted = true;
    }
}
