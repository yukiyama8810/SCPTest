using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingManager : MonoBehaviour
{
    [SerializeField] Slider CPs;
    [SerializeField] InputField CPi;

    [SerializeField] Slider APs;
    [SerializeField] InputField APi;

    [SerializeField] Slider RRs;
    [SerializeField] InputField RRi;

    [SerializeField] Slider BSs;
    [SerializeField] InputField BSi;


    // Start is called before the first frame update
    void Start()
    {
        if (!DateManager.instance.isDefaultSet)
        {
            SetDefault();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    float value;
    public void InputCPChange()
    {
        value = float.Parse(CPi.text);
        value = Mathf.Clamp(value, CPs.minValue, CPs.maxValue);
        CPChenge();
    }

    public void CPChenge()
    {
        if(value != 0)
        {
            CPs.value = value;
        }
        CPi.text = CPs.value.ToString("F0");
        DateManager.instance.ClearPercentage = CPs.value/100;
        value = 0;
    }


    public void InputAPChange()
    {
        APs.value = float.Parse(APi.text);
    }

    public void APChange()
    {
        APi.text = APs.value.ToString("F0");
        DateManager.instance.AlphaPercentage = APs.value / 100;
    }


    public void InputRRChange()
    {
        RRs.value = float.Parse(RRi.text);
    }

    public void RRChange()
    {
        RRi.text = RRs.value.ToString("F1");
        DateManager.instance.RayRange = RRs.value;
    }


    public void InputBSChange()
    {
        BSs.value = float.Parse(BSi.text);
    }

    public void BSChange()
    {
        BSi.text = BSs.value.ToString("F1");
        DateManager.instance.BrushSize = BSs.value;
    }

    public void SetDefault()
    {
        CPs.value = 75f;
        APs.value = 20f;
        RRs.value = 2.3f;
        BSs.value = 15f;
        CPi.text = "75";
        APi.text = "20";
        RRi.text = "2.3";
        BSi.text = "15";
        DateManager.instance.ClearPercentage = 0.75f;
        DateManager.instance.AlphaPercentage = 0.2f;
        DateManager.instance.RayRange = 2.3f;
        DateManager.instance.BrushSize = 15f;
    }


    [SerializeField] GameObject[] AllCanvas;
    public void ButtonManager(string name)
    {
        switch (name)
        {
            case "DifficutyPanel":
                foreach(GameObject AC in AllCanvas)
                {
                    if(name == AC.name)
                    {
                        AC.SetActive(true);
                    }
                    else
                    {
                        AC.SetActive(false);
                    }
                }
                break;

            case "VideoPanel":
                foreach (GameObject AC in AllCanvas)
                {
                    if (name == AC.name)
                    {
                        AC.SetActive(true);
                    }
                    else
                    {
                        AC.SetActive(false);
                    }
                }
                break;

            case "Return00":
                SceneManager.LoadScene("Opening");
                break;

            case "Return01":
                foreach (GameObject AC in AllCanvas)
                {
                    if (AC.name == "Layer01")
                    {
                        AC.SetActive(true);
                    }
                    else
                    {
                        AC.SetActive(false);
                    }
                }
                break;
        }
            
    }



    [SerializeField] GameObject[] DifficutyPanel;
    public void DifficultyPanelChange(string name)
    {
        foreach(GameObject DP in DifficutyPanel)
        {
            if(name == DP.name)
            {
                DP.SetActive(true);
            }
            else
            {
                DP.SetActive(false);
            }
        }
    }
}
