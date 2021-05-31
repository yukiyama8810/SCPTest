using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkGageUI : MonoBehaviour
{
    NPCManager NPC;
    Slider blink;

    //ToDo なんかできないのとスマートにしたいなーこれ
    private void Start()
    {
        NPC = transform.root.gameObject.GetComponent<NPCManager>();
    }

    private void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
        
        //blink.value = NPC.blinkGage / 100;

    }
}
