using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour, IInteractable
{
    public float degree = 90f;
    public float speed = 0.5f;
    private bool isOpen = false;
    private bool coroutineRunning = false;

    public void Interact() {
        if (isOpen && !coroutineRunning)
        {
            StartCoroutine(rotateObject(gameObject.transform.rotation, Quaternion.Euler(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y - degree, gameObject.transform.eulerAngles.z), speed));
            //gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, Quaternion.Euler(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y - degree, gameObject.transform.eulerAngles.z), speed);
            isOpen = false;
        }
        if (!isOpen && !coroutineRunning)
        {
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
