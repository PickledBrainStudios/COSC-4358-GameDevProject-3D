using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine;

public class CopyUI : MonoBehaviour
{
    public TextMeshProUGUI topRightText;
    // Start is called before the first frame update
    private void OnEnable()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = topRightText.text;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = topRightText.text;
    }
}
