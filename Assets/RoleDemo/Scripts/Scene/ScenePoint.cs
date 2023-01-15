using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 记录场景里面点的位置
/// </summary>
public class ScenePoint : MonoBehaviour
{
    public static ScenePoint Instance;
    /// <summary>
    /// 角色 出生的点位。
    /// </summary>
    public Transform[] CreatePoint;

    /// <summary>
    /// 从出生点，到校园的路径
    /// </summary>
    public Transform[] MainPathPoint;

    public Transform[] TwoMainPathPoint;
    
    public Transform[] StepsOne;
    
    public Transform[] StepsTwo;
    
    private Vector3[] createArry;

    private Vector3[] mainPathArry;

    private Vector3[] twoMainPathArry;
 
    private Vector3[] stepsOneArry;

    private Vector3[] stepsTwoArry;

    //闲置点
    public Transform[] IdlePoints;
    private Vector3[] IdlePointArry;
    
    //事件区域 1
    public Transform[] EventOnePoints;
    private Vector3[] EventOnePointArry;
    
    //事件区域 2
    public Transform[] EventTwoPoints;
    private Vector3[] EventTwoPointArry;

    //行为事件点
    public Transform[] Event10ToiletTarget;
    private Vector3[] Event10ToiletTargetArry;

    public Transform[] Event8ToiletTarget;
    private Vector3[] Event8ToiletTargetArry;
    
    public Transform[] Event6ToiletTarget;
    private Vector3[] Event6ToiletTargetArry;
    
    //事件 1
    public Transform[] Event1Target;
    private Vector3[] Event1TargetArry;
    
    //事件 2 上课
    public Transform[] Event2AttendClassTarget;
    private Vector3[] Event2AttendClassTargetArry;
    
    //事件 4 上课
    public Transform[] Event4AttendClassTarget;
    private Vector3[] Event4AttendClassTargetArry;
    
    //事件7 洗澡 
    public Transform[] Event7BatheTarget;
    private Vector3[] Event7BatheTargetArry;
    
    //事件9 洗澡 
    public Transform[] Event9BatheTarget;
    private Vector3[] Event9BatheTargetArry;

    //事件11 洗澡 
    public Transform[] Event11BatheTarget;
    private Vector3[] Event11BatheTargetArry;
    
    //事件3 开关柜门 
    public Transform[] Event3SwitchDoorTarget;
    private Vector3[] Event3SwitchDoorArry;
    
    //事件5 开关柜门 
    public Transform[] Event5SwitchDoorTarget;
    private Vector3[] Event5SwitchDoorArry;
    
    //事件12 科学实验室 
    public Transform[] Event12LaboratoryTarget;
    private Vector3[] Event12LaboratoryArry;
    
    private void Awake()
    {
        Instance = this;
        InitPos();
        InitIdleAndEvenRandom();
    }

