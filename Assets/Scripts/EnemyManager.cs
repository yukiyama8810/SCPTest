using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 瞬き全般の管理
/// </summary>
public class EnemyManager : MonoBehaviour
{
    public float RayTime;
    public Transform Playertf;
    [SerializeField] GameObject Player;
    UnityStandardAssets.Characters.FirstPerson.PlayerManager playerManager;
    



    private GameObject[] classD;
    private bool LookEnemy = false;
    private bool OnWillRenderCheck;
    // Start is called before the first frame update
    void Start()
    {
        classD = GameObject.FindGameObjectsWithTag("DClass");
        playerManager = Player.GetComponent<UnityStandardAssets.Characters.FirstPerson.PlayerManager>();
    }

    void Update()
    {
        GameManagerWithDoor.iiinstance.DebugLog.text = LookEnemy ? "見てます" : "見えてませーん";
        if (GameManagerWithDoor.iiinstance.GameStart)
        {
            if (!LookEnemy)
            {
                CheckNPC();
            }
            //Debug.Log(LookEnemy ? "見てます" : "見えてませーん");
            LookEnemy = false;
        }


    }

    private void OnWillRenderObject()
    {
        if (Camera.current.name == "FirstPersonCharacter")
        {            
            
            var target = Player.transform.position - transform.position;
            Ray ray = new Ray(transform.position, target);
            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, RayTime, true);
            if (Physics.Raycast(ray, out hit,Vector3.Distance(Playertf.position,transform.position)))
            {
                /*
                 ・現状「視界内」か「障害物あり」の2つしか判定が無く「視界外」が存在しない。
                 原案：OnBecameInvisibleを使用し視界外に出た瞬間問答無用でLookEnemyをfalseにする。
                 問題：シーンカメラが邪魔、ゲームカメラ以外が存在してると機能しないので機能するやつを作ってみたい。
                 第二案：このメソッド内でまずfalseに変えてから上記二つの路線をたどることによってそれ以外の場合＝視界外と見てfalseで抜けるように変更
                 問題？：一時的とはいえ毎度強制falseに変わるのでそれによって問題が生じないかどうか。

                あと下のifでの判定が雑なので綺麗にできればする

                上記二つひとまずクリア
                 */
                if (hit.collider.tag == "Player")
                {
                    //Debug.Log("プレイヤーの視界内");
                    if (!LookEnemy)
                    {
                        if (!playerManager.p_Inshadow)
                        {
                            LookEnemy = true;
                        }
                    }
                }
                else
                {
                    //Debug.Log("障害物あり");
                    if (LookEnemy)
                    {
                        LookEnemy = false;
                    }
                }
            }
        }
        else
        {
            //Debug.Log(Camera.current.name);
        }
    }





    int a;

    /// <summary>
    /// NPCの瞬き状態の管理
    /// </summary>
    void CheckNPC()
    {
        if(classD.Length == 0)
        {
            StartCoroutine(GameManagerWithDoor.iiinstance.GameOver());
        }
        foreach(GameObject obj in classD)
        {
            if (obj.GetComponent<NPCManager>().Inshadow)
            {
                a++;
            }
            else
            {
                a = 0;
                break;
            }
            if(a == classD.Length)
            {
                MoveForKill();
            }

        }
    }

    GameObject MinDisObj;
    void MoveForKill()
    {
        float MinDistance = -1;
        float distance;
        foreach (GameObject obj in classD)
        {
            distance = Vector3.Distance(transform.position, obj.transform.position);
            if(MinDistance == -1 || distance < MinDistance)
            {
                MinDistance = distance;
                MinDisObj = obj;
            }
            Debug.Log(obj.name + "との距離" + distance);
        }
        Debug.Log("殺害対象は" + MinDisObj.name);
        MinDisObj.GetComponent<Animator>().SetBool("Death", true);
        MinDisObj.tag = "DD";
        classD = GameObject.FindGameObjectsWithTag("DClass");
        transform.LookAt(MinDisObj.transform.position);
        transform.position = MinDisObj.transform.position;
    }
}
