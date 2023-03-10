using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Pathfinding.RVO;
using UnityEngine;

public partial class Role : BaseObj
{
    /// <summary>
    /// 当前角色动画
    /// </summary>
    private Animator animator;
    /// <summary>
    /// 当前动画状态信息
    /// </summary>
    private AnimatorStateInfo currRoleAnimatorStateInfo { get; set; }
    public RoleData roleData;
    public CreateRoleType currRoleType = CreateRoleType.None;

    private CharacterController characterController;
    private SimpleSmoothModifier simpleSmoothModifier ;
    private AIDestinationSetter aiDestination ;
    private AIPath aiPath;
    private RoleAnimator roleAnimator;
    //private RVOController rvoController;
    public int randomNumMax = 100;
    public int currRandomNumMax = 0;
    public int currRandomPathMax = 0;
    
    public Role(RoleData data, Action<GameObject> loadCB = null)
    {
        this.roleData = data;
        currRoleType = data.createRoleType;

        GameManager.Instance.DoorRaycastEvent += TriggerDoorRayEvent;

        InitAttendAct();
       
        var t = GameManager.Instance.roleRoot.transform;
        if (t == null)
            return;

        LoadGo(currRoleType.ToString(), t, loadCB);
        
        var targetRoot = GameManager.Instance.targetRoot.transform;
        if (targetRoot == null)
            return;
        
        LoadTarget("Target", targetRoot, LoadTarget);
        
    }

    private void LoadTarget(GameObject obj)
    {
        aiDestination.target = obj.transform;
    }

    
    private void TriggerDoorRayEvent(DoorAnim doorAnim, Collider collider)
    {
        if (currRandomEvent == doorAnim.randomEvent && go == collider.gameObject)
        {
            doorAnim.OpenAnim();
        }
    }
    
    public void MoveTo(float posX, float posY, Action moveCB = null)
    {
        //需要处理 寻路
        this.MoveCallBack = moveCB;
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    protected override void LoadGoCallBack(GameObject go)
    {
        base.LoadGoCallBack(go);
        animator = go.GetComponentInChildren<Animator>();
        characterController = go.GetComponent<CharacterController>();
        simpleSmoothModifier = go.GetComponent<SimpleSmoothModifier>();
        aiDestination = go.GetComponent<AIDestinationSetter>();
        aiPath = go.GetComponent<AIPath>();
        roleAnimator = go.GetComponent<RoleAnimator>();
        //rvoController = go.GetComponent<RVOController>();
        InitState();
    }

    private void InitAttendAct()
    {
        if (roleData.roleType == RoleType.Student)
        {
            GameManager.Instance.StudentAttendAct += StudentAttendAct;
        }
        else if (roleData.roleType == RoleType.Teacher)
        {
            GameManager.Instance.TeacherAttendAct += TeacherAttendAct;
        }
        
        GameManager.Instance.AttendEndAct += AttendEndAct;
        GameManager.Instance.LoopAttendAct += LoopAttendAct;
    }
    
    private void SetAIComponent(bool isEnabled)
    {
        characterController.enabled = isEnabled;
        simpleSmoothModifier.enabled = isEnabled;
        aiDestination.enabled = isEnabled;
        aiPath.enabled = isEnabled;
        //rvoController.enabled = isEnabled;
    }

    private void SetAiDestination(bool isEnabled)
    {
        if (aiDestination != null)
        {
            aiDestination.enabled = isEnabled;
        }
    }

    private void SetCharacterController(bool isEnabled)
    { 
        if (characterController != null)
        {
            characterController.enabled = isEnabled;
        }
    }
 
    public void SetTargetPos(Vector3 pos)
    {
        target.transform.position = pos;
    }
    
    protected override RoleData GetBaseData()
    {
        return roleData;
    }
    
    public void SetPath(List<Point> path)
    {
        this.m_movePath = path;
    }

    private void GetRolePath(int maxRandom)
    {
        curRandomPath = EventRandomPath.None;
        currRandomPathMax = maxRandom;
        currRandomNumMax = 0;
        isRoleRandom = false;
        GetRoleRandomPath();
    }

    private bool isRoleRandom = false;
    private void GetRoleRandomPath()
    {
        if (currRandomNumMax > randomNumMax)
        {
            curRandomPath = EventRandomPath.None;
            return;
        }
        
        currRandomNumMax++;
        var path = RoleRandomPath();
        RoleActPathRandom((EventRandomPath)path);
        if (!isRoleRandom)
        {
            isRoleRandom = true;
            if (currRandomNumMax > randomNumMax)
            {
                curRandomPath = EventRandomPath.None;
            }
            else
            {
                curRandomPath = (EventRandomPath)path;
            }
        }
    }
    
    private int RoleRandomPath()
    {
        return UnityEngine.Random.Range(1 , currRandomPathMax);;
    }
    
    private void RoleActPathRandom(EventRandomPath randomPath)
    {
        var roleList = SceneObjMgr.Instance.roleList;
        var len = roleList.Count;
       
        for (var i = 0; i < len; i++)
        {
            if (roleList[i].currRandomEvent == currRandomEvent)
            {
                if (roleList[i].currRandomEventAct == currRandomEventAct)
                {
                    if (roleList[i].roleData.roleType != RoleType.Teacher)
                    {
                        if (roleList[i].curRandomPath == randomPath)
                        {
                            GetRoleRandomPath();
                            break;
                        }
                    }
                }
            }
        }   
    }

    private bool RoleSwitchDoor()
    {
        curRandomPath = EventRandomPath.None;
        currRandomPathMax = 2;
        var path = RoleRandomPath();
        var roleList = SceneObjMgr.Instance.roleList;
        var len = roleList.Count;
        bool isSwitchDoor = false;

        for (var i = 0; i < len; i++)
        {
            if (roleList[i].currRandomEvent == currRandomEvent)
            {
                if (roleList[i].currRandomEventAct == currRandomEventAct)
                {
                    if (roleList[i].curRandomPath == (EventRandomPath)path)
                    {
                        isSwitchDoor = true;
                        break;
                    }
                }
            }
        }

        curRandomPath = (EventRandomPath)path;
        return isSwitchDoor;
    }
    
    public override void OnDelete()
    {
        if (roleData.roleType == RoleType.Student)
        {
            GameManager.Instance.StudentAttendAct -= StudentAttendAct;
        }
        else if (roleData.roleType == RoleType.Teacher)
        {
            GameManager.Instance.StudentAttendAct -= TeacherAttendAct;
        }
        
        GameManager.Instance.AttendEndAct -= AttendEndAct;
        GameManager.Instance.LoopAttendAct -= LoopAttendAct;
        
        base.OnDelete();
        ReleaseState();
    }
    
}