using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDemo : MonoBehaviour, IInteractable
{
    private GameObject player;
    //On interact
    public void Interact()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Destroy(player);
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("EndScene");//load next scene
    }
}
