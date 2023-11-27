using UnityEngine;

public class LightSwitchPuzzle : MonoBehaviour, IInteractable
{
    //This script is for like a light switch. Add all the objs to the public array and when player interacts with the switch it will flip all the objs in the array active status

    public GameObject[] gameObjs;
    public BathroomPuzzle bathroomManager;
    public PhysicalDoor door;

    public AudioSource audioSource;
    public AudioClip audioClip;

    private bool active = true;

    public void Interact()
    {
        active = !active;
        foreach (GameObject obj in gameObjs)
        {
            obj.SetActive(!obj.activeSelf);
        }
        bathroomManager.ToggleLight();
        bathroomManager.StartTest();

        if (!door.isOpen)
        {
            door.LockDoor();
        }
        else {
            door.CloseDoor();
            door.LockDoor();
        }
        if (!active)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(audioClip);
        }
        else 
        {
            audioSource.Stop();
        }
    }
}
