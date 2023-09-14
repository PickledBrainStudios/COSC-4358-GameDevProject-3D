using UnityEngine;

public class ToggleObjects : MonoBehaviour, IInteractable
{
    public GameObject[] gameObjs;
    public void Interact()
    {
        foreach (GameObject obj in gameObjs) {
            obj.SetActive(!obj.activeSelf);
        }
    }
}
