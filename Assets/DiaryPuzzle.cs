using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryPuzzle : MonoBehaviour
{

    public int pages = 0;
    public int totalPages = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pages >= totalPages) {
            PuzzleComplete();
        }
    }

    public void PickUpPage() {
        pages++;
    }

    private void PuzzleComplete() { 
    
    }

}