    private void InitPos()
    {
        createArry = new Vector3[CreatePoint.Length];
        for (int i = 0; i < CreatePoint.Length; i++)
        {
            createArry[i] = CreatePoint[i].position;
        }
        
        mainPathArry = new Vector3[MainPathPoint.Length];
        for (int i = 0; i < MainPathPoint.Length; i++)
        {
            mainPathArry[i] = MainPathPoint[i].position;
        }
        
        twoMainPathArry = new Vector3[TwoMainPathPoint.Length];
        for (int i = 0; i < TwoMainPathPoint.Length; i++)
        {
            twoMainPathArry[i] = TwoMainPathPoint[i].position;
        }
        
        stepsOneArry = new Vector3[StepsOne.Length];
        for (int i = 0; i < StepsOne.Length; i++)
        {
            stepsOneArry[i] = StepsOne[i].position;
        }
        
        stepsTwoArry = new Vector3[StepsTwo.Length];
        for (int i = 0; i < StepsTwo.Length; i++)
        {
            stepsTwoArry[i] = StepsTwo[i].position;
        }
        
        IdlePointArry = new Vector3[IdlePoints.Length];
        for (int i = 0; i < IdlePoints.Length; i++)
        {
            IdlePointArry[i] = IdlePoints[i].position;
        }
        
        EventOnePointArry = new Vector3[EventOnePoints.Length];
        for (int i = 0; i < EventOnePoints.Length; i++)
        {
            EventOnePointArry[i] = EventOnePoints[i].position;
        }
        
        EventTwoPointArry = new Vector3[EventTwoPoints.Length];
        for (int i = 0; i < EventTwoPoints.Length; i++)
        {
            EventTwoPointArry[i] = EventTwoPoints[i].position;
        }
        
        Event10ToiletTargetArry = new Vector3[Event10ToiletTarget.Length];
        for (int i = 0; i < Event10ToiletTarget.Length; i++)
        {
            Event10ToiletTargetArry[i] = Event10ToiletTarget[i].position;
        }

        Event8ToiletTargetArry = new Vector3[Event8ToiletTarget.Length];
        for (int i = 0; i < Event8ToiletTarget.Length; i++)
        {
            Event8ToiletTargetArry[i] = Event8ToiletTarget[i].position;
        }
        
        Event6ToiletTargetArry = new Vector3[Event6ToiletTarget.Length];
        for (int i = 0; i < Event6ToiletTarget.Length; i++)
        {
            Event6ToiletTargetArry[i] = Event6ToiletTarget[i].position;
        }
        
        Event1TargetArry = new Vector3[Event1Target.Length];
        for (int i = 0; i < Event1Target.Length; i++)
        {
            Event1TargetArry[i] = Event1Target[i].position;
        }
        
        Event2AttendClassTargetArry = new Vector3[Event2AttendClassTarget.Length];
        for (int i = 0; i < Event2AttendClassTarget.Length; i++)
        {
            Event2AttendClassTargetArry[i] = Event2AttendClassTarget[i].position;
        }
        
        Event4AttendClassTargetArry = new Vector3[Event4AttendClassTarget.Length];
        for (int i = 0; i < Event4AttendClassTarget.Length; i++)
        {
            Event4AttendClassTargetArry[i] = Event4AttendClassTarget[i].position;
        }
        
        Event7BatheTargetArry = new Vector3[Event7BatheTarget.Length];
        for (var i = 0; i < Event7BatheTarget.Length; i++)
        {
            Event7BatheTargetArry[i] = Event7BatheTarget[i].position;
        }
        
        Event9BatheTargetArry = new Vector3[Event9BatheTarget.Length];
        for (var i = 0; i < Event9BatheTarget.Length; i++)
        {
            Event9BatheTargetArry[i] = Event9BatheTarget[i].position;
        }

        Event11BatheTargetArry = new Vector3[Event11BatheTarget.Length];
        for (var i = 0; i < Event11BatheTarget.Length; i++)
        {
            Event11BatheTargetArry[i] = Event11BatheTarget[i].position;
        }
        
  
        Event3SwitchDoorArry = new Vector3[Event3SwitchDoorTarget.Length];
        for (var i = 0; i < Event3SwitchDoorTarget.Length; i++)
        {
            Event3SwitchDoorArry[i] = Event3SwitchDoorTarget[i].position;
        }
        
        Event5SwitchDoorArry = new Vector3[Event5SwitchDoorTarget.Length];
        for (var i = 0; i < Event5SwitchDoorTarget.Length; i++)
        {
            Event5SwitchDoorArry[i] = Event5SwitchDoorTarget[i].position;
        }

        Event12LaboratoryArry = new Vector3[Event12LaboratoryTarget.Length];
        for (var i = 0; i < Event12LaboratoryTarget.Length; i++)
        {
            Event12LaboratoryArry[i] = Event12LaboratoryTarget[i].position;
        }
        
    }


    #region 获取行为事件的目标点

    public Vector3 GetRandomEventActPoint(RandomEventAct randomEventAct)
    {
        Vector3 pos = Vector3.zero;
        switch (randomEventAct)
        {
            case RandomEventAct.None:
                break;
            case RandomEventAct.Main:
                break;
            case RandomEventAct.Idle:
                break;
            case RandomEventAct.Event1Sit:
                pos = Event1TargetArry[0];
                break;
            case RandomEventAct.Event1Talk:
                pos = Event1TargetArry[0];
                break;
            case RandomEventAct.Event1EatDrink:
                pos = Event1TargetArry[0];
                break;
            case RandomEventAct.Event2AttendClass:
                pos = Event2AttendClassTargetArry[0];
                break;
            case RandomEventAct.Event2Stand:
                pos = Event2AttendClassTargetArry[0];
                break;
            case RandomEventAct.Event3Sleep:
                pos = Event3SwitchDoorArry[0];
                break;
            case RandomEventAct.Event3SwitchDoor:
                pos = Event3SwitchDoorArry[0];
                break;
            case RandomEventAct.Event4AttendClass:
                pos = Event4AttendClassTargetArry[0];
                break;
            /*case RandomEventAct.Event5Sleep:
                pos = Event5SwitchDoorArry[0];
                break;
            case RandomEventAct.Event5SwitchDoor:
                pos = Event5SwitchDoorArry[0];
                break;*/
            case RandomEventAct.Event6Toilet:
                pos = Event6ToiletTargetArry[0];
                break;
            case RandomEventAct.Event7Bathe:
                pos = Event7BatheTargetArry[0];
                break;
            case RandomEventAct.Event8Toilet:
                pos = Event8ToiletTargetArry[0];
                break;
            case RandomEventAct.Event9Bathe:
                pos = Event9BatheTargetArry[0];
                break;
            case RandomEventAct.Event10Toilet:
                pos = Event10ToiletTargetArry[0];
                break;
            case RandomEventAct.Event12Laboratory:
                pos = Event12LaboratoryArry[0];
                break;
        
        }
        return pos;
    }

    #endregion
    
    #region 随机点位处理

    private Vector3[] IdleAndEvenRandom;

