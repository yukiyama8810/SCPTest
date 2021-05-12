using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{

    int AllCheckPx;//現状の消えたピクセル数

    [HideInInspector] public int AllPx;//ステージ上全てのDirtオブジェクトのピクセル合計数

    [SerializeField,Header("クリア割合"), Range(0f, 1.0f)] float ClearPercentage;
    [Header("一度にどれだけ透明にするか"),Range(0f,1.0f)] public float _AlphaPercentage;
    [Header("消しゴムサイズ(初期値15)")]public int _BrushSize;
    //ToDo 上記二つは読み取り専用にしたい

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "DirtFloor")
                {
                    hit.collider.gameObject.GetComponent<CleanFloor>().ChangeTexture(hit.textureCoord);
                }
            }


        }
    }

    public void CheckCount(int x)
    {
        AllCheckPx += x;
        if(AllCheckPx >= AllPx * ClearPercentage)
        {
            Clear();
        }
    }

    void Clear()
    {
        Debug.Log("CCCLLEARRRRRRRRRRRR");
        //ToDo 現状掃除完了で即クリアなので何か考える
    }
}
