using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerWithDoor : MonoBehaviour
{
    public static GameManagerWithDoor iiinstance;

    [SerializeField] GameObject SCP173;
    [SerializeField] NPCManager[] NPC;
    public bool PushStatus;
    public bool GameStart;
    public bool DoorOpen;
    public bool inPlayer;
    public bool StatusClear;
    public Text DebugLog;

    GameObject Appearance;
    GameObject Barrier;
    Canvas Gameover;
    

    private void Awake()
    {
        if(iiinstance == null)
        {
            iiinstance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Time.timeScale = 1f;
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
        Debug.LogError("ドアおーっぷん");
        //ドアが開いてかつゲームスタートでかつプレイヤーが中に居るときに閉じていざホントにスタート
        yield return new WaitUntil(() => DoorOpen && GameStart && inPlayer);
        PushStatus = false;
        DoorOpen = false;
        Barrier.SetActive(true);
        Appearance.transform.DOLocalMoveY(-0.1f, 1.5f).SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
        //NPC全員所定の位置に着いてるか確認、OKならスタートをture
        if (!GameStart && !StatusClear)//ToDo 中身Forの方がいい気もするので確認
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

        Debug.LogError("タイムスケール"+Time.timeScale);
    }

    public IEnumerator GameOver()
    {
        //やられた感じのカメラワークか何か追加
        Gameover.gameObject.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        yield return null;
    }

    /// <summary>
    /// 掃除完了後のドアオープンからクリア準備まで
    /// </summary>
    /// <returns></returns>
    public IEnumerator Clear()
    {
        Appearance.transform.DOLocalMoveY(2.8f, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Debug.LogError("ドアオープンクリア用");
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
        StatusClear = true;
        //中のNPCにグッバイする流れを作る
        GameStart = false;
        Appearance.transform.DOLocalMoveY(-0.1f, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
        {
            StartCoroutine(Finish());
        });
        yield return new WaitForSeconds(0.5f);
        foreach(NPCManager npc in NPC)
        {
            npc.agent.ResetPath();
        }
    }

    IEnumerator Finish()
    {
        //yield return NPCの帰還チェック
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0f;
        //クリア画面
        Gameover.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void GameoverButton(string name)
    {
        switch (name)
        {
            case "Restart":
                SceneManager.LoadScene("SampleScene");
                break;

            case "Menu":
                SceneManager.LoadScene("Opening");
                break;
                
        }
    }
}
