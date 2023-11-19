using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScareTrigger : MonoBehaviour
{
    public float pause = 10.0f;
    public AudioClip jumpScareSound;
    public bool loadScene;

    private bool activated = false;
    private GameObject player;
    private PlayerManager playerManager;
    private AudioSource audioSource;
    private Animator enemyAnimator;
    private SkinnedMeshRenderer enemyMesh;
    private SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
        enemyAnimator = GameObject.FindGameObjectWithTag("JumpScare").GetComponent<Animator>();
        audioSource = GameObject.FindGameObjectWithTag("JumpScare").GetComponent<AudioSource>();
        enemyMesh = GameObject.FindGameObjectWithTag("EnemyMesh").GetComponent<SkinnedMeshRenderer>();
        sceneLoader = gameObject.GetComponent<SceneLoader>();
    }

    private void OnTriggerEnter(Collider other)
    {
        audioSource.PlayOneShot(jumpScareSound);
        playerManager.DeactivateControl();
        player.transform.Rotate(-player.transform.eulerAngles.x, 0.0f, 0.0f) ;
        activated = true;
        enemyMesh.enabled = true;
        enemyAnimator.enabled = true;
        gameObject.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated) {
            pause -= Time.deltaTime;
        }
        if (pause <= 0) {
            activated = false;
            enemyMesh.enabled = false;
            enemyAnimator.enabled = false;
            playerManager.ActivateControl();
            Destroy(gameObject);
            if (loadScene) {
                sceneLoader.LoadScene();
            }
        }
    }
}
