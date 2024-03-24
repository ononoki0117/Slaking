using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI TargetText;
    public string[] DialogLines;
    public int currentIndex;
    void Start()
    {
        if (DialogLines.Length > 0)
        {
            TargetText.text = DialogLines[0];
            currentIndex = 0;
        }

        Debug.Log(DialogLines.Length);
        Debug.Log(TargetText.text);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space) && currentIndex + 1 < DialogLines.Length)
        {
            currentIndex++;
            TargetText.text = DialogLines[currentIndex];
        }
    }
}
