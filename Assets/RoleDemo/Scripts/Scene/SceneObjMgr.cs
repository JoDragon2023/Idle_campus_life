using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SceneObjMgr : MonoSingleton<SceneObjMgr>,IDisposable
{
    public List<Role> roleList;    
    public List<BaseObj> objList;
    private float createTime = 2;
    private float durTime;
    private int objIdCount = 0;
    private int roleCount = 20;
    private bool isUpdate = false;
    
    public SceneObjMgr()
    {
        objList = new List<BaseObj>();
        roleList = new List<Role>();
    }

    public void Init()
    {
        isUpdate = false;
        CreateRole();
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
    
    private void CreateRole()
    {
        RoleData data = new RoleData();
        data.roleType = GetCreateRoleType();
        data.roleId = CreateObjId();
        data.pos = ScenePoint.Instance.GetCreatePoint();
        CreateObj(data);
    }

    private CreateRoleType GetCreateRoleType()
    {
        var index = Random.Range(1, 13);
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
        return role;
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
    
}