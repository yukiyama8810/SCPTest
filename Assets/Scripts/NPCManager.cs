using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField,Header("瞬きゲージの減少速度")] float BlinkSpeed;
    [SerializeField,Header("瞬きゲージリセットに要する時間")] float BlinkRecast;
    [System.NonSerialized]public bool Inshadow = false;

    [Range(0f,100f)]float BlinkGage = 100;

    public float blinkGage
    {
        get { return BlinkGage; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
    //    //Debug.LogError(gameObject.name + "のゲージ" + BlinkGage + "で暗闇状態が" + Inshadow);
    //    if(BlinkGage > 0)
    //    {
    //        BlinkGage -= BlinkSpeed * Time.deltaTime;
    //    }
    //    else
    //    {
    //        if (!Inshadow)
    //        {
    //            Invoke("BlinkReset", BlinkRecast);
    //            Inshadow = true;
    //        }
    //    }
    //}

    private void FixedUpdate()
    {
        //Debug.LogError(gameObject.name + "のゲージ" + BlinkGage + "で暗闇状態が" + Inshadow);
        if (BlinkGage > 0)
        {
            BlinkGage -= BlinkSpeed;
        }
        else
        {
            if (!Inshadow)
            {
                Invoke("BlinkReset", BlinkRecast);
                Inshadow = true;
            }
        }
        Debug.Log(blinkGage);
    }

    void BlinkReset()
    {
        BlinkGage = 100;
        Inshadow = false;
    }
}