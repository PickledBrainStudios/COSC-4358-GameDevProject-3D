using UnityEngine;
using UnityEngine.SceneManagement;


public class OnCollideLoadScene : MonoBehaviour
{
    public string nextScene;
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(nextScene);
    }
}
