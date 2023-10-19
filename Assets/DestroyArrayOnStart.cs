using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArrayOnStart : MonoBehaviour
{
    public string[] objectsToDestroy;

    // Update is called once per frame
    private void Start()
    {
        foreach (string obj in objectsToDestroy)
        {
            try { Destroy(GameObject.Find(obj)); }
            catch { }
        }
        Destroy(gameObject);
    }
}
