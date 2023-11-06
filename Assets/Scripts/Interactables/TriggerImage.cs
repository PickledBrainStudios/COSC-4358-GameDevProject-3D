using UnityEngine;
using UnityEngine.UI;

public class TriggerImage : MonoBehaviour
{
    public Texture2D imageTexture;
    public AudioClip jumpScareSound;
    public float time = 10f;
    private GameObject player;
    private PlayerManager playerManager;
    private RawImage jumpScareImage;
    private AudioSource jumpScareAudio;
    private bool activated = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
        jumpScareImage = GameObject.FindGameObjectWithTag("UI_JumpScare_Raw").GetComponent<RawImage>();
        jumpScareAudio = GameObject.FindGameObjectWithTag("Player_AudioSource").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerManager.DeactivateControl();
        jumpScareImage.texture = imageTexture;
        jumpScareImage.enabled = true;
        activated = true;
        jumpScareAudio.PlayOneShot(jumpScareSound);
    }

    void Update()
    {
        if (activated) {
            time -= Time.deltaTime;
        }
        if (time <= 0) {
            activated = false;
            jumpScareImage.enabled = false;
            playerManager.ActivateControl();
            Destroy(gameObject);
        }
    }
}





