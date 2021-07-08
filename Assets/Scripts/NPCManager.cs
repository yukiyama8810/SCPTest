using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCManager : MonoBehaviour
{
    [SerializeField,Header("瞬きゲージの減少速度")] float BlinkSpeed;
    [SerializeField,Header("瞬きゲージリセットに要する時間")] float BlinkRecast;
    public bool death = false;
    public bool MoveComplete;
    [System.NonSerialized]public bool Inshadow = false;
    GameObject Enemy;
    NavMeshAgent agent;
    Animator anim;

    [SerializeField] Transform targetpos;
    Vector3 Startpos;

    [Range(0f,100f)]float BlinkGage = 100;

    public float blinkGage
    {
        get { return BlinkGage; }
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {
        Startpos = transform.position;
        anim = GetComponent<Animator>();
        Enemy = GameObject.FindGameObjectWithTag("173");
        agent = GetComponent<NavMeshAgent>();
        enabled = false; //Update,FixedUpdate停止の為にスクリプトコンポーネントを無効化
        yield return new WaitUntil(() => GameManagerWithDoor.iiinstance.DoorOpen);
        yield return StartCoroutine(MoveFirst());
        enabled = true;

    }



    IEnumerator MoveFirst()
    {
        agent.destination = targetpos.position;
        anim.SetBool("Walk", true);
        while (true)
        {
            if(Vector3.Distance(transform.position,targetpos.position) < 0.62f)
            {
                break;
                Debug.Log("MoveComplete" + this.name);
            }
            yield return null;
        }
        anim.SetBool("Walk", false);
        MoveComplete = true;
    }

    public IEnumerator Escape()
    {
        agent.destination = Startpos;
        anim.SetBool("Walk", true);
        yield return new WaitUntil(() => Vector3.Distance(transform.position,Startpos) < 0.1f);
        anim.SetBool("Walk", false);
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
        else if (anim.GetBool("Walk"))
        {
            agent.ResetPath();
        }

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
    }

    private void FixedUpdate()
    {
        //Debug.LogError(gameObject.name + "のゲージ" + BlinkGage + "で暗闇状態が" + Inshadow);
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