using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Three generators that all need to be activated to unlock the final door

    public int numberOfSwitches = 3;
    public GameObject[] toActivate;
    public Door[] doors;
    public OpenDoor[] openDoors;
    public GameObject[] toDestory;

    private int activeCount = 0;

    private void Update()
    {
        if (activeCount == numberOfSwitches) {
            foreach (Door obj in doors)
            {
                obj.UnlockDoor();
            }
            foreach (OpenDoor obj in openDoors)
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

    public void generatorActivated() 
    {
        activeCount++;
        Debug.Log("Activated: " + activeCount);
    }
}
