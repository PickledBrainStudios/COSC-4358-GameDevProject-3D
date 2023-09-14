using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    public string nextScene;
    public void Interact() {
        //Debug.Log("LET ME IN!!! LET ME INNNNNN!!!");
        SceneManager.LoadScene(nextScene);
    }
}
