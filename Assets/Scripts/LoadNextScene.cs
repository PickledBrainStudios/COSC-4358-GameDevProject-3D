using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public string nextScene;

    private void Start()
    {
        SceneManager.LoadScene(nextScene);
    }
}
