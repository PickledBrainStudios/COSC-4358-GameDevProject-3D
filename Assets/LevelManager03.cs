using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelManager03 : MonoBehaviour
{

    public bool hasShovel = false;
    public GameObject doorDestroy;
    public GameObject doorActivate;
    public GameObject crow;
    public GameObject enemy;
    public float timer = 3f;
    public int totalSouls = 10;
    private int souls = 0;
    private TextMeshProUGUI topRightText;
    
    private float counter;

    private void Start()
    {
        topRightText = GameObject.FindGameObjectWithTag("UI_TopRight").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (counter > 0f)
        {
            counter -= Time.deltaTime;
        }
        else
        {
            topRightText.alpha -= .5f * Time.deltaTime;
        }
        if (souls >= totalSouls) {
            Destroy(doorDestroy);
            doorActivate.SetActive(true);
            Destroy(crow);
        }
        if (souls == 5) {
            enemy.SetActive(true);
        }
    }

    public void PickUpShovel() {
        hasShovel = true;
    }

    public void CollectSoul() {
        counter = timer;
        souls++;
        topRightText.text = souls.ToString() + "/" + totalSouls.ToString();
        topRightText.alpha = 1f;
    }

    public void StartSearch() {
        topRightText.text = souls.ToString() + "/" + totalSouls.ToString();
        topRightText.alpha = 1f;
        counter = timer;
    }
}
