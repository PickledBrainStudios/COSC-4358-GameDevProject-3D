using System.Collections;
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
        if (other.CompareTag("Player") && !activated)
        {
            StartCoroutine(DisplayImage());
        }
    }

    private IEnumerator DisplayImage()
    {
        playerManager.DeactivateControl();
        jumpScareImage.texture = imageTexture;
        jumpScareImage.enabled = true;

        activated = true;
        jumpScareAudio.PlayOneShot(jumpScareSound);

        Vector2 originalSizeDelta = jumpScareImage.rectTransform.sizeDelta;
        float textureAspect = (float)imageTexture.width / imageTexture.height;
        jumpScareImage.rectTransform.sizeDelta = new Vector2(jumpScareImage.rectTransform.sizeDelta.y * textureAspect, jumpScareImage.rectTransform.sizeDelta.y);

        yield return new WaitForSeconds(time);

        jumpScareImage.rectTransform.sizeDelta = originalSizeDelta;

        activated = false;
        jumpScareImage.enabled = false;
        playerManager.ActivateControl();
        Destroy(gameObject);
    }
}





