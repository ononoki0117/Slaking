using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

/// <summary>
/// 게임 실행 중 sheet의 Queue에서 Note를 꺼내서 자신이 가지고 있다가 
/// 화면에 디스플레이
/// 각 Note의 판정이 끝나면 FinishedNote로 Note를 보냄
/// 
/// 화면 디스플레이 방식은 인터페이스로 구현?? <- 게임 플레이 방법마다 다를 수 있음
/// 노트의 움직임은 metronome의 currentTime & deltaTime에 영향을 받음 / 유니티 자체 시간이 아님!
/// </summary>
public class NoteObjectPool : MonoBehaviour
{
    [System.Serializable]
    private class ObjectInfo
    {
        public string name;
        public GameObject prefab;
        public int count;
    }

    public static NoteObjectPool instance;

    public bool isReady { get; private set; }

    [SerializeField]
    private ObjectInfo[] objectInfos = null;

    private string objectName;

    private Dictionary<string, IObjectPool<GameObject>> objectPoolDic = new Dictionary<string, IObjectPool<GameObject>>();
    private Dictionary<string, GameObject> gameObjectDic = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        Init();
    }

    private void Init()
    {
        isReady = false;

        for (int i = 0; i < objectInfos.Length; i++)
        {
            IObjectPool<GameObject> pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, true, objectInfos[i].count, objectInfos[i].count);

            if (gameObjectDic.ContainsKey(objectInfos[i].name))
            {
                Debug.LogFormat("{0} 이미 등록된 오브젝트입니다.", objectInfos[i].name);
                return;
            }

            gameObjectDic.Add(objectInfos[i].name, objectInfos[i].prefab);
            objectPoolDic.Add(objectInfos[i].name, pool);   

            for (int j = 0; j < objectInfos[i].count; j++)
            {
                objectName = objectInfos[i].name;
                NoteObject noteObject = CreatePooledItem().GetComponent<NoteObject>();
                noteObject.pool.Release(noteObject.gameObject);
            }

        }

        Debug.Log("오브젝트풀링 준비 완료");
        isReady = true;
    }

    private GameObject CreatePooledItem()
    {
        GameObject _object = Instantiate(gameObjectDic[objectName]);
        _object.GetComponent<NoteObject>().pool = objectPoolDic[objectName];
        return _object;
    }

    private void OnTakeFromPool(GameObject _object)
    {
        _object.SetActive(true);
    }

    private void OnReturnedToPool(GameObject _object)
    {
        _object.SetActive(false);
    }

    private void OnDestroyPoolObject(GameObject _object)
    {
        Destroy(_object);
    }

    public GameObject GetGameObject(string _name)
    {
        objectName = _name;

        if (gameObjectDic.ContainsKey(_name) == false)
        {
            Debug.LogFormat("{0} 오브젝트풀에 등록되지 않은 오브젝트입니다.", _name);
            return null;
        }

        return objectPoolDic[_name].Get();
    }

    #region Deprecated
    //public Sprite sprite;

    //public float noteSpeed;

    //private Metronome metronome;
    //private Image imageElement;

    //public void makeNote()
    //{
    //    imageElement = new GameObject("Image").AddComponent<Image>();
    //    imageElement.transform.SetParent(GameObject.Find("Canvas").transform);
    //    imageElement.sprite = sprite;

    //    RectTransform rectTransform = imageElement.GetComponent<RectTransform>();
    //    rectTransform.anchoredPosition = new Vector2(-468, 0);
    //    rectTransform.sizeDelta = new Vector2(100, 100);
    //}

    //float getNotePosition(RectTransform _rectTransform)
    //{
    //    float xPos = _rectTransform.transform.position.x;
    //    xPos += noteSpeed * GetComponent<Metronome>().deltaTime / 10000;
    //    return xPos;
    //}

    //void moveNote(Image _image)
    //{
    //    RectTransform rectTransform = _image.GetComponent<RectTransform>();
    //    float newPos = getNotePosition(rectTransform);
    //    _image.rectTransform.position = new Vector2(newPos, _image.rectTransform.position.y);
    //}

    //// Start is called before the first frame update
    //void Start()
    //{
    //    makeNote();
    //}

    //// Update is called once per frame
    //void FixedUpdate()
    //{
    //    moveNote(imageElement);
    //}

    //public static NoteObjectPool instance;
    //public GameObject notePrefab;

    //private PLAYER kind;
    //private Sheet sheet;
    //private Metronome metronome;

    //public IObjectPool<NoteObject> Pool { get; private set; }

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else
    //    {
    //        Destroy(this.gameObject);
    //    }

    //    //Init();
    //}

    ////private void Init()
    ////{
    ////    Pool = new IObjectPool<NoteObject>()
    ////}

    ////private NoteObject CreatePooledItem()
    ////{
    ////    GameObject noteObject = Instantiate(notePrefab);
    ////    noteObject.GetComponent<NoteObject>().Pool;
    ////}


    //private void Start()
    //{
    //    sheet = Parser.GetSheet();
    //    metronome = GetComponent<Metronome>();



    //}

    //private void FixedUpdate()
    //{

    //}


    #endregion
}
