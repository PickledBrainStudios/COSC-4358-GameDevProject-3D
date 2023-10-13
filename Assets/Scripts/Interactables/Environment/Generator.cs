using UnityEngine;

public class Generator : MonoBehaviour, IInteractable
{
    public GameObject[] toActivate;
    public GameObject[] toUnlock;
    public GameObject[] toPower;
    private LevelManager levelManager;
    // Start is called before the first frame update
    public void Interact()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        levelManager.generatorActivated();
        foreach (GameObject obj in toUnlock) {
            obj.GetComponent<OpenDoor>().UnlockDoor();
        }
        foreach (GameObject obj in toActivate) {
            obj.SetActive(true);
        }
        foreach (GameObject obj in toPower)
        {
            obj.GetComponent<ToggleObjects>().powerOn();
        }
        Destroy(this);
    }
}
