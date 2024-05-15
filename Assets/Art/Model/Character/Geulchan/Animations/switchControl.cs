using Mocopi.Receiver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchControl : MonoBehaviour
{
    public Animator animator;
    public RuntimeAnimatorController controller;
    public MocopiAvatar mocopi;
    // Start is called before the first frame update
    void Start()
    {
        mocopi = FindAnyObjectByType<MocopiAvatar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(animator.runtimeAnimatorController == null)
            {
                mocopi.enabled = false;
                animator.runtimeAnimatorController = controller;
                StartCoroutine(PlayAnimation(animator));
            }
            else
            {
                mocopi.enabled = true;
                animator.runtimeAnimatorController = null;
            }
        }
    }

    private IEnumerator PlayAnimation(Animator animator)
    {
        yield return new WaitForSeconds(2f);

        animator.SetTrigger("DanceTrigger");

        yield return null;
    }
}
