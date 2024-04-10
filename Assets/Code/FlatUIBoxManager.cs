using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlatUIBoxManager : MonoBehaviour
{
    [SerializeField] private Image BoxImage;

    private void Awake()
    {
        BoxImage = GetComponent<Image>();

        GameManager.ToGame += delegate () { StartCoroutine(Hide()); };
        GameManager.ToResult += delegate () { StartCoroutine(Show()); };
        GameManager.ToRequestEncore += delegate() { StartCoroutine(Hide()); };
    }

    public IEnumerator Hide()
    {
        while (BoxImage.color.a > 0.001)
        {
            BoxImage.color = new Color(BoxImage.color.r, BoxImage.color.g, BoxImage.color.b, BoxImage.color.a - Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator Show()
    {
        while (BoxImage.color.a <= 0.999)
        {
            BoxImage.color = new Color(BoxImage.color.r, BoxImage.color.g, BoxImage.color.b, BoxImage.color.a + Time.deltaTime);
            yield return null;
        }
    }
}
