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
        //NPC = transform.parent.parent.GetComponent<NPCManager>();
        NPC = transform.parent.GetComponentInParent<NPCManager>();
        blink = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        transform.rotation = Camera.main.transform.rotation;
        
        blink.value = NPC.blinkGage / 100;

    }
}
