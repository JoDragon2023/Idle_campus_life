using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public partial class Role : IStateMachineObj
{
    public float arriveDistance = 0.1f;
    private int pathIndex;

    public StateMachine stateMachine;
    public float rotSpeed = 180;

    public float moveSpeed = 3f;
    private List<Point> m_movePath;

    private Action moveCallBack;
    private Action enterIdleCallBack;

    #region 接口实现

    public GameObject gameObject => go;

    public StateMachine SMachine => stateMachine;

    public Action MoveCallBack
    {
        get { return moveCallBack; }
        set { moveCallBack = value; }
    }

    public Action EnterIdleCallBack
    {
        get { return enterIdleCallBack; }
        set { enterIdleCallBack = value; }
    }

    #endregion

    private void InitState()
    {
        InitStateMachine();
    }

    private void InitStateMachine()
    {
        var idle = new IdleState(EnterIdle, ExitIdle, UpdateIdle);
        var run = new RunState(EnterRun, ExitRun, UpdateRun);
        var talk = new TalkState(EnterTalk, ExitTalk, UpdateTalk);
        var sit = new SitState(EnterSit, ExitSit, UpdateSit);
        // var headAct = new HeadActState(EnterHeadAct, ExitHeadAct, UpdateHeadAct);
        // var loseGarbage = new LoseGarbageState(EnterLoseGarbageAct, ExitLoseGarbageAct, UpdateLoseGarbageAct);
        var eatDrink = new EatDrinkState(EnterEatDrinkAct, ExitEatDrinkAct, UpdateEatDrinkAct);
        var toilet = new ToiletState(EnterToiletAct, ExitToiletAct, UpdateToiletAct);
        var bathe = new BatheState(EnterBatheAct, ExitBatheAct, UpdateBatheAct);
        var urination = new UrinationState(EnterUrinationAct, ExitUrinationAct, UpdateUrinationAct);
        var attendClassS = new AttendClassState(EnterAttendClassAct, ExitAttendClassAct, UpdateAttendClassAct);
        var stand = new StandState(EnterStandAct, ExitStandAct, UpdateStandAct);
        var switchDoor = new SwitchDoorState(EnterSwitchDoorAct, ExitSwitchDoorAct, UpdateSwitchDoorAct);
        var sleep = new SleepState(EnterSleepAct, ExitSleepAct, UpdateSleepAct);
        stateMachine = new StateMachine(idle);
        stateMachine.InitState(run);
        stateMachine.InitState(talk);
        stateMachine.InitState(sit);
        // stateMachine.InitState(headAct);
        // stateMachine.InitState(loseGarbage);
        stateMachine.InitState(eatDrink);
        stateMachine.InitState(toilet);
        stateMachine.InitState(bathe);
        stateMachine.InitState(urination);
        stateMachine.InitState(attendClassS);
        stateMachine.InitState(stand);
        stateMachine.InitState(switchDoor);
        stateMachine.InitState(sleep);

        //
        // switch (currRoleType)
        // {
        //     case RoleType.None:
        //         break;
        //     case RoleType.Student:
        //         var sleep = new SleepState(EnterSleepAct, ExitSleepAct, UpdateSleepAct);
        //         var attendClassS = new AttendClassState(EnterAttendClassAct, ExitAttendClassAct, UpdateAttendClassAct);
        //         var switchDoor = new SwitchDoorState(EnterSwitchDoorAct, ExitSwitchDoorAct, UpdateSwitchDoorAct);
        //         stateMachine.InitState(sleep);
        //         stateMachine.InitState(attendClassS);
        //         stateMachine.InitState(switchDoor);
        //         break;
        //     case RoleType.Teacher:
        //         var attendClassT = new AttendClassState(EnterAttendClassAct, ExitAttendClassAct, UpdateAttendClassAct);
        //         stateMachine.InitState(attendClassT);
        //         break;
        //     case RoleType.Handyman:
        //         var cleanGarbage = new CleanGarbageState(EnterCleanGarbageAct, ExitCleanGarbageAct, UpdateCleanGarbageAct);
        //         stateMachine.InitState(cleanGarbage);
        //         break;
        //     case RoleType.Fettler:
        //         var repair = new RepairState(EnterRepairAct, ExitRepairAct, UpdateRepairAct);
        //         stateMachine.InitState(repair);
        //         break;
        // }

        stateMachine.TranslateNextState();
    }

    public int roleRandomId = -1;
    
    // ReSharper disable Unity.PerformanceAnalysis
    /// <summary>
    /// 执行随机
    /// </summary>
    private void GetAllRandom(bool isAll = true)
    {
        var randomDic = new Dictionary<int, Vector3>();
        //开始随机位置点。
        // if (isAll)
        // {
        //     randomDic = ScenePoint.Instance.GetIdleAndEvenRandom();
        // }
        // else
        // {
        //     randomDic = ScenePoint.Instance.GetIdleRandom();
        // }
        
        randomDic = ScenePoint.Instance.GetIdleAndEvenRandom();

        var randomKey = 0;
        var randomValue = Vector3.zero;
        foreach (var randomItem in randomDic)
        {
            randomKey = randomItem.Key;
            randomValue = randomItem.Value;
        }

        if (randomKey < 12)
        {
            currRandomArea = RandomArea.Idle;
        }
        else if (randomKey > 11 && randomKey < 18)
        {
            currRandomArea = RandomArea.EventOne;
        }
        else if (randomKey > 17 && randomKey < 27)
        {
            currRandomArea = RandomArea.EventTwo;
        }
        
        // currRandomArea = RandomArea.EventTwo;
        // if (!isAll)
        // {
        //     currRandomArea = RandomArea.Idle;
        // }
        
        currRandomTarget = randomValue;
    }

    /// <summary>
    /// 随机事件
    /// </summary>
    public int GetNextRandomEvent()
    {
        List<int> randomEvent = new List<int>();
        if (currRandomArea == RandomArea.EventOne)
        {
            randomEvent.Add((int)RandomEvent.Event3);
            randomEvent.Add((int)RandomEvent.Event1);
            randomEvent.Add((int)RandomEvent.Event2);
            randomEvent.Add((int)RandomEvent.Event4);
            randomEvent.Add((int)RandomEvent.Event6);
            randomEvent.Add((int)RandomEvent.Event7);
            randomEvent.Add((int)RandomEvent.Event10);
            randomEvent.Add((int)RandomEvent.Event11);
            //randomEvent.Add((int)RandomEvent.Event5);
        }
        else if (currRandomArea == RandomArea.EventTwo)
        {
            randomEvent.Add((int)RandomEvent.Event2);
            randomEvent.Add((int)RandomEvent.Event4);
            //randomEvent.Add((int)RandomEvent.Event5);
            randomEvent.Add((int)RandomEvent.Event8);
            randomEvent.Add((int)RandomEvent.Event9);
            randomEvent.Add((int)RandomEvent.Event10);
            randomEvent.Add((int)RandomEvent.Event11);
        }

        var index = Random.Range(0, randomEvent.Count);
        return randomEvent[index];
    }

    /// <summary>
    /// 根据随机事件 获取随机行为
    /// </summary>
    private int GetEventRandomAct(RandomEvent randomEvent)
    {
        //根据不同的行为，处理玩家不同的事情。
        List<int> randomEventList = new List<int>();
        switch (randomEvent)
        {
            case RandomEvent.None:
                break;
            case RandomEvent.Event1:
                randomEventList.Add((int)RandomEventAct.Event1Sit);
                randomEventList.Add((int)RandomEventAct.Event1EatDrink);
                break;
            case RandomEvent.Event2:
                randomEventList.Add((int)RandomEventAct.Event2AttendClass);
                randomEventList.Add((int)RandomEventAct.Event2Stand);
                break;
            case RandomEvent.Event3:
                randomEventList.Add((int)RandomEventAct.Event3Sleep);
                randomEventList.Add((int)RandomEventAct.Event3SwitchDoor);
                break;
            case RandomEvent.Event4:
                randomEventList.Add((int)RandomEventAct.Event4AttendClass);
                break;
            case RandomEvent.Event5:
                randomEventList.Add((int)RandomEventAct.Event5Sleep);
                randomEventList.Add((int)RandomEventAct.Event5SwitchDoor);
                break;
            case RandomEvent.Event6:
                randomEventList.Add((int)RandomEventAct.Event6Toilet);
                break;
            case RandomEvent.Event7:
                randomEventList.Add((int)RandomEventAct.Event7Bathe);
                break;
            case RandomEvent.Event8:
                randomEventList.Add((int)RandomEventAct.Event8Toilet);
                break;
            case RandomEvent.Event9:
                randomEventList.Add((int)RandomEventAct.Event9Bathe);
                break;
            case RandomEvent.Event10:
                randomEventList.Add((int)RandomEventAct.Event10Toilet);
                break;
            case RandomEvent.Event11:
                randomEventList.Add((int)RandomEventAct.Event11Bathe);
                break;
        }

        var index = Random.Range(0, randomEventList.Count);
        return randomEventList[index];
    }


    
    
    // ReSharper disable Unity.PerformanceAnalysis
    private void leaveState()
    {
        animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
        currRandomEventAct = RandomEventAct.Idle;
        GetAllRandom(false);
        SMachine.TranslateState(RoleState.Run);
    }
    
    public void PlayAnim(RoleState roleState)
    {
        if (animator == null)
            return;

        // string curAnim = GetAnimName(roleState);
        // animator.CrossFade(curAnim, 0.2f);
    }

    public bool HitTest(Vector3 v3)
    {
        // if (renderer == null)
        //     return false;
        //
        // if (!renderer.isVisible)
        //     return false;
        //
        // v3.z = go.transform.position.z;

        //return renderer.bounds.Contains(v3);
        return false;
    }

    void ReleaseState()
    {
        //ScreenClickMgr.I.DelState(this);
        stateMachine.Release();
        stateMachine = null;
    }
}