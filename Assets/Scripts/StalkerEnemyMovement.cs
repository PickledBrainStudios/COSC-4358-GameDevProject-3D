using UnityEngine;
using UnityEngine.AI;

public class StalkerEnemyMovement : MonoBehaviour
{
    public float movementSpeed = 1f;
    public Transform startingPosition;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private GameObject player;
    public bool safeZoned = false;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.speed = movementSpeed;
    }

    void Update()
    {
        if (!safeZoned)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
        else
        {
            navMeshAgent.SetDestination(startingPosition.position);
        }
    }
}
