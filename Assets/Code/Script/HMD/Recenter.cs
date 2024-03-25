using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recenter : MonoBehaviour
{
    [SerializeField] Transform resetTransform;
    [SerializeField] GameObject player;
    [SerializeField] Camera playerHead;

    [ContextMenu("Reset Position")]
    public void ResetPosition()
    {
        var rotationAngleY = resetTransform.rotation.eulerAngles.y - playerHead.transform.rotation.eulerAngles.y;
        player.transform.Rotate(0, rotationAngleY, 0);

        var distanceDiff = resetTransform.position - playerHead.transform.position;
        player.transform.position += distanceDiff;
        Debug.Log("HMD position Reset");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPosition();
        }
    }
}
