using UnityEngine;

public class ToggleObjects : MonoBehaviour, IInteractable
{
    //This script is for like a light switch. Add all the objs to the public array and when player interacts with the switch it will flip all the objs in the array active status

    public GameObject[] gameObjs;
    public void Interact()
    {
        foreach (GameObject obj in gameObjs) {
            obj.SetActive(!obj.activeSelf);
        }
    }
}
