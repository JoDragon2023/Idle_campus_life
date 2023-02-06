using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideMager : MonoBehaviour
{
    public static GuideMager Instance;
    
    /// <summary>
    /// 移动摄像头
    /// </summary>
    public GameObject Panning;

    /// <summary>
    /// 旋转
    /// </summary>
    public GameObject Rotating;

    /// <summary>
    /// 缩放
    /// </summary>
    public GameObject Zooming;

    /// <summary>
    /// 俯视
    /// </summary>
    public GameObject Lift;
    
    private const string GuideStepStr = "GuideStepStr";
    private GuideStep currGuideStep = GuideStep.None;
    [HideInInspector]
    public bool isGuide = false;
    [HideInInspector]
    public bool isPan = false;
    [HideInInspector]
    public bool isRotate = false;
    [HideInInspector]
    public bool isZoom = false;
    [HideInInspector]
    public bool isTile = false;

    private bool isStartGuide = false;
    private int currStep = 0;
    private float durTime;
    private float StartTime = 1;
    
    
    private void Awake()
    {
        Instance = this;
        var step = PlayerPrefs.GetInt(GuideStepStr);
        currGuideStep = (GuideStep)step;
        if (step <= 0)
        {
            currGuideStep = GuideStep.Panning;
        }

        currStep = (int)currGuideStep;
        
    }

    void Start()
    {
        //StartGuide();
    }

    private void Update()
    {
        if (isStartGuide)
        {
            durTime += Time.deltaTime;
            if (durTime > StartTime)
            {
                isStartGuide = false;
                durTime = 0;
                StartGuide();
            }
        }
    }

    public void StartGuide()
    {
        if (currGuideStep == GuideStep.Complete) return;
        isGuide = true;
        SetCheck(currGuideStep,true);
        SetIsShowGuide(currGuideStep,true);
    }
    
    public void CompleteGuide(GuideStep guideStep)
    {
        SetCheck(guideStep,false);
        if (guideStep == GuideStep.Lift)
        {
            guideStep = GuideStep.Complete;
            isGuide = false;
        }
        
        SetIsShowGuide(guideStep,false);
        currStep++;
        currGuideStep = (GuideStep)currStep;
        PlayerPrefs.SetInt(GuideStepStr, (int)currGuideStep);
        PlayerPrefs.Save();
        isStartGuide = true;
    }

    private void SetCheck(GuideStep guideStep, bool isStart)
    {
        switch (guideStep)
        {
            case GuideStep.Panning:
                isPan = isStart;
                break;
            case GuideStep.Rotating:
                isRotate = isStart;
                break;
            case GuideStep.Zooming:
                isZoom = isStart;
                break;
            case GuideStep.Lift:
                isTile = isStart;
                break;
        }
    }
    
    private void SetIsShowGuide(GuideStep guideStep,bool isStart)
    {
        switch (guideStep)
        {
            case GuideStep.Panning:
                Panning.SetActive(isStart);
                break;
            case GuideStep.Rotating:
                Rotating.SetActive(isStart);
                break;
            case GuideStep.Zooming:
                Zooming.SetActive(isStart);
                break;
            case GuideStep.Lift:
                Lift.SetActive(isStart);
                break;
            case GuideStep.Complete:
                Lift.SetActive(isStart);
                break;
          
        }
    }
}
