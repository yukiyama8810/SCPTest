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
    public bool GameStart;
    public bool DoorOpen;
    public bool inPlayer;
    public bool AllClear;
    public Text DebugLog;

    GameObject Appearance;
    GameObject Barrier;
    

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
        DebugLog = transform.Find("Canvas/DebugLog").gameObject.GetComponent<Text>();
        Appearance = transform.Find("Appearance").gameObject;
        Barrier = transform.Find("Barrier").gameObject;
        yield return new WaitForSeconds(2f);
        Appearance.transform.DOLocalMoveY(2.8f, 1.5f).SetEase(Ease.Linear).OnComplete(() => 
        {
            DoorOpen = true;
            Barrier.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameStart)//ToDo 中身Forの方がいい気もするので確認
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
        if(DoorOpen && GameStart && inPlayer)
        {
            Barrier.SetActive(true);
            Appearance.transform.DOLocalMoveY(-0.1f, 1.5f).SetEase(Ease.Linear);
        }

        if (AllClear)
        {
            StartCoroutine(Clear());
        }
    }

    /// <summary>
    /// 掃除完了後のドアオープンからクリア準備まで
    /// </summary>
    /// <returns></returns>
    IEnumerator Clear()
    {
        Appearance.transform.DOLocalMoveY(2.8f, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Barrier.SetActive(false);
        });
        yield return null;
    }
}
