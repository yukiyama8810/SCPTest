using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{

    [SerializeField] Button _Start;
    [SerializeField] Button _Setting;


    // Start is called before the first frame update
    void Start()
    {
        _Start.onClick.AddListener(ButtonStart);
        _Setting.onClick.AddListener(ButtonSetting);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ButtonStart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void ButtonSetting()
    {
        SceneManager.LoadScene("Settings");
    }
}
