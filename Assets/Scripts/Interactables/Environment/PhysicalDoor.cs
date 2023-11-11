using UnityEngine;
using TMPro;
using System.Collections;

public class PhysicalDoor : MonoBehaviour, IInteractable
{
    public float degree = 90f;
    public float speed = 0.5f;
    public AudioClip openClip;
    public AudioClip closeClip;
    private AudioSource audioSource;
    public bool isOpen = false;
    private bool coroutineRunning = false;

    public bool locked = false;

    public string lockedDialogue;

    public AudioClip lockedClip;

    public float dialogueTimer;

    private float timer;

    private bool informPlayer = false;

    private TextMeshProUGUI dialogueText;

    private void Start()
    {
        dialogueText = GameObject.FindWithTag("UI_DialogueBox").GetComponent<TextMeshProUGUI>();
        audioSource = GameObject.FindWithTag("Player_AudioSource").GetComponent<AudioSource>();
    }

    public void Interact() {
        if (locked)
        {
            audioSource.PlayOneShot(lockedClip);
            dialogueText.text = "";
            dialogueText.text = lockedDialogue;
            informPlayer = true;
            timer = dialogueTimer;
            //tell the player the door is locked but don't lock them in place
        }
        else
        {
            if (isOpen && !coroutineRunning) //if the door is open and the coroutine isn't running, rotate back to closed position
            {
                try { audioSource.PlayOneShot(closeClip); } //play the close door audio
                catch { }
                StartCoroutine(rotateObject(gameObject.transform.rotation, Quaternion.Euler(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y - degree, gameObject.transform.eulerAngles.z), speed));
                //gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y - degree, gameObject.transform.eulerAngles.z), speed);
                isOpen = false;
            }
            if (!isOpen && !coroutineRunning) //if the door is closed and the coroutine isn't running, rotate back to open position
            {
                try { audioSource.PlayOneShot(openClip); } //play the open door audio
                catch { }
                StartCoroutine(rotateObject(gameObject.transform.rotation, Quaternion.Euler(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y + degree, gameObject.transform.eulerAngles.z), speed));
                //gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y + degree, gameObject.transform.eulerAngles.z), speed);
                isOpen = true;
            }
        }
    }

    private IEnumerator rotateObject(Quaternion source, Quaternion target, float overTime)
    {
        coroutineRunning = true;
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            gameObject.transform.rotation = Quaternion.Slerp(source, target, (Time.time - startTime) / overTime);
            yield return null;
        }
        transform.rotation = target;
        coroutineRunning = false;
    }

    private void Update()
    {
        if (informPlayer)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                dialogueText.text = "";
                informPlayer = false;
            }
        }
    }

    public void LockDoor()
    {
        locked = true;
    }

    public void UnlockDoor()
    {
        locked = false;
    }

    public void CloseDoor() {
        if (isOpen && !coroutineRunning) {
            try { audioSource.PlayOneShot(closeClip); } //play the close door audio
            catch { }
            StartCoroutine(rotateObject(gameObject.transform.rotation, Quaternion.Euler(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y - degree, gameObject.transform.eulerAngles.z), speed));
            //gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y - degree, gameObject.transform.eulerAngles.z), speed);
            isOpen = false;
        }
    }

    public void OpenDoor() {
        if (!isOpen && !coroutineRunning) {
            try { audioSource.PlayOneShot(openClip); } //play the open door audio
            catch { }
            StartCoroutine(rotateObject(gameObject.transform.rotation, Quaternion.Euler(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y + degree, gameObject.transform.eulerAngles.z), speed));
            //gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y + degree, gameObject.transform.eulerAngles.z), speed);
            isOpen = true;
        }
    }

}
