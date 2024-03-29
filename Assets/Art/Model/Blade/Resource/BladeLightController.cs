using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeLightController : MonoBehaviour
{
    public Animator animator;
    public Material mat;

    // Start is called before the first frame update
    //void Start()
    //{
    //    Renderer renderer = gameObject.GetComponent<Renderer>();
    //    Material mat = renderer.material; 
    //}

    // Update is called once per frame
    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Anim_Shake")==true) {
            float animTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if(animTime == 0) {
                OffBladeLight();
            }//흔드는 애니메이션 실행 중이 아닐때
            if (animTime > 0 && animTime < 1.0f) {
                OnBladeLight();
            }//애니메이션 실행 중
            if (animTime >= 1.0f) {
                OffBladeLight();
            }//애니메이션 종료

        }
    }

    void OnBladeLight() {
        mat.SetColor("_EmissionColor", new Color(255,81,199)*0.1f);
    }

    void OffBladeLight() {
        mat.SetColor("_EmissionColor", new Color(255,81,199)*0.01f);
    }
}
