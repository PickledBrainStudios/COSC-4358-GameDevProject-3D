using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour, IInteractable
{
    public float degree = 90f;
    public float speed = 0.5f;
    public AudioClip openClip;
    public AudioClip closeClip;
    private AudioSource audioSource;
    private bool isOpen = false;
    private bool coroutineRunning = false;

    private void Start()
    {
        audioSource = GameObject.FindWithTag("Player_AudioSource").GetComponent<AudioSource>();
    }

    public void Interact() {
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

}