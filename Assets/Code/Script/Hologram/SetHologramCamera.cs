using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHologramCamera : MonoBehaviour
{
    [Header("Target Object")]
    [SerializeField] private GameObject TitleLogo;
    [SerializeField] private GameObject Charactor;
    
    public bool isTarget = false;
    private GameObject targetObject;
    
    
    public Vector3 offset;
    [Range(0f, 1f)] public float smoothness;
    
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        GameManager.ToWearing += delegate () { targetObject = TitleLogo; };

        GameManager.ToSkip += delegate () { targetObject = Charactor; };

        targetObject = TitleLogo;
    }

    private void Start()
    {
        if (targetObject != null)
        {
            isTarget = true;
        }
    }

    void Update()
    {
        if (isTarget)
        {
            Vector3 targetPosition = targetObject.transform.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothness);
        }
    }
}
