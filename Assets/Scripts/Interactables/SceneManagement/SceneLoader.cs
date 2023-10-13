using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public string level;
    public GameObject[] activateArray;
    public bool isReset = false;

    /*
    private void Awake()
    {
        XRGeneralSettings.Instance.Manager.InitializeLoader();
    }
    public void LoadVRScene()
    {
        SceneManager.LoadScene(VRSceneName); // Replace "YourVRSceneName" with the actual name of your VR scene.
    }

    public void Load3DScene()
    {
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        SceneManager.LoadScene(TraditionalSceneName); // Replace "Your3DSceneName" with the actual name of your 3D scene.
    }
    */
    public void LoadScene() {
        foreach (GameObject obj in activateArray) 
        {
            obj.SetActive(true);
        }
        if (isReset) 
        {
            Debug.Log("destroy!");
            foreach (GameObject o in Object.FindObjectsOfType<GameObject>())
            {
                Destroy(o);
            }
        }
        SceneManager.LoadScene(level);
    }
    public void CloseGame() {
        Application.Quit();
    }
}



