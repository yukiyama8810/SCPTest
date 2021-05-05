using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public float BlinkSpeed;
    public float BlinkRecast;
    [System.NonSerialized]public bool Inshadow = false;

    float BlinkGage = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.LogError(gameObject.name + "‚ÌƒQ[ƒW" + BlinkGage + "‚ÅˆÃˆÅó‘Ô‚ª" + Inshadow);
        if(BlinkGage > 0)
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
    }
    void BlinkReset()
    {
        BlinkGage = 100;
        Inshadow = false;
    }
}