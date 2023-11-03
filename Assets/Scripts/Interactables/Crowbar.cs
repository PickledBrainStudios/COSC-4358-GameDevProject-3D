using UnityEngine;

public class Crowbar : MonoBehaviour, IInteractable
{
    public AudioClip pickUpClip;
    private LevelManager02 levelManager;
    private AudioSource playerSFX;

    private void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager02").GetComponent<LevelManager02>();
    }

    public void Interact()
    {
        playerSFX = GameObject.FindGameObjectWithTag("Player_AudioSource_02").GetComponent<AudioSource>();
        levelManager.PickUpCrowbar();
        playerSFX.PlayOneShot(pickUpClip);
        Destroy(gameObject);
    }
}