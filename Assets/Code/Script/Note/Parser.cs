using UnityEngine;

public class Parser : MonoBehaviour
{
    private static Sheet sheet;
    public static Sheet Load(string path)
    {
        // ������ �� �ϳ��� ������ �������� �ϰ� ���� => null ������ ���� �ƴ϶� sheet üũ ������ ����� 
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

