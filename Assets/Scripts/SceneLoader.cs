using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    public string level01;
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
    public void StartGame() {
        SceneManager.LoadScene(level01);
    }
    public void CloseGame() {
        Application.Quit();
    }
}



