using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanFloor : MonoBehaviour
{
    public float AlphaPercentage; //一度の判定でどれくらい透明化するか
    public float ClearParcentage; //エリアの何割を消せばクリア判定に移るか

    int Check; //指定アルファ値を下回った合計ピクセル数
    int okpix; //メインテクスチャの全ピクセル数
    Texture2D mainTex;
    Texture2D drawTexture;
    Color[] buffer;

    //テスト用数値
    int texX;
    int texY;


    // Start is called before the first frame update
    void Start()
    {
        mainTex = (Texture2D)GetComponent<Renderer>().material.mainTexture;
        Color[] pixels = mainTex.GetPixels();
        okpix = mainTex.width * mainTex.height;


        buffer = new Color[pixels.Length];
        pixels.CopyTo(buffer, 0);

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
                if ((p - new Vector2(x, y)).magnitude < 20)
                {
                    original.a -= AlphaPercentage;
                    buffer.SetValue(original, x + mainTex.width * y);
                }
            }
        }
        Debug.Log("X : Y[" + texX + " : " + texY / texX + "]");
        if (Check > okpix * ClearParcentage)
        {
            ClearCheck();
        }
    }

    void ClearCheck()
    {
        Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
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
                if(hit.collider.tag == "DirtFloor")
                {
                    Draw(hit.textureCoord * mainTex.width);
                    drawTexture.SetPixels(buffer);
                    drawTexture.Apply();
                    hit.collider.GetComponent<Renderer>().material.mainTexture = drawTexture;
                }
            }


        }
    }
}
