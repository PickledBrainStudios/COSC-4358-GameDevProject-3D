using UnityEngine;
using UnityEngine.AI;

public class LineOfSight : MonoBehaviour
{
    public float maxSightDistance = 10f;
    public float fieldOfViewAngle = 45f;
    public float moveSpeed = 3f;
    public float chaseSpeed = 5f;
    public float chaseDuration = 10f; // Duration of chasing in seconds
    public float patrolCooldown = 5f; // Cooldown time before returning to patrolling
    public float searchingDuration = 2f; // Duration of searching for the player in seconds
    public float headHeight = 0.5f;


    private Transform player;
    private Vector3 raySource;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isChasing = false;
    private float chaseStartTime;
    private float lastSightingTime;
    //private Quaternion originalRotation; // Store the original rotation for looking around
    private bool isSearchingForPlayer = false;
    private float searchingStartTime;
    


    private EnemyPatrol patrol;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        patrol = GetComponent<EnemyPatrol>();
        animator = GetComponent<Animator>();
        //originalRotation = transform.rotation;
    }

    void Update()
    {

        //Debug.Log(navMeshAgent.velocity.magnitude);
        raySource = new Vector3(transform.position.x, transform.position.y + headHeight, transform.position.z);

        //Debug cast ray to show field of view
        // Calculate the half-extent of the wide ray
        float halfFOV = fieldOfViewAngle * 0.5f;
        Vector3 halfExtentsRight = Quaternion.Euler(0, halfFOV, 0) * transform.forward * maxSightDistance;
        Vector3 halfExtentsLeft = Quaternion.Euler(0, -halfFOV, 0) * transform.forward * maxSightDistance;

        // Draw a debug wide ray to visualize the field of view angle
        Debug.DrawRay(raySource, halfExtentsRight, Color.red);
        Debug.DrawRay(raySource, halfExtentsLeft, Color.red);



        if (navMeshAgent.velocity.magnitude > chaseSpeed - .5f)
        {
            animator.SetFloat("speed", 5f);
            //Debug.Log("Speed: " + animator.speed);
        }
        else if (navMeshAgent.velocity.magnitude > 0f)
        {
            animator.SetFloat("speed", 3f);
            //Debug.Log("Speed: " + animator.speed);
        }
        else 
        {
            animator.SetFloat("speed", 0f);
            //Debug.Log("Speed: " + animator.speed);
        }

        Vector3 directionToPlayer = player.position - transform.position;

        // Check if player is within maxSightDistance
        if (Vector3.Distance(transform.position, player.position) <= maxSightDistance)
        {
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            Debug.DrawRay(raySource, directionToPlayer.normalized * maxSightDistance, Color.blue);
            // Check if the player is within the field of view
            if (angleToPlayer <= fieldOfViewAngle * 0.5f)
            {
                RaycastHit hit;
                Physics.Raycast(raySource, directionToPlayer, out hit, maxSightDistance);
                Debug.Log(hit.collider.tag);

                // Check for obstacles between the enemy and the player
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.DrawRay(raySource, directionToPlayer.normalized * maxSightDistance, Color.green);
                    // Player is in line of sight
                    StartChasing();
                    lastSightingTime = Time.time;
                }
            }
        }
        else
        {
            Debug.DrawRay(raySource, directionToPlayer.normalized * maxSightDistance, Color.red);
        }

        if (isChasing)
        {
            // Update the chase destination to the player's position
            navMeshAgent.SetDestination(player.position);

            // Check if it's time to stop chasing and return to patrolling
            if (Time.time - chaseStartTime >= chaseDuration || Vector3.Distance(transform.position, player.position) > maxSightDistance)
            {
                StopChasing();
            }
        }
        else if (Time.time - lastSightingTime >= patrolCooldown)
        {
            // If not chasing and cooldown time has passed, return to patrolling
            if (!isSearchingForPlayer)
            {
                // Start searching for the player if not already searching
                StartSearchingForPlayer();
            }
            else if (Time.time - searchingStartTime >= searchingDuration)
            {
                // Stop searching for the player after the specified duration
                StopSearchingForPlayer();
                StopChasing();
            }
            // If not chasing and cooldown time has passed, return to patrolling
        }
    }

    public void StartChasing()
    {
        Debug.Log("Chase started");
        patrol.enabled = false;
        isChasing = true;
        navMeshAgent.speed = chaseSpeed;
        chaseStartTime = Time.time;
    }
    public void StopChasing()
    {
        patrol.enabled = true;
        isChasing = false;
        navMeshAgent.speed = patrol.patrolSpeed; // Reset speed to patrolling speed
        lastSightingTime = Time.time;
    }

    void StartSearchingForPlayer()
    {
        isSearchingForPlayer = true;
        searchingStartTime = Time.time;
        // You can add code to make the enemy search for the player here.
    }

    void StopSearchingForPlayer()
    {
        isSearchingForPlayer = false;
        // Reset the rotation to the original rotation after searching for the player.
        //transform.rotation = originalRotation;
    }
}