using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    public GameObject obj1;
    public GameObject obj2;
    public GameObject obj3;

    private int activeMode;

    // Start is called before the first frame update

    void switcher() {
        if (activeMode==0) // 1번 오브젝트
        {
            obj1.SetActive(true);
            obj2.SetActive(false);
            obj3.SetActive(false);
        }
        else if (activeMode==1) // 2번 오브젝트
        {
            obj1.SetActive(false);
            obj2.SetActive(true);
            obj3.SetActive(false);
        }
        else if (activeMode==2) // 3번 오브젝트
        {
            obj1.SetActive(false);
            obj2.SetActive(false);
            obj3.SetActive(true);
        }
    }

    void Start()
    {
        activeMode = 0;
        switcher();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O)) 
        {
            if(activeMode < 2) activeMode++;
            else activeMode = 0;

            switcher();
        }
    }
}
