using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    [SerializeField] private GameObject avatar;
    [SerializeField] private Material controllerMaterial;
    [SerializeField] private GameObject[] Controllers;
    
    private void Awake()
    {
        //GameManager.ToWearing += delegate () { avatar.SetActive(true); };
        //GameManager.ToWearing += delegate () { ShowXRController(true); };

        //GameManager.ToTutorial += delegate () { avatar.SetActive(false); };
        //GameManager.ToTutorial += delegate () { ShowXRController(true); };

        //GameManager.ToSelectMusic += delegate () { avatar.SetActive(false); };
        //GameManager.ToSelectMusic += delegate () { ShowXRController(true); };

        //GameManager.ToGame += delegate () { avatar.SetActive(true); };
        //GameManager.ToGame += delegate () { ShowXRController(false); };

        //GameManager.ToCommunication += delegate () { avatar.SetActive(true); };
        //GameManager.ToCommunication += delegate () { ShowXRController(false); };

        //GameManager.ToResult += delegate () { avatar.SetActive(true); };
        //GameManager.ToResult += delegate () { ShowXRController(true); };

        //GameManager.ToRequestEncore += delegate () { avatar.SetActive(true); };
        //GameManager.ToRequestEncore += delegate () { ShowXRController(true); };

        //GameManager.ToGameOver += delegate () { avatar.SetActive(true); };
        //GameManager.ToGameOver += delegate () { ShowXRController(true); };
    }
    private void Start()
    {
        foreach(GameObject obj in Controllers)
        {
            obj.SetActive(true);
        }
    }

    private void ShowXRController(bool isActive)
    {
        if (isActive)
        {
            controllerMaterial.SetColor("_Color", new Color(1, 1, 1, 1));
        }
        else
        {
            controllerMaterial.SetColor("_Color", new Color(1, 1, 1, 0));
        }
    }

    private void ShowAvatar(bool isActive)
    {
        if (isActive)
        {
            
        }
    }
}
