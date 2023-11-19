using UnityEngine;
using UnityEngine.SceneManagement;

public class TheEnd : MonoBehaviour
{

    public float timer = 1;
    public string nextScene;
    public SkinnedMeshRenderer movingMesh;
    public AudioClip jumpScareClip;

    private GameObject player;
    private PlayerManager playerManager;
    private SkinnedMeshRenderer dadMesh;
    private AudioSource audioSource;
    private float tempTimer = 1;
    private bool primer = false;


    // Start is called before the first frame update
    void Start()
    {
        dadMesh = GameObject.FindGameObjectWithTag("JumpScare_dad").GetComponent<SkinnedMeshRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");//find player
        playerManager = player.GetComponent<PlayerManager>();//find playerManager Script
        audioSource = GameObject.FindGameObjectWithTag("Player_AudioSource_02").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (primer) {
            tempTimer -= Time.deltaTime;
        }
        if (tempTimer <= 0) {
            Destroy(player);
            SceneManager.LoadScene(nextScene);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        audioSource.PlayOneShot(jumpScareClip);
        playerManager.DeactivateControl();
        player.transform.Rotate(-player.transform.eulerAngles.x, 0.0f, 0.0f);
        movingMesh.enabled = false;
        dadMesh.enabled = true;
        gameObject.GetComponent<Collider>().enabled = false;
        tempTimer = timer;
        primer = true;
    }
}
