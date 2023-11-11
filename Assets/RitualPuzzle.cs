using UnityEngine;

public class RitualPuzzle : MonoBehaviour
{

    public int candleTarget = 5;
    private int candles = 0;
    private int matches = 0;
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

    public void CollectMatches() {
        matches++;
    }

    public void LightCandles() {
        candles++;
    }

    private void PuzzleComplete() { 
        
    }
}
