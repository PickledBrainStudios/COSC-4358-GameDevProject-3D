using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour, IInteractable
{
    public LevelManager03 levelManager;
    // Start is called before the first frame update

    public void Interact() {
        levelManager.PickUpShovel();
        Destroy(gameObject);
    }
}
