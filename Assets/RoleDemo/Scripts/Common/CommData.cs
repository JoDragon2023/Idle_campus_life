
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class RoleData
{
    public CreateRoleType createRoleType;
    public RoleType roleType;
    public int roleId;
    public Vector3 pos;
}

public class Point
{
    public GameObject go;
    public List<Vector3> posPoint;
}

public interface IStateMachineObj
{
    GameObject gameObject { get; }

    StateMachine SMachine { get; }
        
    void SetPath(List<Point> path);

    Action MoveCallBack { get; set; }

    bool HitTest(Vector3 v3);
}