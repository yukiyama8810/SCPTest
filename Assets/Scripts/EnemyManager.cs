using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �u���S�ʂ̊Ǘ�
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
        GameManagerWithDoor.iiinstance.DebugLog.text = LookEnemy ? "���Ă܂�" : "�����Ă܂��[��";
        if (GameManagerWithDoor.iiinstance.GameStart)
        {
            if (!LookEnemy)
            {
                CheckNPC();
            }
            //Debug.Log(LookEnemy ? "���Ă܂�" : "�����Ă܂��[��");
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
                 �E����u���E���v���u��Q������v��2�������肪�����u���E�O�v�����݂��Ȃ��B
                 ���āFOnBecameInvisible���g�p�����E�O�ɏo���u�Ԗⓚ���p��LookEnemy��false�ɂ���B
                 ���F�V�[���J�������ז��A�Q�[���J�����ȊO�����݂��Ă�Ƌ@�\���Ȃ��̂ŋ@�\����������Ă݂����B
                 ���āF���̃��\�b�h���ł܂�false�ɕς��Ă����L��̘H�������ǂ邱�Ƃɂ���Ă���ȊO�̏ꍇ�����E�O�ƌ���false�Ŕ�����悤�ɕύX
                 ���H�F�ꎞ�I�Ƃ͂������x����false�ɕς��̂ł���ɂ���Ė�肪�����Ȃ����ǂ����B

                ���Ɖ���if�ł̔��肪�G�Ȃ̂��Y��ɂł���΂���

                ��L��ЂƂ܂��N���A
                 */
                if (hit.collider.tag == "Player")
                {
                    //Debug.Log("�v���C���[�̎��E��");
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
                    //Debug.Log("��Q������");
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
    /// NPC�̏u����Ԃ̊Ǘ�
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
            Debug.Log(obj.name + "�Ƃ̋���" + distance);
        }
        Debug.Log("�E�Q�Ώۂ�" + MinDisObj.name);
        MinDisObj.GetComponent<Animator>().SetBool("Death", true);
        MinDisObj.tag = "DD";
        classD = GameObject.FindGameObjectsWithTag("DClass");
        transform.LookAt(MinDisObj.transform.position);
        transform.position = MinDisObj.transform.position;
    }
}