    private void InitIdleAndEvenRandom()
    {
        var IdleLen = IdlePointArry.Length;
        var OnePointLen = EventOnePointArry.Length;
        var TwoPointLen = EventTwoPointArry.Length;
        var len = IdleLen + OnePointLen + TwoPointLen;
        IdleAndEvenRandom = new Vector3[len];

        int arryIndex = 0;
        for (var i = 0; i < IdleLen; i++)
        {
            IdleAndEvenRandom[arryIndex] = IdlePointArry[i];
            arryIndex++;
        }
        
        for (var i = 0; i < OnePointLen; i++)
        {
            IdleAndEvenRandom[arryIndex] = EventOnePointArry[i];
            arryIndex++;
        }
        
        for (var i = 0; i < TwoPointLen; i++)
        {
            IdleAndEvenRandom[arryIndex] = EventTwoPointArry[i];
            arryIndex++;
        }
    }
    
    public Dictionary<int, Vector3> GetIdleAndEvenRandom()
    {
        var randomInfoDic = new Dictionary<int, Vector3>();

        var randomId = RoleIdleAndEvenRandom();
        
        if (randomInfoDic.ContainsKey(randomId))
        {
            randomInfoDic[randomId] = IdleAndEvenRandom[randomId];
        }
        else
        {
            randomInfoDic.Add(randomId, IdleAndEvenRandom[randomId]);
        }
        
        return randomInfoDic;
    }
    
    public Dictionary<int, Vector3> GetIdleRandom()
    {
        Dictionary<int, Vector3> randomInfoDic = new Dictionary<int, Vector3>();
       
        var index = UnityEngine.Random.Range(0, IdlePointArry.Length);
        
        if (randomInfoDic.ContainsKey((int)RandomArea.Idle))
        {
            randomInfoDic[(int)RandomArea.Idle] = IdlePointArry[index];
        }
        else
        {
            randomInfoDic.Add((int)RandomArea.Idle,IdlePointArry[index]);
        }

        return randomInfoDic;
    }


    private int RoleIdleAndEvenRandom()
    {
       var roleRandomId = GetRoleIdleAndEvenRandom();
       var len = SceneObjMgr.Instance.roleList.Count;
       var roleList = SceneObjMgr.Instance.roleList;
       for (var i = 0; i < len; i++)
       {
           if (roleList[i].roleRandomId == roleRandomId)
           {
               RoleIdleAndEvenRandom();
           }
       }
       return roleRandomId;
    }

    private int GetRoleIdleAndEvenRandom()
    {
        return UnityEngine.Random.Range(0, IdleAndEvenRandom.Length);
    }
    
    #endregion
    
    
    #region 角色到主城 前往休息区

    // public Vector3[] GetMainIdleOnePath()
    // {
    //     return GetIdelRandomPath(idleOneRandomArry,idleOnePathArry);
    // }
    //
    // public Vector3[] GetMainIdleTwoPath()
    // {
    //     return GetIdelRandomPath(idleTwoRandomArry,idleTwoPathArry);
    // }

    private Vector3[] GetIdelRandomPath(Vector3[] randomArry, Vector3[] pathArry)
    {
        var index = UnityEngine.Random.Range(0, randomArry.Length);
        var len = pathArry.Length;
        var paths = new Vector3[(len + 1)];

        for (int i = 0; i < len; i++)
        {
            paths[i] = pathArry[i];
        }
        
        paths[paths.Length -1] = randomArry[index];
        return paths;
    }
    
    #endregion

    #region 返回休闲区

    // public Vector3[] GetBackIdleOnePath(Vector3[] pathArry)
    // {
    //     return GetIdelRandomPath(idleOneRandomArry,pathArry);
    // }
    //
    // public Vector3[] GetBackIdleTwoPath(Vector3[] pathArry)
    // {
    //     return GetIdelRandomPath(idleTwoRandomArry,pathArry);
    // }

    #endregion
   
    /// <summary>
    /// 获取角色 出生点
    /// </summary>
    public Vector3 GetCreatePoint()
    {
        return GetRandomPoint(createArry);
    }

    public Vector3 GetRandomPoint(Vector3[] arry)
    {
        var index = UnityEngine.Random.Range(0, arry.Length);
        return arry[index];
    }
    
    public Vector3[] GetMainPath()
    {
        var index = UnityEngine.Random.Range(0, 2);
        Vector3[] mainPath = new Vector3[mainPathArry.Length - 1];

        int mainIndex = 0;
        for (int i = 0; i < mainPathArry.Length; i++)
        {
            if (index == 0 && i == 2)
                continue;

            if (index == 1 && i == 3)
                continue;
            
            mainPath[mainIndex] = mainPathArry[i];
            mainIndex++;
        }
        return mainPath;
    }

    public Vector3[] GetTwoMainPath()
    {
        return twoMainPathArry;
    }
    
    public Vector3[] GetStepsOne()
    {
        return stepsOneArry;
    }
    
    public Vector3[] GetStepsTwo()
    {
        return stepsTwoArry;
    }
    
}