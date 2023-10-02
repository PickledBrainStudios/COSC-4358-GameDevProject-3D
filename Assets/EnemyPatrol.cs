using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float patrolSpeed = 3f;
    private int currentWaypoint = 0;
    private NavMeshAgent navMeshAgent;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Initialize the NavMeshAgent to start patrolling
        SetNextWaypoint();
    }

    void SetNextWaypoint()
    {
        if (waypoints.Length > 0)
        {
            navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
        }
    }

    void Update()
    {
        // Check if the enemy has reached the current waypoint
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            SetNextWaypoint();
        }
    }
}