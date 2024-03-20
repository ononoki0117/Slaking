using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PressAnyKeyToStart : MonoBehaviour
{
    [SerializeField][Range (0.5f, 2.5f)]
    private float breathSpeed = 1.5f;
    private TextMeshProUGUI text;
    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    void Update()
    {
        if (Input.anyKeyDown)
        {
            LoadingManager.LoadScene("Wearing");
        }
        else
        {
            var alpha = (byte)((Mathf.Sin(Time.time * breathSpeed) + 1) * 128);
            text.color = new Color32((byte)((int)text.color.r * 256), (byte)((int)text.color.g * 256), (byte)(text.color.b*256), alpha);
        }
    }

}
