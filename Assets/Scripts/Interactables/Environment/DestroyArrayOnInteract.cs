using UnityEngine;

public class DestroyArrayOnInteract : MonoBehaviour, IInteractable
{
    public GameObject[] objectsToDestroy;

    // Update is called once per frame
    public void Interact()
    {
        foreach (GameObject obj in objectsToDestroy) 
        {
            Destroy(obj);
        }
        Destroy(gameObject);
    }
}
