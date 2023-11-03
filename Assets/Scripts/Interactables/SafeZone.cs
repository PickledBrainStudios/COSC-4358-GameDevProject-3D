using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public StalkerEnemyMovement enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        enemy.safeZoned = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        enemy.safeZoned = true;
    }

    private void OnTriggerStay(Collider other)
    {
        enemy.safeZoned = true;
    }

}
