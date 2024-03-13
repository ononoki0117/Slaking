using UnityEngine;

public class Parser : MonoBehaviour
{
    private static Sheet sheet;
    public static Sheet Load(string path)
    {
        // 지금은 곡 하나만 쓸것을 상정으로 하고 있음 => null 조건을 볼게 아니라 sheet 체크 조건을 만들것 
        if (sheet != null) return sheet;

        TextAsset textAsset = Resources.Load<TextAsset>(path);
        Debug.Log(textAsset.text);

        sheet = JsonUtility.FromJson<Sheet>(textAsset.text).Init();

        return sheet;
    }
    public static Sheet GetSheet()
    {
        return sheet;
    }
}

