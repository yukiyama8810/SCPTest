using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateManager : MonoBehaviour
{
    public static DateManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float ClearPercentage = 0.75f;
    public float AlphaPercentage = 0.2f;

    public float RayRange = 2.3f;
    public float BrushSize = 15f;

    public bool isDefaultSet;

    // Start is called before the first frame update
    void Start()
    {
        ClearPercentage = 0.75f;
        AlphaPercentage = 0.2f;
        RayRange = 2.3f;
        BrushSize = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
