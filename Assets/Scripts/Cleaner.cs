using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{

    int AllCheckPx;//����̏������s�N�Z����

    [HideInInspector] public int AllPx;//�X�e�[�W��S�Ă�Dirt�I�u�W�F�N�g�̃s�N�Z�����v��

    [SerializeField,Header("�N���A����"), Range(0f, 1.0f)] float ClearPercentage;
    [Header("��x�ɂǂꂾ�������ɂ��邩"),Range(0f,1.0f)] public float _AlphaPercentage;
    [Header("�����S���T�C�Y(�����l15)")]public int _BrushSize;
    //ToDo ��L��͓ǂݎ���p�ɂ�����

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
        //ToDo ����|�������ő��N���A�Ȃ̂ŉ����l����
    }
}
