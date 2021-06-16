using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCManager : MonoBehaviour
{
    [SerializeField,Header("�u���Q�[�W�̌������x")] float BlinkSpeed;
    [SerializeField,Header("�u���Q�[�W���Z�b�g�ɗv���鎞��")] float BlinkRecast;
    public bool death = false;
    [System.NonSerialized]public bool Inshadow = false;
    GameObject Enemy;
    NavMeshAgent agent;
    Animator anim;

    [Range(0f,100f)]float BlinkGage = 100;

    public float blinkGage
    {
        get { return BlinkGage; }
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Enemy = GameObject.FindGameObjectWithTag("173");
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine("MoveFirst");
        
    }

    IEnumerator MoveFirst()
    {
        yield return new WaitForSeconds(5f);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetBool("Death") && !death)
        {
            death = true;
        }

        if (!death)
        {
            transform.LookAt(Enemy.transform.position);
        }
        

    //    //Debug.LogError(gameObject.name + "�̃Q�[�W" + BlinkGage + "�ňÈŏ�Ԃ�" + Inshadow);
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
    }

    private void FixedUpdate()
    {
        //Debug.LogError(gameObject.name + "�̃Q�[�W" + BlinkGage + "�ňÈŏ�Ԃ�" + Inshadow);
        if (!death)
        {
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
        }
        else if(BlinkGage > 0)
        {
            BlinkGage = 0;
            Inshadow = true;
        }
    }

    void BlinkReset()
    {
        if (!death)
        {
            BlinkGage = 100;
            Inshadow = false;
        }
    }
}