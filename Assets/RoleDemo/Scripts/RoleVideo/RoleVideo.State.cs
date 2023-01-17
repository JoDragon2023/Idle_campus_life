using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public partial class RoleVideo : IStateMachineObj
{
    private int pathIndex;
    public StateMachine stateMachine;
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
        var goToClass = new GoToClassState(EnterGoToClassAct, ExitGoToClassAct, UpdateGoToClassAct);
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
        stateMachine.InitState(goToClass);
        stateMachine.TranslateNextState();
    }

    public void PlayAnim(RoleState roleState)
    {
        if (animator == null)
            return;
        // string curAnim = GetAnimName(roleState);
        // animator.CrossFade(curAnim, 0.2f);
    }

   
    void ReleaseState()
    {
        //ScreenClickMgr.I.DelState(this);
        stateMachine.Release();
        stateMachine = null;
    }
        
}