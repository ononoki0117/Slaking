using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class Sheet
{
    public string title;
    public uint BPM;
    public int noteCount;
    public short signatureHI;
    public short signatureLO;
    public uint offset;

    public string artist;
    public string difficulty;
    public string source;

    public NoteData[] spectator;
    public NoteData[] actor;

    public Queue<NoteData> spectatorQueue;
    public Queue<NoteData> actorQueue;

    // 판정 범위
    private uint[] judgementRange = { 80, 120, 150, 180 };

    public void PrintInfo()
    {
        Debug.Log($"title: {title}\n" +
            $"BPM: {BPM}\n" +
            $"noteCount: {noteCount}\n" +
            $"signature: {signatureHI} / {signatureLO}\n" +
            $"offset: {offset}\n" +
            $"artist: {artist}\n" +
            $"difficulty: {difficulty}\n" +
            $"source: {source}\n");

        foreach (NoteData data in spectatorQueue)
        {
            data.PrintInfo();
        }
        foreach (NoteData data in actorQueue)
        {
            data.PrintInfo();
        }
    }

    public Sheet Init()
    {
        // 지금은 고정 판정 범위


        //judgementRange = new uint[5];
        //switch (difficulty)
        //{
        //    // 난이도에 따라서 판정 값을 다르게 설정 할거임
        //}

        if (spectatorQueue == null)
            spectatorQueue = new Queue<NoteData>(spectator);

        if (actorQueue == null)
            actorQueue = new Queue<NoteData>(actor);
        return this;
    }
    #region Deprecated
    /*
    public Sprite sprite;

    public float noteSpeed;

    private Image imageElement;

    public void makeNote()
    {
        imageElement = new GameObject("Image").AddComponent<Image>();
        imageElement.transform.SetParent(GameObject.Find("Canvas").transform);
        imageElement.sprite = sprite;

        RectTransform rectTransform = imageElement.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(-468, 0);
        rectTransform.sizeDelta = new Vector2(100, 100);
    }

    float getNotePosition(RectTransform _rectTransform)
    {
        float xPos = _rectTransform.transform.position.x;
        xPos += noteSpeed * Time.deltaTime;
        return xPos;
    }

    void moveNote(Image _image)
    {
        RectTransform rectTransform = _image.GetComponent<RectTransform>();
        float newPos = getNotePosition(rectTransform);
        _image.rectTransform.position = new Vector2(newPos, _image.rectTransform.position.y);
    }

    void Start()
    {
        makeNote();
    }

    void Update()
    {
        moveNote(imageElement);
    }
    */
    #endregion
}

[Serializable]
public class NoteData
{
    public string kind;
    public uint position;
    public uint? duration;

    public NoteData PrintInfo()
    {
        Debug.Log($"NoteKind: {kind}\n" +
            $"position: {position}\n" +
            $"duration: {duration}");

        return this;
    }
}

public enum NOTEKIND
{
    KEYDOWN, KEYUP
}

public enum JUDGEMENT
{
    PERFECT, GREAT, NICE, BAD, MISS, NONOTE
}

public enum PLAYER
{
    SPECTATOR, ACTOR
}

public enum DIFFICULTY
{

}