using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestory : MonoBehaviour
{
    [HideInInspector]
    public string objectID;

    private void Awake()
    {
        objectID = name + transform.position.ToString();
        for (int i = 0; i < Object.FindObjectsOfType<DontDestory>().Length; i++)
        {
            if (Object.FindObjectsOfType<DontDestory>()[i] != this)
            {
                if (Object.FindObjectsOfType<DontDestory>()[i].objectID == objectID)
                {
                    Destroy(gameObject);
                }
            }
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {

    }
}
