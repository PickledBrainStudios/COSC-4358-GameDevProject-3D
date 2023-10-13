using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToObject : MonoBehaviour
{
    public bool goToPlayer = false; //will overwrite what ever designer writes in destination
    public GameObject destination;
    public float speed = 5.0f;
    private Vector3 direction;

    private void Start()
    {
        if (goToPlayer) {
            destination = GameObject.FindGameObjectWithTag("Player");//find player
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != destination.transform.position)
        {
            // Calculate the direction from this GameObject to the player.
            direction = destination.transform.position - transform.position;

            // Normalize the direction vector to make the movement smooth.
            direction.Normalize();

            // Move the GameObject towards the player.
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
