using UnityEngine;
using UnityEngine.AI;

public class StalkerEnemyDamage : MonoBehaviour
{
    public float damage = 2f;
    public float maxSightDistance = 10f;
    public float headHeight = 0.5f;


    private Transform player;
    private PlayerManager playerManager;
    private Vector3 raySource;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerManager = player.GetComponent<PlayerManager>();
    }

    void Update()
    {

        //Debug.Log(navMeshAgent.velocity.magnitude);
        raySource = new Vector3(transform.position.x, transform.position.y + headHeight, transform.position.z);
        
        Vector3 directionToPlayer = player.position - transform.position;

        Debug.DrawRay(raySource, directionToPlayer.normalized * maxSightDistance, Color.red);

        // Check if player is within maxSightDistance
        if (Vector3.Distance(transform.position, player.position) <= maxSightDistance)
        {

            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            Debug.DrawRay(raySource, directionToPlayer.normalized * maxSightDistance, Color.blue);
            // Check if the player is within the field of view
            RaycastHit hit;
            Physics.Raycast(raySource, directionToPlayer, out hit, maxSightDistance);
            Debug.Log(hit.collider.tag);

            // Check for obstacles between the enemy and the player
            if (hit.collider.CompareTag("Player"))
            {
                Debug.DrawRay(raySource, directionToPlayer.normalized * maxSightDistance, Color.green);
                playerManager.health -= (damage/ Vector3.Distance(transform.position, player.position)) * Time.deltaTime;
            }
        }
    }


}
