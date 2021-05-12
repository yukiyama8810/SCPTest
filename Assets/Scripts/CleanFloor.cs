using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanFloor : MonoBehaviour
{
    [SerializeField, Header("�K��l�ɏ]��")] bool Auto = true;//���L���GameManager�Ɠ������� 
    [SerializeField,Range(0f,1.0f)] float AlphaPercentage; //��x�̔���łǂꂭ�炢���������邩
    //[SerializeField] float ClearParcentage; //�G���A�̉����������΃N���A����Ɉڂ邩

    [SerializeField] int BrushSize;//�����S���̃T�C�Y

    int Check; //�w��A���t�@�l������������v�s�N�Z����
    int okpix; //���C���e�N�X�`���̑S�s�N�Z����
    Texture2D mainTex;
    Texture2D drawTexture;
    Color[] buffer;

    //�e�X�g�p���l
    int texX;
    int texY;

    private bool FirstCheck;//��������Z�̃X�L�b�v�p
    private int ErasePx = 0;

    private MeshRenderer meshRenderer;
    private Cleaner cleaner;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("GameController").TryGetComponent(out cleaner);
        TryGetComponent(out meshRenderer);

        mainTex = (Texture2D)meshRenderer.material.mainTexture;
        Color[] pixels = mainTex.GetPixels();
        okpix = mainTex.width * mainTex.height;
        cleaner.AllPx += okpix;

        buffer = new Color[pixels.Length];
        pixels.CopyTo(buffer, 0);

        if (Auto)
        {
            AlphaPercentage = cleaner._AlphaPercentage;
            BrushSize = cleaner._BrushSize;
        }

        //for(int x = 0; x < mainTexture.width; x++)
        //{
        //    for(int y = 0; y < mainTexture.height; y++)
        //    {
        //        if(y < mainTexture.height / 2)
        //        {
        //            buffer.SetValue(ChangeColor, x + 256 * y);
        //        }
        //    }
        //}

        drawTexture = new Texture2D(mainTex.width, mainTex.height, TextureFormat.RGBA32, false);
        drawTexture.filterMode = FilterMode.Point;
        //Debug.Log("thes~~~" + this.GetComponent<Renderer>().material.mainTexture.height);
    }

    public void Draw(Vector2 p)
    {
        Color original;

        
        Check = 0;
        //buffer.SetValue(ChangeColor, (int)p.x + 256 * (int)p.y);
        texX = 0;
        texY = 0;

        for (int x = 0; x < mainTex.width; x++)
        {
            texX++;
            for (int y = 0; y < mainTex.height; y++)
            {
                texY++;
                original = (Color)buffer.GetValue(x + mainTex.width * y);
                if (original.a < 0.1)
                {
                    Check++;
                }
                if ((p - new Vector2(x, y)).magnitude < BrushSize)
                {
                    original.a -= AlphaPercentage;
                    buffer.SetValue(original, x + mainTex.width * y);
                }
            }
        }
        if (ErasePx < Check)
        {
            cleaner.CheckCount(Check - ErasePx);
        }
        ErasePx = Check;
        //Debug.Log("X : Y[" + texX + " : " + texY / texX + "]");
        //if (Check > okpix * ClearParcentage)
        //{
        //    ClearCheck();
        //}
    }

    //void ClearCheck()
    //{
    //    Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
    //}

    public void ChangeTexture(Vector3 textureCoord)
    {
        Draw(textureCoord * mainTex.width);
        drawTexture.SetPixels(buffer);
        drawTexture.Apply();
        meshRenderer.material.mainTexture = drawTexture;
    }


    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            if(hit.collider.tag == "DirtFloor")
    //            {
    //                Draw(hit.textureCoord * mainTex.width);
    //                drawTexture.SetPixels(buffer);
    //                drawTexture.Apply();
    //                hit.collider.GetComponent<Renderer>().material.mainTexture = drawTexture;
    //            }
    //        }


    //    }
    //}
}
