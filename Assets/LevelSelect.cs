using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject levelSelectUI;

    public void ToggleLevelSelect() { 
        mainUI.SetActive(!mainUI.activeSelf);
        levelSelectUI.SetActive(!levelSelectUI.activeSelf);
    }
}
