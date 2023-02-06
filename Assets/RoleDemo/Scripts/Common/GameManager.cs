﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Action<float> UpdateAct;

    /// <summary>
    /// 学生上课 事件
    /// </summary>
    public Action StudentAttendAct;

    /// <summary>
    /// 老师上课 事件
    /// </summary>
    public Action TeacherAttendAct;

    /// <summary>
    /// 上课结束 事件
    /// </summary>
    public Action AttendEndAct;

    /// <summary>
    /// 循环 上课 事件
    /// </summary>
    public Action LoopAttendAct;

    
    public Action ClassroomOneAct;

    public Action ClassroomTwoAct;

    public Action ClassroomThreeAct;

    public Action ClassroomForAct;
    
    //事件 触发事件 时间
    // private int TeacherAttendTime = 30;
    // private int StudentAttendTime = 35;
    // private int AttendEndTime = 60;
    // private int LoopAttendTime = 60;

    // 阶梯教室 动作间隔时间
    private float TeacherAttendTime = 0.6f;

    private float StudentAttendTime = 0.8f;

    private float AttendEndTime = 1f;

    private float LoopAttendTime = 1.2f;

    //图书馆教室 动作间隔时间
    private float ClassroomOneTime = 0.6f;

    private float ClassroomTwoTime = 0.8f;

    private float ClassroomThreeTime = 1f;

    private float ClassroomForTime = 1.2f;

    private bool OpenClassroomOneTime;

    private bool OpenClassroomTwoTime;

    private bool OpenClassroomThreeTime;

    private bool OpenClassroomForTime;


    private float currClassroomOneTime;
    private float currClassroomTwoTime;
    private float currClassroomThreeTime;
    private float currClassroomForTime;
    
    private float currTeacherAttendTime;
    private float currStudentAttendTime;
    private float currAttendEndTime;
    private float currLoopAttendTime;
    [HideInInspector] public bool OpenTeacherAttendTime;
    [HideInInspector] public bool OpenStudentAttendTime;
    [HideInInspector] public bool OpenAttendEndTime;
    [HideInInspector] public bool OpenLoopAttendTime;

    //事件 开始触发事件
    /// <summary>
    /// 到达目标点事件
    /// </summary>
    public Action ArriveAct;

    public float currentStateTime;
    [HideInInspector] public GameObject roleRoot;
    [HideInInspector] public GameObject targetRoot;
    [HideInInspector] public DoorEvent[] doorEventAry;

    private BedAnim[] bedAniAry;
    private LaboratoryAnim[] LaboratoryAnimAry;
    private ToiletDoorAnim[] toiletDoorAnim;
    public Action<DoorAnim, Collider> DoorRaycastEvent;


    public Action btnRightAct;

    public Action btnMiddleAct;

    public Action btnLeftAct;

    public Button btnRight;

    public Button btnMiddle;

    public Button btnLeft;

    public GameObject CTCameraController;

    public RandomEvent RandomEvent = RandomEvent.Event12;


    private void Awake()
    {
        Instance = this;
        roleRoot = GameObject.Find("__roleRoot__");
        if (roleRoot == null)
        {
            roleRoot = new GameObject("__roleRoot__");
        }

        targetRoot = GameObject.Find("__targetRoot__");
        if (targetRoot == null)
        {
            targetRoot = new GameObject("__targetRoot__");
        }

        doorEventAry = Resources.FindObjectsOfTypeAll<DoorEvent>();
        bedAniAry = Resources.FindObjectsOfTypeAll<BedAnim>();
        LaboratoryAnimAry = Resources.FindObjectsOfTypeAll<LaboratoryAnim>();
        toiletDoorAnim = Resources.FindObjectsOfTypeAll<ToiletDoorAnim>();
        btnRight.onClick.AddListener(OnBtnRight);
        btnMiddle.onClick.AddListener(OnBtnMiddle);
        btnLeft.onClick.AddListener(OnBtnLeft);
    }

    private void OnBtnRight()
    {
        btnRightAct?.Invoke();
    }

    private void OnBtnMiddle()
    {
        //Debug.Log(" ");

        Debug.Log(" CTCameraController position " + CTCameraController.transform.position);
        Debug.Log(" CTCameraController  rotation  x " + CTCameraController.transform.rotation.eulerAngles.x + " y" +
                  CTCameraController.transform.rotation.eulerAngles.y + " z " +
                  CTCameraController.transform.rotation.eulerAngles.z);


        var tilt = CTCameraController.transform.GetChild(0);
        Debug.Log(" Tilt position " + tilt.localPosition);
        Debug.Log(" Tilt  rotation  x " + tilt.localRotation.eulerAngles.x + " y" +
                  tilt.localRotation.eulerAngles.y + " z " + tilt.localRotation.eulerAngles.z);

        var Zoom = tilt.GetChild(0);
        Debug.Log(" Zoom position " + Zoom.localPosition);
        Debug.Log(" Zoom  rotation  x " + Zoom.localRotation.eulerAngles.x + " y" +
                  Zoom.localRotation.eulerAngles.y + " z " + Zoom.localRotation.eulerAngles.z);


        btnMiddleAct?.Invoke();
    }

    private void OnBtnLeft()
    {
        btnLeftAct?.Invoke();
    }

    void Start()
    {
        SceneObjMgr.Instance.Init();
        InitDoorRayEvent();
        OpenTeacherAttendTime = true;
        OpenStudentAttendTime = true;
        OpenAttendEndTime = true;
        OpenLoopAttendTime = true;

        OpenClassroomOneTime = true;
        OpenClassroomTwoTime = true;
        OpenClassroomThreeTime = true;
        OpenClassroomForTime = true;
 
    }

    void Update()
    {
        OnUpdate(Time.deltaTime);
    }

    private void InitDoorRayEvent()
    {
        if (doorEventAry == null || doorEventAry.Length <= 0) return;

        foreach (var t in doorEventAry)
        {
            t.rayEvent += TriggerDoorRayEvent;
        }
    }

    private void TriggerDoorRayEvent(DoorAnim doorAnim, Collider collider)
    {
        DoorRaycastEvent?.Invoke(doorAnim, collider);
    }

    private void OnUpdate(float deltaTime)
    {
        currentStateTime += deltaTime;
        UpdateAttendAct(deltaTime);
        UpdateAct?.Invoke(deltaTime);
    }


    /// <summary>
    /// 上课事件更新的处理
    /// </summary>
    private void UpdateAttendAct(float deltaTime)
    {
        if (OpenClassroomOneTime)
        {
            currClassroomOneTime += deltaTime;
            if (currClassroomOneTime > ClassroomOneTime)
            {
                currClassroomOneTime = 0;
                OpenClassroomOneTime = false;
                ClassroomOneAct?.Invoke();
            }
        }
        
        if (OpenClassroomTwoTime)
        {
            currClassroomTwoTime += deltaTime;
            if (currClassroomTwoTime > ClassroomTwoTime)
            {
                currClassroomTwoTime = 0;
                OpenClassroomTwoTime = false;
                ClassroomTwoAct?.Invoke();
            }
        }
        
        if (OpenClassroomThreeTime)
        {
            currClassroomThreeTime += deltaTime;
            if (currClassroomThreeTime > ClassroomThreeTime)
            {
                currClassroomThreeTime = 0;
                OpenClassroomThreeTime = false;
                ClassroomThreeAct?.Invoke();
            }
        }
        
        if (OpenClassroomForTime)
        {
            currClassroomForTime += deltaTime;
            if (currClassroomForTime > ClassroomForTime)
            {
                currClassroomForTime = 0;
                OpenClassroomForTime = false;
                ClassroomForAct?.Invoke();
            }
        }
        
        if (OpenTeacherAttendTime)
        {
            currTeacherAttendTime += deltaTime;
            if (currTeacherAttendTime > TeacherAttendTime)
            {
                //Debug.Log("  老师 ");
                currTeacherAttendTime = 0;
                OpenTeacherAttendTime = false;
                TeacherAttendAct?.Invoke();
            }
        }

        if (OpenStudentAttendTime)
        {
            currStudentAttendTime += deltaTime;
            if (currStudentAttendTime > StudentAttendTime)
            {
                //Debug.Log("  学生 ");
                currStudentAttendTime = 0;
                OpenStudentAttendTime = false;
                StudentAttendAct?.Invoke();
            }
        }

        if (OpenAttendEndTime)
        {
            currAttendEndTime += deltaTime;
            if (currAttendEndTime > AttendEndTime)
            {
                //Debug.Log("  上课结束 ");
                currAttendEndTime = 0;
                OpenAttendEndTime = false;
                AttendEndAct?.Invoke();
            }
        }

        if (OpenLoopAttendTime)
        {
            currLoopAttendTime += deltaTime;
            if (currLoopAttendTime > LoopAttendTime)
            {
                //Debug.Log("  再次 开始上课 ");
                currLoopAttendTime = 0;
                OpenLoopAttendTime = false;
                LoopAttendAct?.Invoke();
                TeacherAttendAct?.Invoke();
                StudentAttendAct?.Invoke();
            }
        }
    }

    /// <summary>
    /// 获取床播放动画对象
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public BedAnim GetBedAnim(EventRandomPath path)
    {
        for (var i = 0; i < bedAniAry.Length; i++)
        {
            if (bedAniAry[i].eventRandomPath == path)
            {
                return bedAniAry[i];
            }
        }

        return null;
    }

    /// <summary>
    /// 获取科学实验室动画
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public LaboratoryAnim GetLaboratoryAnim(EventRandomPath path)
    {
        for (var i = 0; i < LaboratoryAnimAry.Length; i++)
        {
            if (LaboratoryAnimAry[i].eventRandomPath == path)
            {
                return LaboratoryAnimAry[i];
            }
        }

        return null;
    }


    /// <summary>
    /// 获取厕所门 动画对象
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public ToiletDoorAnim GetToiletDoorAnim(RandomEvent randomEvent, EventRandomPath path)
    {
        int len = toiletDoorAnim.Length;
        for (var i = 0; i < len; i++)
        {
            if (toiletDoorAnim[i].doorAnimType == path && toiletDoorAnim[i].randomEvent == randomEvent)
            {
                return toiletDoorAnim[i];
            }
        }

        return null;
    }
}