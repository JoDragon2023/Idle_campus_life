using System;
using UnityEngine;

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
    
    //事件 触发事件 时间
    // private int TeacherAttendTime = 30;
    // private int StudentAttendTime = 35;
    // private int AttendEndTime = 60;
    // private int LoopAttendTime = 60;
    
    private int TeacherAttendTime = 110;
    private int StudentAttendTime = 115;
    private int AttendEndTime = 120;
    private int LoopAttendTime = 300;
    
    private float currTeacherAttendTime;
    private float currStudentAttendTime;
    private float currAttendEndTime;
    private float currLoopAttendTime;
    [HideInInspector]
    public bool OpenTeacherAttendTime;
    [HideInInspector]
    public bool OpenStudentAttendTime;
    [HideInInspector]
    public bool OpenAttendEndTime;
    [HideInInspector]
    public bool OpenLoopAttendTime;
    
    //事件 开始触发事件
    /// <summary>
    /// 到达目标点事件
    /// </summary>
    public Action ArriveAct;
    public float currentStateTime;
    [HideInInspector]
    public GameObject roleRoot;
    [HideInInspector]
    public GameObject targetRoot;
    [HideInInspector]
    public DoorEvent[] doorEventAry;
   
    private BedAnim[] bedAniAry;
    private ToiletDoorAnim[] toiletDoorAnim;
    public Action<DoorAnim, Collider> DoorRaycastEvent;

    public float CreateRoleTime = 1f;
    
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

        doorEventAry =  Resources.FindObjectsOfTypeAll<DoorEvent>();
        bedAniAry =  Resources.FindObjectsOfTypeAll<BedAnim>();
        toiletDoorAnim =  Resources.FindObjectsOfTypeAll<ToiletDoorAnim>();
    }

    void Start()
    {
        SceneObjMgr.Instance.Init();
        InitDoorRayEvent();
        OpenTeacherAttendTime = true;
        OpenStudentAttendTime = true;
    }

    void Update()
    {
        OnUpdate(Time.deltaTime);
    }

    private void InitDoorRayEvent()
    {
        if (doorEventAry == null|| doorEventAry.Length <= 0 ) return;

        foreach (var t in doorEventAry)
        {
            t.rayEvent += TriggerDoorRayEvent;
        }
    }

    private void TriggerDoorRayEvent(DoorAnim doorAnim, Collider collider)
    {
        DoorRaycastEvent?.Invoke(doorAnim,collider);
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
