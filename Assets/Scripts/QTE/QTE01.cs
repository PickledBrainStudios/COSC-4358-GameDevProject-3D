using UnityEngine;
using TMPro;
using UnityEngine.AI;


public class QTE01 : MonoBehaviour, QQuickTimeEvent
{
    //public float damageMultiplyer = 1f;
    public float qteTimeLimit = 3.0f; //How long the player has to complete the QTE
    public int pressPenalty = 2;
    public int failPenalty = 10;
    public string[] qteSequence = { "Space", "C", "Space" }; // Example QTE sequence
    public float stunDuration = 5f;
    
    private bool qteActive = false;
    private bool isStunned = false;
    private int currentQTEIndex = 0;
    private float qteTimer = 0.0f;
    private float temp;

    private GameObject player;
    private PlayerManager playerManager;
    private TextMeshProUGUI prompt;

    private LineOfSight lineOfSight;
    private EnemyPatrol enemyPatrol;
    private NavMeshAgent navMeshAgent;

    public AudioClip jumpScareSound;
    private AudioSource audioSource;
    private Animator enemyAnimator;
    private SkinnedMeshRenderer enemyMesh;
    public SkinnedMeshRenderer mesh;

    void Update()
    {
        if (qteActive)
        {
            qteTimer -= Time.deltaTime;//cound down the timer based on the frame modified time
            //playerScript.health -= Time.deltaTime * damageMultiplyer;
            //Debug.Log("Health:" + Mathf.Round(playerManager.health));

            prompt.text = qteSequence[currentQTEIndex];

            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))//if multiple buttons have been pressed, they are placed into an array that will be looped through
            {
                if (Input.GetKeyDown(keyCode))//will return true if input is matching to the keyCode recoreded in the active array
                {
                    CheckInput(keyCode.ToString());//check the current keyCode with the check input function
                }
            }

            if (qteTimer <= 0 && qteActive)//a check to see if timer has run out
            {
                // QTE time limit exceeded
                Debug.Log("QTE FAILED!");
                playerManager.health -= failPenalty;
                //Debug.Log("Health:" + Mathf.Round(playerManager.health));
                ResetQTE();
            }
        }
        else if (isStunned && !qteActive)
        {
            temp -= Time.deltaTime;
            if (temp <= 0f)
            {
                isStunned = false;
                ResumePatrol();
            }
        }
    }

    public void ActivateQTE()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
        lineOfSight = gameObject.GetComponent<LineOfSight>();
        enemyPatrol = gameObject.GetComponent<EnemyPatrol>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        prompt = GameObject.FindWithTag("UI_QTE").GetComponent<TextMeshProUGUI>();
        enemyAnimator = GameObject.FindGameObjectWithTag("JumpScare_Enemy01").GetComponent<Animator>();
        audioSource = GameObject.FindGameObjectWithTag("JumpScare_Enemy01").GetComponent<AudioSource>();
        enemyMesh = GameObject.FindGameObjectWithTag("Enemy01Mesh").GetComponent<SkinnedMeshRenderer>();

        audioSource.PlayOneShot(jumpScareSound);
        playerManager.DeactivateControl();
        player.transform.LookAt(transform);
        player.transform.Rotate(-player.transform.eulerAngles.x, 0.0f, 0.0f);
        mesh.enabled = false;
        enemyMesh.enabled = true;
        enemyAnimator.enabled = true;
        lineOfSight.StopChasing();
        lineOfSight.enabled = false;
        enemyPatrol.enabled = false;
        //navMeshAgent.updatePosition = false;
        navMeshAgent.velocity = Vector3.zero;
        navMeshAgent.isStopped = true;

        qteActive = true;
        currentQTEIndex = 0;
        qteTimer = qteTimeLimit;
        Debug.Log("QTE START!");
    }

    private void ResetQTE()
    {
        isStunned = true;
        qteActive = false;
        prompt.text = "";
        currentQTEIndex = 0;
        qteTimer = 0.0f;
        temp = stunDuration;
        mesh.enabled = true;
        enemyMesh.enabled = false;
        enemyAnimator.enabled = false;
        playerManager.ActivateControl();
    }

    private void ResumePatrol() 
    {
        //navMeshAgent.updatePosition = true;
        navMeshAgent.isStopped = false;
        lineOfSight.enabled = true;
        enemyPatrol.enabled = true;
    }

    private void CheckInput(string input)
    {
        Debug.Log(input);
        if (qteActive)
        {
            if (input == qteSequence[currentQTEIndex])
            {
                currentQTEIndex++;

                if (currentQTEIndex >= qteSequence.Length)
                {
                    // Successfully completed the QTE
                    Debug.Log("QTE COMPLETE!");
                    //Debug.Log("Health:" + Mathf.Round(playerManager.health));
                    ResetQTE();
                }
            }
            else
            {
                // Incorrect input during QTE
                Debug.Log("WRONG INPUT!");
                playerManager.health -= pressPenalty;
                currentQTEIndex = 0;
                //Debug.Log("Health:" + Mathf.Round(playerManager.health));
                //ResetQTE();
            }
        }
    }
}
