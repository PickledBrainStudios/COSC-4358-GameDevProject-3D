using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matches : MonoBehaviour, IInteractable
{

    public int numberOfMatches = 5;
    public RitualPuzzle ritual;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact() {
        ritual.CollectMatches(numberOfMatches);
        Destroy(gameObject);
    }
}
