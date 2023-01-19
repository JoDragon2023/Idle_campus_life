using System;
using System.Collections;
using System.Collections.Generic;
using Exoa.Events;
using Pathfinding;
using UnityEngine;

public partial class RoleVideo : BaseObj
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
    private CharacterController characterController;
    private RoleAnimator roleAnimator;
    
    public RoleVideo(RoleData data, Action<GameObject> loadCB = null)
    {
        this.roleData = data;
        var t = GameManager.Instance.roleRoot.transform;
        if (t == null)
            return;
        
        LoadGo("RoleVideo", t, loadCB);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    protected override void LoadGoCallBack(GameObject go)
    {
        base.LoadGoCallBack(go);
        CameraEvents.OnRequestObjectFollow?.Invoke(go, false, false);
        animator = go.GetComponentInChildren<Animator>();
        characterController = go.GetComponent<CharacterController>();
        InitState();
    }

    protected override RoleData GetBaseData()
    {
        return roleData;
    }
    
    public void SetPath(List<Point> path)
    {
        this.m_movePath = path;
    }

    public bool HitTest(Vector3 v3)
    {
        throw new NotImplementedException();
    }

    public override void OnDelete()
    {
        base.OnDelete();
        ReleaseState();
    }
    
    
  
}
