using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAnimate : MonoBehaviour
{
    private Animator animator;

    void start()
    {
    }
    void Update()
    {
        animator = this.GetComponent<Animator>();
        
        if (Input.GetKeyDown(KeyCode.D)) {
            animator.SetTrigger("DanceStart");
        }
        if (Input.GetKeyUp(KeyCode.D)) {
            animator.SetTrigger("DanceCancel");
        }

        if (Input.GetKeyDown(KeyCode.S)) {
            animator.SetTrigger("SnakeStart");
        }
        if (Input.GetKeyUp(KeyCode.S)) {
            animator.SetTrigger("SnakeCancel");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            animator.SetTrigger("RunStart");
        }
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            animator.SetTrigger("RunCancel");
        }
    }
}
