using UnityEngine;

public class DiaryPuzzle : MonoBehaviour
{

    public int pages = 0;
    public int totalPages = 5;

    public GameObject[] activate;
    public GameObject[] destroy;

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(pages);
        if (pages >= totalPages) {
            PuzzleComplete();
        }
    }

    public void PickUpPage() {
        pages++;
    }

    private void PuzzleComplete() {
        try
        {
            foreach (GameObject obj in destroy)
            {
                Destroy(obj);
            }
        }
        catch { }
        try
        {
            foreach (GameObject obj in activate)
            {
                obj.SetActive(true);
            }
        }
        catch { }
    }

}
