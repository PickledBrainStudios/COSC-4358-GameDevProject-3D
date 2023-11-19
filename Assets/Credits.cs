using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public RectTransform rect;

    public float scrollSpeed = 20f;

    public float startPosition;
    public float finalPosition;

    public GameObject menuButton;
    public RawImage fadeIn;
    public float fadeTimer = 5;
    public float fadeInSpeed = 1;

    private float y;
    private float realValue = 1;


    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        rect.anchoredPosition = new Vector3(0f, startPosition, 0f);
        y = startPosition;
    }

    private void Update()
    {
        if (fadeTimer > 0) {
            fadeTimer -= Time.deltaTime;
        }
        else if (realValue > 0)
        {
            realValue -= Time.deltaTime * fadeInSpeed;
            fadeIn.color = new Color(0f, 0f, 0f, realValue);
        }
        if (y < finalPosition && realValue <= 0)
        {
            y += Time.deltaTime * scrollSpeed;
            rect.anchoredPosition = new Vector3(0f, y, 0f);
        }
        else if (y >= finalPosition) 
        {
            menuButton.SetActive(true);
        }

    }
}
