using UnityEngine;

public class QTE01 : MonoBehaviour, QQuickTimeEvent
{
    //public float damageMultiplyer = 1f;
    public float qteTimeLimit = 3.0f;
    public int pressPenalty = 2;
    public int failPenalty = 10;
    public string[] qteSequence = { "Space", "C", "Space" }; // Example QTE sequence

    private bool qteActive = false;
    private int currentQTEIndex = 0;
    private float qteTimer = 0.0f;

    private GameObject player;
    private PlayerManager playerManager;

    void Update()
    {
        if (qteActive)
        {
            qteTimer -= Time.deltaTime;//cound down the timer based on the frame modified time
            //playerScript.health -= Time.deltaTime * damageMultiplyer;
            //Debug.Log("Health:" + Mathf.Round(playerManager.health));


            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))//if multiple buttons have been pressed, they are placed into an array that will be looped through
            {
                if (Input.GetKeyDown(keyCode))//will return true if input is matching to the keyCode recoreded in the active array
                {
                    CheckInput(keyCode.ToString());//check the current keyCode with the check input function
                }
            }

            if (qteTimer <= 0 && qteActive)//a check to see if timer has run out
            {
                // QTE time limit exceeded
                Debug.Log("QTE FAILED!");
                playerManager.health -= failPenalty;
                //Debug.Log("Health:" + Mathf.Round(playerManager.health));
                ResetQTE();
            }
        }
    }

    public void ActivateQTE()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<PlayerManager>();
        playerManager.ToggleControl();
        qteActive = true;
        currentQTEIndex = 0;
        qteTimer = qteTimeLimit;
        Debug.Log("QTE START!");
    }

    private void ResetQTE()
    {
        qteActive = false;
        currentQTEIndex = 0;
        qteTimer = 0.0f;
        playerManager.ToggleControl();
    }

    public void StopQTE01()
    {
        qteActive = false;
        currentQTEIndex = 0;
        qteTimer = 0.0f;
    }

    private void CheckInput(string input)
    {
        Debug.Log(input);
        if (qteActive)
        {
            if (input == qteSequence[currentQTEIndex])
            {
                currentQTEIndex++;

                if (currentQTEIndex >= qteSequence.Length)
                {
                    // Successfully completed the QTE
                    Debug.Log("QTE COMPLETE!");
                    //Debug.Log("Health:" + Mathf.Round(playerManager.health));
                    ResetQTE();
                }
            }
            else
            {
                // Incorrect input during QTE
                Debug.Log("WRONG INPUT!");
                playerManager.health -= pressPenalty;
                //Debug.Log("Health:" + Mathf.Round(playerManager.health));
                //ResetQTE();
            }
        }
    }

}
