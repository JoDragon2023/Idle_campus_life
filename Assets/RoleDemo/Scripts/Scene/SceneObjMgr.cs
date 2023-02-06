using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SceneObjMgr : MonoSingleton<SceneObjMgr>,IDisposable
{
    public List<Role> roleList;   
    public List<Role> roleClassList;   
    public List<BaseObj> objList;
    private float createTime = 1;
    private float durTime;
    private int objIdCount = 0;
    private int roleCount = 4;
    private bool isUpdate = false;
    /// <summary>
    /// 是否 开始上课
    /// </summary>
    public bool isStartGoToClass = false;
    public SceneObjMgr()
    {
        objList = new List<BaseObj>();
        roleList = new List<Role>();
        roleClassList = new List<Role>();
    }

    public void Init()
    {
        isUpdate = true;
        // createRandomEvent = GameManager.Instance.RandomEvent;
        // InitCreate();
        // isUpdate = false;
        // CreateRole();
    }

  
    private void Update()
    {
        if (!isUpdate)
        {
            if (roleList.Count >= roleCount)
            {
                isUpdate = true;
            }
            
            durTime += Time.deltaTime;
            if (durTime > createTime)
            {
                durTime = 0;
                CreateRole();
            }
        }
    }

    private int RoleIndex = 0;
    private void CreateRole()
    {
        RoleData data = new RoleData();
        data.roleId = CreateObjId();
        
        if (data.roleId > 16)
        {
            data.createRoleType = GetCreateRoleType();
        }
        else
        {
            data.createRoleType = createObjList[RoleIndex];
            RoleIndex++;
        }

        if (createRandomEvent == RandomEvent.Event4)
        {
            data.roleType = RoleType.None;
            if (data.roleId <= 4)
            {
                data.roleType = RoleType.Student;
            }
            else if (data.roleId == 5)
            {
                data.roleType = RoleType.Teacher;
            }
        }
        data.pos = ScenePoint.Instance.GetCreatePoint();
        CreateObj(data);
    }

    private CreateRoleType GetCreateRoleType()
    {
        var index = Random.Range(1, 17);
        return (CreateRoleType)index;
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    public BaseObj CreateObj(RoleData data)
    {
        BaseObj obj;
        obj = CreateRole(data);
        objList.Add(obj);
        return obj;
    }
    
    private Role CreateRole(RoleData data)
    {
        for (int i = 0; i < roleList.Count; i++)
        {
            RoleData tData = roleList[i].roleData;
            if (tData.roleId == data.roleId)
            {
                roleList[i].SetData(data);
                return roleList[i];
            }
        }

        var role = new Role(data);
        roleList.Add(role);
        if (data.roleId <= 5)
        {
            roleClassList.Add(role);
        }
        
        return role;
    }

    public void GetIsRoleEnterClass()
    {
        var len = roleClassList.Count;
        bool isStartTime = true;
        for (var i = 0; i < len; i++)
        {
            if (!roleClassList[i].isEnterClassroom)
            {
                isStartTime = false;
                break;;
            }
        }

        if (isStartTime)
        {
            isStartGoToClass = true;
            GameManager.Instance.OpenAttendEndTime = true;
        }
    }
    
    public void GetIsRoleLeaveClass()
    {
        var len = roleClassList.Count;
        bool isStartTime = true;
        for (var i = 0; i < len; i++)
        {
            if (!roleClassList[i].isleaveClassroom)
            {
                isStartTime = false;
                break;;
            }
        }

        if (!isStartTime) return;
        //开启 新的一轮
        GameManager.Instance.OpenLoopAttendTime = true;
        isStartGoToClass = false;
    }
    
    private void OnDestroy()
    {
    }

    internal int CreateObjId()
    {
        return ++objIdCount;
    }
    
    protected virtual RoleData GetBaseData(RoleData data)
    {
        return data;
    }

    private List<CreateRoleType> createObjList = new List<CreateRoleType>();

    public RandomEvent createRandomEvent;
    
    private void InitCreate()
    {
        switch (createRandomEvent)
        {
            case RandomEvent.None:
                break;
            /// <summary>
            /// 休息区
            /// </summary>
            case RandomEvent.Event1:
                roleCount = 2;
                createObjList.Add(CreateRoleType.Role9);
                createObjList.Add(CreateRoleType.Role10);
                break;
            /// <summary>
            /// 图书馆
            /// </summary>
            case RandomEvent.Event2:
                roleCount = 4;
                createObjList.Add(CreateRoleType.Role3);
                createObjList.Add(CreateRoleType.Role5);
                createObjList.Add(CreateRoleType.Role1);
                createObjList.Add(CreateRoleType.Role4);
                break;
            /// <summary>
            /// 睡觉
            /// </summary>
            case RandomEvent.Event3:
                roleCount = 1;
                createObjList.Add(CreateRoleType.Role8);
                break;
            /// <summary>
            /// 阶梯实验室
            /// </summary>
            case RandomEvent.Event4:
                roleCount = 4;
                createObjList.Add(CreateRoleType.Role2);
                createObjList.Add(CreateRoleType.Role4);
                createObjList.Add(CreateRoleType.Role1);
                createObjList.Add(CreateRoleType.Role7);
                createObjList.Add(CreateRoleType.Role16);
                break;
            case RandomEvent.Event5:
                break;
            case RandomEvent.Event6:
                break;
            case RandomEvent.Event7:
                break;
            /// <summary>
            /// 上厕所
            /// </summary>
            case RandomEvent.Event8:
                roleCount = 2;
                createObjList.Add(CreateRoleType.Role2);
                createObjList.Add(CreateRoleType.Role5);
                break;
            /// <summary>
            /// 洗澡
            /// </summary>
            case RandomEvent.Event9:
                roleCount = 2;
                createObjList.Add(CreateRoleType.Role3);
                createObjList.Add(CreateRoleType.Role7);
                break;
            case RandomEvent.Event10:
                break;
            case RandomEvent.Event11:
                break;
            /// <summary>
            /// 科学实验室
            /// </summary>
            case RandomEvent.Event12:
                roleCount = 2;
                createObjList.Add(CreateRoleType.Role11);
                createObjList.Add(CreateRoleType.Role12);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
}