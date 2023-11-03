using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSearch : MonoBehaviour
{
    public LevelManager03 levelManager;
    private void OnTriggerExit(Collider other)
    {
        levelManager.StartSearch();
        Destroy(gameObject);
    }
}
