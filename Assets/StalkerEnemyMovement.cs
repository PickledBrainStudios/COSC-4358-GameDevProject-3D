using UnityEngine;
using UnityEngine.AI;

public class StalkerEnemyMovement : MonoBehaviour
{
    public float movementSpeed = 1f;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    private GameObject player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        navMeshAgent.speed = movementSpeed;
    }

    void Update()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }
}
