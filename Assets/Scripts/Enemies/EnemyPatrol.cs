using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour
{
    public float patrolSpeed = 1f;
    public Transform[] waypoints;
    private int currentWaypoint = 0;
    private NavMeshAgent navMeshAgent;


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = patrolSpeed;
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