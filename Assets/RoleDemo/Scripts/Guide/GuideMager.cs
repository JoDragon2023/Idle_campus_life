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
    
    private void Awake()
    {
        Instance = this;
        var step = PlayerPrefs.GetInt(GuideStepStr);
        currGuideStep = (GuideStep)step;
        if (step == 0)
        {
            currGuideStep = GuideStep.Panning;
        }
    }
    
    void Start()
    {
        StartGuide();
    }

    public void StartGuide()
    {
        if (currGuideStep == GuideStep.Complete) return;
        isGuide = true;
        SetIsShowGuide(currGuideStep,true);
    }
    
    public void CompleteGuide(GuideStep guideStep)
    {
        if (guideStep == GuideStep.Lift)
        {
            guideStep = GuideStep.Complete;
            isGuide = false;
        }
        
        SetIsShowGuide(guideStep,false);
        PlayerPrefs.SetInt(GuideStepStr, (int)guideStep);
        PlayerPrefs.Save();
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
