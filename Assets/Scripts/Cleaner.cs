using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{

    int AllCheckPx;//現状の消えたピクセル数
    bool AllClear;

    [HideInInspector] public int AllPx;//ステージ上全てのDirtオブジェクトのピクセル合計数

    [SerializeField,Header("クリア割合"), Range(0f, 1.0f)] float ClearPercentage;
    [SerializeField,Header("一度にどれだけ透明にするか"),Range(0f,1.0f)] private float _alphaPercentage;
    [SerializeField,Header("手の長さ(2くらいが丁度いい？)")] float RayRange;
    [SerializeField,Header("消しゴムサイズ(初期値15)")]private int _brushSize;
    // TODO 上記二つは読み取り専用にしたい

    


    public float _AlphaPercentage
    {
        get { return _alphaPercentage; }
    }
    public int _BrushSize { get { return _brushSize; } }

    void Start()
    {
        ClearPercentage = DateManager.instance.ClearPercentage;
        _alphaPercentage = DateManager.instance.AlphaPercentage;
        RayRange = DateManager.instance.RayRange;
        _brushSize = (int)DateManager.instance.BrushSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,RayRange))
            {
                if (hit.collider.tag == "DirtFloor")
                {
                    hit.collider.gameObject.GetComponent<CleanFloor>().ChangeTexture(hit.textureCoord);
                }
                else if(hit.collider.tag == "Push")
                {
                    if (!GameManagerWithDoor.iiinstance.PushStatus)
                    {
                        //押されたアニメーションを追加
                        GameManagerWithDoor.iiinstance.PushStatus = true;
                    }
                }
            }
        }
    }

    public void CheckCount(int x)
    {
        if (!AllClear)
        {
            AllCheckPx += x;
            if (AllCheckPx >= AllPx * ClearPercentage)
            {
                StartCoroutine(GameManagerWithDoor.iiinstance.Clear());
                AllClear = true;
            }
        }
    }
}
