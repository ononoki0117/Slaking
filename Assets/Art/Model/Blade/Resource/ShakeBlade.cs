using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBlade : MonoBehaviour
{
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        //Renderer renderer = gameObject.GetComponent<Renderer>();
        //Material mat = renderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        // 응원봉 입력 시
        if(Input.GetKeyDown(KeyCode.S))
        {
            animator.Play("Shake pink", -1, 0);
            animator.Play("Shake blue", -1, 0);
        }
    }
}
