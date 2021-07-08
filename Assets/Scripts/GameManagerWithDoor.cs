using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManagerWithDoor : MonoBehaviour
{
    public static GameManagerWithDoor iiinstance;

    [SerializeField] GameObject SCP173;
    [SerializeField] NPCManager[] NPC;
    public bool PushStatus;
    public bool GameStart;
    public bool DoorOpen;
    public bool inPlayer;
    public Text DebugLog;

    GameObject Appearance;
    GameObject Barrier;
    Canvas Gameover;
    

    private void Awake()
    {
        if(iiinstance == null)
        {
            iiinstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Start()
    {
        Gameover = transform.Find("GameOver").gameObject.GetComponent<Canvas>();
        DebugLog = transform.Find("Canvas/DebugLog").gameObject.GetComponent<Text>();
        Appearance = transform.Find("Appearance").gameObject;
        Barrier = transform.Find("Barrier").gameObject;
        yield return new WaitUntil(() => PushStatus);
        Appearance.transform.DOLocalMoveY(2.8f, 1.5f).SetEase(Ease.Linear).OnComplete(() => 
        {
            DoorOpen = true;
            Barrier.SetActive(false);
        });
        Debug.LogError("�h�A���[���Ղ�");
        //�h�A���J���Ă��Q�[���X�^�[�g�ł��v���C���[�����ɋ���Ƃ��ɕ��Ă����z���g�ɃX�^�[�g
        yield return new WaitUntil(() => DoorOpen && GameStart && inPlayer);
        PushStatus = false;
        DoorOpen = false;
        Barrier.SetActive(true);
        Appearance.transform.DOLocalMoveY(-0.1f, 1.5f).SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
        //NPC�S������̈ʒu�ɒ����Ă邩�m�F�AOK�Ȃ�X�^�[�g��ture
        if (!GameStart)//ToDo ���gFor�̕��������C������̂Ŋm�F
        {
            int x = 0;
            foreach (NPCManager npc in NPC)
            {
                if (npc.MoveComplete)
                {
                    x++;
                    if(NPC.Length == x)
                    {
                        GameStart = true;
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }

    public IEnumerator GameOver()
    {
        //���ꂽ�����̃J�������[�N�������ǉ�
        Gameover.gameObject.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        yield return null;
    }

    /// <summary>
    /// �|��������̃h�A�I�[�v������N���A�����܂�
    /// </summary>
    /// <returns></returns>
    public IEnumerator Clear()
    {
        Appearance.transform.DOLocalMoveY(2.8f, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Debug.LogError("�h�A�I�[�v���N���A�p");
            DoorOpen = true;
            Barrier.SetActive(false);
            foreach(NPCManager npc in NPC)
            {
                if (!npc.death)
                {
                    StartCoroutine(npc.Escape());
                }
            }
        });
        yield return new WaitUntil(() => PushStatus);
        Debug.LogError("PushStatus" + PushStatus);
        Barrier.SetActive(true);
        //����NPC�ɃO�b�o�C���闬������
        GameStart = false;
        Appearance.transform.DOLocalMoveY(-0.1f, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            StartCoroutine(Finish());
        });
    }

    IEnumerator Finish()
    {
        //yield return NPC�̋A�҃`�F�b�N
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0f;
        //�N���A���
    }
}
