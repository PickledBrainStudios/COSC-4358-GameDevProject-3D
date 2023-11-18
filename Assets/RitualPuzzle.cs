using UnityEngine;

public class RitualPuzzle : MonoBehaviour
{

    public int candleTarget = 5;
    public int matches = 0;

    public GameObject[] activate;
    public GameObject[] destroy;

    private int candles = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (candles >= candleTarget) {
            PuzzleComplete();
        }
    }

    public void CollectMatches(int num) {
        matches += num;
    }

    public void LightCandles() {
        candles++;
        matches--;
    }

    private void PuzzleComplete() {
        Debug.Log("PUZZLE COMPLETE!");
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
