using UnityEngine;

public class LevelManager02 : MonoBehaviour
{
    //Three generators that all need to be activated to unlock the final door

    public int numberOfPlanks= 4;
    public bool hasCrowbar = false;
    public GameObject[] toActivate;
    public Door[] doors;
    public GameObject[] toDestory;
    

    private void Update()
    {
        if (numberOfPlanks <= 0) {
            foreach (Door obj in doors)
            {
                obj.UnlockDoor();
            }
            foreach (GameObject obj in toActivate) {
                obj.SetActive(true);
            }
            foreach (GameObject obj in toDestory)
            {
                Destroy(obj);
            }
        }
    }

    public void PlankRemoved() 
    {
        numberOfPlanks--;
        Debug.Log("Planks Left: " + numberOfPlanks);
    }

    public void PickUpCrowbar() 
    {
        Debug.Log("I have the crow bar!");
        hasCrowbar = true;
    }
}
