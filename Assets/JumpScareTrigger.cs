using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareTrigger : MonoBehaviour
{
    public float pause = 10.0f;
    public AudioClip jumpScareSound;

    private bool activated = false;
    private GameObject player;
    private PlayerManager playerManager;
    private AudioSource audioSource;
    private Animator enemyAnimator;
    private SkinnedMeshRenderer enemyMesh;

    


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
        enemyAnimator = GameObject.FindGameObjectWithTag("JumpScare").GetComponent<Animator>();
        audioSource = GameObject.FindGameObjectWithTag("JumpScare").GetComponent<AudioSource>();
        enemyMesh = GameObject.FindGameObjectWithTag("EnemyMesh").GetComponent<SkinnedMeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        audioSource.PlayOneShot(jumpScareSound);
        playerManager.ToggleControl();
        player.transform.Rotate(-player.transform.eulerAngles.x, 0.0f, 0.0f) ;
        activated = true;
        enemyMesh.enabled = true;
        enemyAnimator.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated) {
            pause -= Time.deltaTime;
        }
        if (pause <= 0) {
            playerManager.ToggleControl();
            Destroy(gameObject);
        }
    }
}
