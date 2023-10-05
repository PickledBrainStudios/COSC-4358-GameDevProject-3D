using UnityEngine;

public class Generator : MonoBehaviour, IInteractable
{
    public GameObject[] toActivate;
    public GameObject[] toUnlock;
    // Start is called before the first frame update
    public void Interact()
    {
        foreach (GameObject obj in toUnlock) {
            obj.GetComponent<OpenDoor>().UnlockDoor();
        }
        foreach (GameObject obj in toActivate) {
            obj.SetActive(true);
        }
    }
}
