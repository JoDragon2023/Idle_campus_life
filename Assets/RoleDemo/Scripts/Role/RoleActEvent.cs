using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleActEvent : MonoBehaviour
{
    public static RoleActEvent Instance;
    
    /// <summary>
    /// 睡觉
    /// </summary>
    public Button BtnSleep;
    /// <summary>
    /// 休息区
    /// </summary>
    public Button BtnRestArea;
    /// <summary>
    /// 上厕所
    /// </summary>
    public Button BtnToilet;
    /// <summary>
    /// 洗澡
    /// </summary>
    public Button BtnBathe ;
    /// <summary>
    /// 图书馆
    /// </summary>
    public Button BtnLibrary ;
    /// <summary>
    /// 科学实验室
    /// </summary>
    public Button BtnLaboratory;
    
    
    /// <summary>
    /// 睡觉
    /// </summary>
    public Action BtnSleepAct;
    /// <summary>
    /// 休息区
    /// </summary>
    public Action BtnRestAreaAct;
    /// <summary>
    /// 上厕所
    /// </summary>
    public Action BtnToiletAct;
    /// <summary>
    /// 洗澡
    /// </summary>
    public Action BtnBatheAct ;
    /// <summary>
    /// 图书馆
    /// </summary>
    public Action BtnLibraryAct ;
    /// <summary>
    /// 科学实验室
    /// </summary>
    public Action BtnLaboratoryAct;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        BtnSleep.onClick.AddListener(()=>{ BtnSleepAct?.Invoke(); });

        BtnRestArea.onClick.AddListener(() => { BtnRestAreaAct?.Invoke(); });
            
        BtnToilet.onClick.AddListener(()=>{ BtnToiletAct?.Invoke(); });
        
        BtnBathe.onClick.AddListener(()=>{ BtnBatheAct?.Invoke(); });
        
        BtnLibrary.onClick.AddListener(()=>{ BtnLibraryAct?.Invoke(); });
        
        BtnLaboratory.onClick.AddListener(()=>{ BtnLaboratoryAct?.Invoke(); });
    }
}
