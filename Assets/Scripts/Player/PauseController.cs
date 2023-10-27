using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject userInterfaceMain;
    private PlayerManager playerManager;
    public bool readingNote = false;
    //private Look look;
    public bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        playerManager = gameObject.GetComponent<PlayerManager>();
        //look = gameObject.GetComponent<Look>();
        //userInterfaceMain = GameObject.FindGameObjectWithTag("UI_main");
        //pauseMenu = GameObject.FindGameObjectWithTag("UI_Pause_main");
    }

    // Update is called once per frame
    void Update()
    {
        if (!readingNote)
        {
            //Debug.Log(paused);
            if (Input.GetKeyDown(KeyCode.Escape) && !paused)
            {
                PauseGame();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && paused)
            {
                UnpauseGame();
            }
            if (paused)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
    public void PauseGame() 
    {
        Camera.main.GetComponent<AudioListener>().enabled = false;
        Time.timeScale = 0;
        paused = true;
        playerManager.DeactivateControl();
        userInterfaceMain.SetActive(!userInterfaceMain.activeSelf);
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void UnpauseGame() 
    {
        Camera.main.GetComponent<AudioListener>().enabled = true;
        Time.timeScale = 1;
        paused = false;
        playerManager.ActivateControl();
        userInterfaceMain.SetActive(!userInterfaceMain.activeSelf);
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }
}
