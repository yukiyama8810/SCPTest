using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{

    int AllCheckPx;//����̏������s�N�Z����

    [HideInInspector] public int AllPx;//�X�e�[�W��S�Ă�Dirt�I�u�W�F�N�g�̃s�N�Z�����v��

    [SerializeField,Header("�N���A����"), Range(0f, 1.0f)] float ClearPercentage;
    [SerializeField,Header("��x�ɂǂꂾ�������ɂ��邩"),Range(0f,1.0f)] private float _alphaPercentage;
    [SerializeField,Header("��̒���(2���炢�����x�����H)")] float RayRange;
    [SerializeField,Header("�����S���T�C�Y(�����l15)")]private int _brushSize;
    // TODO ��L��͓ǂݎ���p�ɂ�����

    


    public float _AlphaPercentage
    {
        get { return _alphaPercentage; }
    }
    public int _BrushSize { get { return _brushSize; } }

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
            if (Physics.Raycast(ray, out hit,RayRange))
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
            GameManagerWithDoor.iiinstance.AllClear = true;
        }
    }
}
