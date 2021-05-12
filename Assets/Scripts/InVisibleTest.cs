using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �u���S�ʂ̊Ǘ�
/// </summary>
public class InVisibleTest : MonoBehaviour
{
    public float RayTime;
    public Transform Playertf;

    private GameObject[] classD;
    // Start is called before the first frame update
    void Start()
    {
        classD = GameObject.FindGameObjectsWithTag("DClass");
        MoveForKill();
    }

    private void OnWillRenderObject()
    {
        if (Camera.current.name == "FirstPersonCharacter")
        {
            var target = Playertf.position - transform.position;
            Ray ray = new Ray(transform.position, target);
            RaycastHit hit;
            //Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, RayTime, true);
            if (Physics.Raycast(ray, out hit,Vector3.Distance(Playertf.position,transform.position)))
            {
                
                if (hit.collider.tag == "Player")
                {
                    Debug.Log("�v���C���[�̎��E��");
                }
                else
                {
                    Debug.Log("��Q������");
                }
            }
        }
        else
        {
            //Debug.Log(Camera.current.name);
        }
    }
    void Update()
    {
        CheckNPC();
    }

    int a;

    /// <summary>
    /// NPC�̏u����Ԃ̊Ǘ�
    /// </summary>
    void CheckNPC()
    {
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
            Debug.Log(obj.name + "�Ƃ̋���" + distance);
        }
        Debug.Log("�E�Q�Ώۂ�" + MinDisObj.name);
    }
}
