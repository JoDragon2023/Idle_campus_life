using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public partial class Role
{
    /// <summary>
    /// 当前 随机的事件
    /// </summary>
    public RandomEvent currRandomEvent = RandomEvent.None;
    /// <summary>
    /// 当前 随机的行为
    /// </summary>
    public RandomEventAct currRandomEventAct = RandomEventAct.Main;
    /// <summary>
    /// 当前 随机的路径
    /// </summary>
    public EventRandomPath curRandomPath = EventRandomPath.None;

    /// <summary>
    /// 这个是给路径找不到的时候，切换状态用的。
    /// </summary>
    private bool isPathLeaveState;
    
    #region 休闲状态
    
    private float idleTime = 3;
    private float durTime;
    public void EnterIdle()
    {
        durTime = 0;
        SetAiDestination(false);
        animator.SetBool(ToAnimatorCondition.ToStand.ToString(), true);
    }

    public void UpdateIdle(float deltaTime)
    {
        currRoleAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Stand.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Stand);
        }

        durTime += deltaTime;
        if (durTime > idleTime)
        {
            durTime = 0;
            switch (currRandomArea)
            {
                case RandomArea.Idle:
                    currRandomEventAct = RandomEventAct.Idle;
                    GetAllRandom(true);
                    SMachine.TranslateState(RoleState.Run);
                    break;
                case RandomArea.EventOne:
                case RandomArea.EventTwo:
                    currRandomEvent = (RandomEvent)GetNextRandomEvent();
                    currRandomEventAct = (RandomEventAct)GetEventRandomAct(currRandomEvent);
                    SMachine.TranslateState(RoleState.Run);
                    break;
            }
        }
    }

    public void ExitIdle()
    {
        animator.SetBool(ToAnimatorCondition.ToStand.ToString(), false);
    }

    #endregion

    #region 行走状态

    private RandomArea currRandomArea = RandomArea.None;
    private Vector3 currRandomTarget;
    private bool isRunUpdate = false;
    private float distance;
    //行走动画条件
    private string runCondition;
    
    // ReSharper disable Unity.PerformanceAnalysis
    public void EnterRun()
    {
        isRunUpdate = true;
        RunPath();
        GetRunAnimation();
        animator.SetBool(runCondition, true);
    }

    private void RunPath()
    {
        switch (currRandomEventAct)
        {
            case RandomEventAct.Main:
                RunMainPath();
                break;
            case RandomEventAct.Idle:
                SetTargetPos(currRandomTarget);
                SetAIComponent(true);
                break;
            case RandomEventAct.Event1Sit:
            case RandomEventAct.Event1EatDrink:
            case RandomEventAct.Event2AttendClass:
            case RandomEventAct.Event2Stand:
            case RandomEventAct.Event3Sleep:
            case RandomEventAct.Event3SwitchDoor:
            case RandomEventAct.Event4AttendClass:
            case RandomEventAct.Event5Sleep:
            case RandomEventAct.Event5SwitchDoor:
            case RandomEventAct.Event6Toilet:
            case RandomEventAct.Event7Bathe:
            case RandomEventAct.Event8Toilet:
            case RandomEventAct.Event9Bathe:
            case RandomEventAct.Event10Toilet:
            case RandomEventAct.Event11Bathe:
                currRandomTarget = ScenePoint.Instance.GetRandomEventActPoint(currRandomEventAct);
                SetTargetPos(currRandomTarget);
                SetAIComponent(true);
                break;
            case RandomEventAct.Event1Talk:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

  
    
    private void OnOneRunComplete()
    {
        //开始随机位置点。
        GetAllRandom(false);
        SetTargetPos(currRandomTarget);
        SetAIComponent(true);
        isRunUpdate = true;
    }
    
    private void RunAnimatorSetInteger()
    {
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Run.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Run);
        }
        
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Walk_01.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Run);
        }
        
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Walk_02.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Run);
        }
    }
    
    private void GetRunAnimation()
    {
        var index = Random.Range(1, 4);
        var toAnimatorCondition = (ToAnimatorCondition)index;
        runCondition = toAnimatorCondition.ToString();
    }
    

    public void UpdateRun(float deltaTime)
    {
        currRoleAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        RunAnimatorSetInteger();

        if (isRunUpdate)
        {
            if (go != null && target != null)
            {
                distance = (go.transform.position - target.transform.position).magnitude;
                if (distance < 0.5f)
                {
                    isRunUpdate = false;
                    RunTranslateState();
                }
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void RunTranslateState()
    {
        switch (currRandomEventAct)
        {
            case RandomEventAct.None:
                break;
            case RandomEventAct.Main:
                SMachine.TranslateState(RoleState.Idle);
                break;
            case RandomEventAct.Idle:
                SMachine.TranslateState(RoleState.Idle);
                break;
            case RandomEventAct.Event1Sit:
                SMachine.TranslateState(RoleState.Sit);
                break;
            case RandomEventAct.Event1Talk:
                break;
            case RandomEventAct.Event1EatDrink:
                SMachine.TranslateState(RoleState.EatDrink);
                break;
            case RandomEventAct.Event2AttendClass:
                SMachine.TranslateState(RoleState.AttendClass);
                break;
            case RandomEventAct.Event2Stand:
                SMachine.TranslateState(RoleState.Stand);
                break;
            case RandomEventAct.Event3Sleep:
                SMachine.TranslateState(RoleState.Sleep);
                break;
            case RandomEventAct.Event3SwitchDoor:
                SMachine.TranslateState(RoleState.SwitchDoor);
                break;
            case RandomEventAct.Event4AttendClass:
                SMachine.TranslateState(RoleState.AttendClass);
                break;
            case RandomEventAct.Event5Sleep:
                SMachine.TranslateState(RoleState.Sleep);
                break;
            case RandomEventAct.Event5SwitchDoor:
                SMachine.TranslateState(RoleState.SwitchDoor);
                break;
            case RandomEventAct.Event6Toilet:
                SMachine.TranslateState(RoleState.Toilet);
                break;
            case RandomEventAct.Event7Bathe:
                SMachine.TranslateState(RoleState.Bathe);
                break;
            case RandomEventAct.Event8Toilet:
                SMachine.TranslateState(RoleState.Toilet);
                break;
            case RandomEventAct.Event9Bathe:
                SMachine.TranslateState(RoleState.Bathe);
                break;
            case RandomEventAct.Event10Toilet:
                SMachine.TranslateState(RoleState.Toilet);
                break;
            case RandomEventAct.Event11Bathe:
                SMachine.TranslateState(RoleState.Bathe);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void ExitRun()
    {
        animator.SetBool(runCondition, false);
    }

    #region Main 角色出生行走路线

    private void RunMainPath()
    {
        go.transform.DOPath(ScenePoint.Instance.GetMainPath(), 13, PathType.CatmullRom).SetEase(Ease.Linear).SetLookAt(0)
            .onComplete = () =>
        {
            go.transform.position = new Vector3(go.transform.position.x, 0.25f, go.transform.position.z);
            go.transform.DOPath(ScenePoint.Instance.GetStepsOne(), 0.6f, PathType.CatmullRom).SetEase(Ease.Linear)
                .SetLookAt(0)
                .onComplete = () =>
            {
                go.transform.position = new Vector3(go.transform.position.x, 0.56f, go.transform.position.z);
                go.transform.DOPath(ScenePoint.Instance.GetStepsTwo(), 1f, PathType.CatmullRom).SetEase(Ease.Linear)
                    .SetLookAt(0).onComplete = OnOneRunComplete;
            };
        };
    }

    #endregion

    #endregion

    #region 坐下状态

    private Vector3[] currEventSitPath;
    private int sitRotate = 180;
    private float sitTime = 0;
    private float nextSitTime = 4;
    private bool isEventSit = false;
    private bool isleaveSit = false;

    public void EnterSit()
    {
        isEventSit = false;
        isleaveSit = false;
        durTime = 0;
        isPathLeaveState = false;
        GetSitPath();
        EntrySit();
    }

    public void UpdateSit(float deltaTime)
    {
        if (curRandomPath == EventRandomPath.None)
        {
            if (!isPathLeaveState)
            {
                isPathLeaveState = true;
                leaveState();
                return;
            }
        }
        
        currRoleAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Sitdown.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Sitdown);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                isEventSit = true;
            }
        }

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Walk_01.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Run);
        }

        if (isEventSit)
        {
            durTime += deltaTime;
            if (durTime > nextSitTime)
            {
                if (!isleaveSit)
                {
                    isleaveSit = true;
                    isEventSit = false;
                    durTime = 0;
                    leaveSit();
                }
            }
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void GetSitPath()
    {
        GetRolePath(4);
        if (curRandomPath == EventRandomPath.None) return;
        
        Vector3[] randomArry = null ;
        if (currRandomEventAct == RandomEventAct.Event1Sit)
        {
            randomArry = ScenePath.Instance.GetEvent1SitPath(curRandomPath);
        }

        currEventSitPath = randomArry;
    }

    private void EntrySit()
    {
        if (curRandomPath == EventRandomPath.None) return;
        
        if (currRandomEventAct == RandomEventAct.Event1Sit)
        {
            switch (curRandomPath)
            {
                case EventRandomPath.Path1:
                    sitRotate = 260;
                    sitTime = 3f;
                    break;
                case EventRandomPath.Path2:
                    sitRotate = 0;
                    sitTime = 5f;
                    break;
                case EventRandomPath.Path3:
                    sitRotate = 180;
                    sitTime = 1f;
                    break;
            }
        }

        SetAIComponent(false);
        animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), true);
        go.transform.DOPath(currEventSitPath, sitTime, PathType.CatmullRom).SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            go.transform.DORotate(new Vector3(0, sitRotate, 0), 0.2f, RotateMode.Fast).onComplete = () =>
            {
                animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
                animator.SetBool(ToAnimatorCondition.ToSitdown.ToString(), true);
            };
        };
    }

    private void leaveSit()
    {
        animator.SetBool(ToAnimatorCondition.ToSitdown.ToString(), false);
        animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), true);
        var leavePoint = ScenePoint.Instance.GetRandomEventActPoint(RandomEventAct.Event1Sit);
        if (currRandomEventAct == RandomEventAct.Event1Sit)
        {
            leavePoint = ScenePoint.Instance.GetRandomEventActPoint(RandomEventAct.Event1Sit);
        }

        go.transform.DOPath(ScenePath.Instance.GetBackPath(currEventSitPath, leavePoint), sitTime, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
            currRandomEventAct = RandomEventAct.Idle;
            GetAllRandom();
            SMachine.TranslateState(RoleState.Run);
        };
    }

    public void ExitSit()
    {
        durTime = 0;
        animator.SetBool(ToAnimatorCondition.ToSitdown.ToString(), false);
    }

    #endregion

    #region 站立

    private Vector3[] currEventStandPath;
    private bool isleaveStand = false;
    private bool isUpdateStand = false;
    private int standRotate = 90;
    private float standTime = 1f;

    public void EnterStandAct()
    {
        isleaveStand = false;
        isUpdateStand = false;
        isPathLeaveState = false;
        durTime = 0;
        GetStandPath();
        EntryStand();
    }

    public void UpdateStandAct(float deltaTime)
    {
        if (curRandomPath == EventRandomPath.None)
        {
            if (!isPathLeaveState)
            {
                isPathLeaveState = true;
                leaveState();
                return;
            }
        }
        
        currRoleAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Stand.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Stand);
        }

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Walk_01.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Run);
        }

        if (isUpdateStand)
        {
            durTime += deltaTime;
            if (durTime > idleTime)
            {
                if (!isleaveStand)
                {
                    isleaveStand = true;
                    isUpdateStand = false;
                    durTime = 0;
                    leaveStand();
                }
            }
        }
    }

    private void EntryStand()
    {
        if (curRandomPath == EventRandomPath.None) return;
        
        standTime = 2.5f;
        standRotate = 90;
        if (curRandomPath == EventRandomPath.Path2)
        {
            standTime = 3f;
            standRotate = 180;
        }

        SetAIComponent(false);
        animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), true);
        go.transform.DOPath(currEventStandPath, standTime, PathType.CatmullRom).SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            go.transform.DORotate(new Vector3(0, standRotate, 0), 0.2f, RotateMode.Fast).onComplete = () =>
            {
                animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
                animator.SetBool(ToAnimatorCondition.ToStand.ToString(), true);
                isUpdateStand = true;
            };
        };
    }

    private void leaveStand()
    {
        animator.SetBool(ToAnimatorCondition.ToStand.ToString(), false);
        animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), true);
        var leavePoint = ScenePoint.Instance.GetRandomEventActPoint(RandomEventAct.Event2Stand);
        go.transform
            .DOPath(ScenePath.Instance.GetBackPath(currEventStandPath, leavePoint), standTime, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
            currRandomEventAct = RandomEventAct.Idle;
            GetAllRandom();
            SMachine.TranslateState(RoleState.Run);
        };
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void GetStandPath()
    {
        GetRolePath(3);
        if (curRandomPath == EventRandomPath.None) return;
        var randomArry = ScenePath.Instance.GetEvent2StandPath(curRandomPath);
        currEventStandPath = randomArry;
    }

    public void ExitStandAct()
    {
    }

    #endregion

    #region 吃喝

    private Vector3[] currEventEatDrinkPath;
    private bool isleaveEatDrink = false;
    private int eatDrinkRotate = 90;
    private float eatDrinkTime = 1f;

    public void EnterEatDrinkAct()
    {
        isleaveEatDrink = false;
        isPathLeaveState = false;
        GetEatDrinkPath();
        EntryEatDrink();
    }

    public void UpdateEatDrinkAct(float deltaTime)
    {
        if (curRandomPath == EventRandomPath.None)
        {
            if (!isPathLeaveState)
            {
                isPathLeaveState = true;
                leaveState();
                return;
            }
        }
        
        currRoleAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Drink.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Drink);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isleaveEatDrink)
                {
                    isleaveEatDrink = true;
                    leaveEatDrink();
                }
            }
        }

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Walk_01.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Run);
        }
    }


    private void EntryEatDrink()
    {
        if (curRandomPath == EventRandomPath.None) return;
        
        SetAIComponent(false);
        animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), true);
        go.transform.DOPath(currEventEatDrinkPath, eatDrinkTime, PathType.CatmullRom).SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            go.transform.DORotate(new Vector3(0, eatDrinkRotate, 0), 0.2f, RotateMode.Fast).onComplete = () =>
            {
                animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
                animator.SetBool(ToAnimatorCondition.ToDrink.ToString(), true);
            };
        };
    }

    private void leaveEatDrink()
    {
        animator.SetBool(ToAnimatorCondition.ToDrink.ToString(), false);
        animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), true);
        var leavePoint = ScenePoint.Instance.GetRandomEventActPoint(RandomEventAct.Event1EatDrink);
        go.transform.DOPath(ScenePath.Instance.GetBackPath(currEventEatDrinkPath, leavePoint), eatDrinkTime,
                PathType.CatmullRom).SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
            currRandomEventAct = RandomEventAct.Idle;
            GetAllRandom();
            SMachine.TranslateState(RoleState.Run);
        };
    }


    // ReSharper disable Unity.PerformanceAnalysis
    private void GetEatDrinkPath()
    {
        GetRolePath(3);
        if (curRandomPath == EventRandomPath.None) return;
        currEventEatDrinkPath = ScenePath.Instance.GetEvent1EatDrinkPath(curRandomPath);
    }


    public void ExitEatDrinkAct()
    {
    }

    #endregion

    #region 上厕所

    private bool isToiletOne = false;
    private bool isToiletTwo = false;
    private bool isToiletThree = false;
    private Vector3[] toiletPath;
    private int rotate = 180;

    public void EnterToiletAct()
    {
        isToiletOne = false;
        isToiletTwo = false;
        isToiletThree = false;
        isPathLeaveState = false;
        EnterToilet();
    }

    public void UpdateToiletAct(float deltaTime)
    {
        if (curRandomPath == EventRandomPath.None)
        {
            if (!isPathLeaveState)
            {
                isPathLeaveState = true;
                leaveState();
                return;
            }
        }
        
        currRoleAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.ToiletOne.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Toilet);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isToiletOne)
                {
                    isToiletOne = true;
                    animator.SetBool(ToAnimatorCondition.ToOpenDoor_Toilet.ToString(), false);
                    animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), true);
                    EntryToiletAnim();
                }
            }
        }

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.ToiletTwo.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Toilet);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isToiletTwo)
                {
                    isToiletTwo = true;
                    animator.SetBool(ToAnimatorCondition.ToToilet.ToString(), false);
                    animator.SetBool(ToAnimatorCondition.ToToilet_GetUp.ToString(), true);
                }
            }
        }

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.ToiletThree.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.ToiletGetUP);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isToiletThree)
                {
                    isToiletThree = true;
                    animator.SetBool(ToAnimatorCondition.ToToilet_GetUp.ToString(), false);
                    animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), true);
                    leaveToilet();
                }
            }
        }

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Walk_01.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Run);
        }
    }

    private void leaveToilet()
    {
        var leavePoint = ScenePoint.Instance.GetRandomEventActPoint(RandomEventAct.Event8Toilet);

        if (currRandomEventAct == RandomEventAct.Event6Toilet)
        {
            leavePoint = ScenePoint.Instance.GetRandomEventActPoint(RandomEventAct.Event6Toilet);
        }

        if (currRandomEventAct == RandomEventAct.Event10Toilet)
        {
            leavePoint = ScenePoint.Instance.GetRandomEventActPoint(RandomEventAct.Event10Toilet);
        }

        go.transform.DOPath(ScenePath.Instance.GetBackPath(toiletPath, leavePoint), 2f, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            currRandomEventAct = RandomEventAct.Idle;
            GetAllRandom();
            animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
            SMachine.TranslateState(RoleState.Run);
        };
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void EnterToilet()
    {
        GetRolePath(5);
        if (curRandomPath == EventRandomPath.None) return;
        SetAIComponent(false);
        var randomAry = ScenePath.Instance.GetEvent8ToiletPath(curRandomPath);
        if (currRandomEventAct == RandomEventAct.Event6Toilet)
        {
            randomAry = ScenePath.Instance.GetEvent6ToiletPath(curRandomPath);
        }
        if (currRandomEventAct == RandomEventAct.Event10Toilet)
        {
            randomAry = ScenePath.Instance.GetEvent10ToiletPath(curRandomPath);
        }

        toiletPath = randomAry;
        
        go.transform.DOPath(ScenePath.Instance.GetEventToiletActPath(toiletPath), 1.5f, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            animator.SetBool(ToAnimatorCondition.ToOpenDoor_Toilet.ToString(), true);
        };
    }

    /// <summary>
    /// 进入厕所
    /// </summary>
    private void EntryToiletAnim()
    {
        rotate = 180;
        if (currRandomEventAct == RandomEventAct.Event6Toilet)
            rotate = 0;

        if (currRandomEventAct == RandomEventAct.Event10Toilet)
            rotate = 0;

        go.transform.DOPath(ScenePath.Instance.GetEventToiletActPoint(toiletPath), 0.5f, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            go.transform.DORotate(new Vector3(0, rotate, 0), 0.2f, RotateMode.Fast).onComplete = () =>
            {
                animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
                animator.SetBool(ToAnimatorCondition.ToToilet.ToString(), true);
            };
        };
    }

    public void ExitToiletAct()
    {
        animator.SetBool(ToAnimatorCondition.ToToilet.ToString(), false);
    }

    #endregion
    
    #region 洗澡

    private bool isBatheOne = false;
    private bool isBatheTwo = false;
    private bool isBatheThree = false;
    private bool isBatheFour = false;
    private Vector3[] bathePath;
    private int batheRotate = 180;
    private int batheRotateDoor = 180;
    private BatheStateAnim batheStateAnim;

    public void EnterBatheAct()
    {
        isBatheOne = false;
        isBatheTwo = false;
        isBatheThree = false;
        isBatheFour = false;
        isPathLeaveState = false;
        batheStateAnim = BatheStateAnim.Open;
        EnterBathe();
    }

    public void UpdateBatheAct(float deltaTime)
    {
        
        if (curRandomPath == EventRandomPath.None)
        {
            if (!isPathLeaveState)
            {
                isPathLeaveState = true;
                leaveState();
                return;
            }
        }
        
        currRoleAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.BatheOne.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.BatheOne);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isBatheOne)
                {
                    isBatheOne = true;
                    if (batheStateAnim == BatheStateAnim.Open)
                    {
                        animator.SetBool(ToAnimatorCondition.ToBatheOne.ToString(), false);
                        animator.SetBool(ToAnimatorCondition.ToBatheTwo.ToString(), true);
                    }
                    else if (batheStateAnim == BatheStateAnim.Close)
                    {
                        animator.SetBool(ToAnimatorCondition.ToBatheOne.ToString(), false);
                        animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), true);
                        leaveBathe();
                    }
                }
            }
        }
        
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.BatheTwo.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.BatheTwo);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isBatheTwo)
                {
                    isBatheTwo = true;
                    animator.SetBool(ToAnimatorCondition.ToBatheTwo.ToString(), false);
                    animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), true);
                    EntryBatheAnim();
                }
            }
        }

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.BatheThree.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.BatheThree);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isBatheThree)
                {
                    isBatheThree = true;
                    animator.SetBool(ToAnimatorCondition.ToBatheThree.ToString(), false);
                    animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), true);
                    //往门口的点走
                    leaveBatheDoor();
                }
            }
        }

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.BatheFour.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.BatheFour);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isBatheFour)
                {
                    isBatheFour = true;
                    isBatheOne = false;
                    batheStateAnim = BatheStateAnim.Close;
                    animator.SetBool(ToAnimatorCondition.ToBatheFour.ToString(), false);
                    animator.SetBool(ToAnimatorCondition.ToBatheOne.ToString(), true);
                }
            }
        }

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Walk_01.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Run);
        }
    }

    /// <summary>
    /// 走到洗澡间门口 播放关门动画
    /// </summary>
    private void leaveBatheDoor()
    {
        batheRotateDoor = 0;
        if (currRandomEventAct == RandomEventAct.Event7Bathe)
            batheRotateDoor = 180;
        
        if (currRandomEventAct == RandomEventAct.Event11Bathe)
            batheRotateDoor = 180;

        var leaveBathePos = ScenePath.Instance.GetEventToiletActPath(bathePath);
        go.transform.DOPath(ScenePath.Instance.GetEventToiletActPoint(leaveBathePos), 0.5f, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            go.transform.DORotate(new Vector3(0, batheRotateDoor, 0), 0.2f, RotateMode.Fast).onComplete = () =>
            {
                animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
                animator.SetBool(ToAnimatorCondition.ToBatheFour.ToString(), true);
            };
        };
    }
    
    
    /// <summary>
    /// 退出洗澡间
    /// </summary>
    private void leaveBathe()
    {
        var leavePoint = ScenePoint.Instance.GetRandomEventActPoint(RandomEventAct.Event9Bathe);

        if (currRandomEventAct == RandomEventAct.Event7Bathe)
        {
            leavePoint = ScenePoint.Instance.GetRandomEventActPoint(RandomEventAct.Event7Bathe);
        }

        if (currRandomEventAct == RandomEventAct.Event11Bathe)
        {
            leavePoint = ScenePoint.Instance.GetRandomEventActPoint(RandomEventAct.Event11Bathe);
        }

        go.transform.DOPath(ScenePath.Instance.GetBackBathePath(bathePath, leavePoint), 1.5f, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            currRandomEventAct = RandomEventAct.Idle;
            GetAllRandom();
            animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
            SMachine.TranslateState(RoleState.Run);
        };
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void EnterBathe()
    {
        GetRolePath(5);
        if (curRandomPath == EventRandomPath.None) return;
       
        SetAIComponent(false);
        var randomAry = ScenePath.Instance.GetEvent9BathePath(curRandomPath);
        if (currRandomEventAct == RandomEventAct.Event7Bathe)
        {
            randomAry = ScenePath.Instance.GetEvent7BathePath(curRandomPath);
        }

        if (currRandomEventAct == RandomEventAct.Event11Bathe)
        {
            randomAry = ScenePath.Instance.GetEvent11BathePath(curRandomPath);
        }

        bathePath = randomAry;
        
        go.transform.DOPath(ScenePath.Instance.GetEventToiletActPath(bathePath), 1.5f, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            animator.SetBool(ToAnimatorCondition.ToBatheOne.ToString(), true);
        };
    }

    /// <summary>
    /// 进入洗澡间
    /// </summary>
    private void EntryBatheAnim()
    {
        batheRotate = 180;
        if (currRandomEventAct == RandomEventAct.Event7Bathe)
            batheRotate = 0;

        else if (currRandomEventAct == RandomEventAct.Event11Bathe)
            batheRotate = 0;

        go.transform.DOPath(ScenePath.Instance.GetEventToiletActPoint(bathePath), 0.5f, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            go.transform.DORotate(new Vector3(0, batheRotate, 0), 0.2f, RotateMode.Fast).onComplete = () =>
            {
                animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
                animator.SetBool(ToAnimatorCondition.ToBatheThree.ToString(), true);
            };
        };
    }


    public void ExitBatheAct()
    {
        animator.SetBool(ToAnimatorCondition.ToToilet.ToString(), false);
    }

    #endregion

    #region 睡觉

    private bool isSleepOne = false;
    private bool isSleepTwo = false;
    private bool isSleepThree = false;
    private bool isUpdateSleep = false;
    private bool isStartSleep = false;
    
    private Vector3[] sleepPath;
    private int sleepRotate = 180;
    private float sleepTime = 1f;
    private float nextSleepTime = 10f;
    private BedAnim bedAnim;

    public void EnterSleepAct()
    {
        isSleepOne = false;
        isSleepTwo = false;
        isSleepThree = false;
        isUpdateSleep = false;
        isStartSleep = false;
        isPathLeaveState = false;
        GetSleepPath();
        EnterSleep();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void UpdateSleepAct(float deltaTime)
    {
        if (curRandomPath == EventRandomPath.None)
        {
            if (!isPathLeaveState)
            {
                isPathLeaveState = true;
                leaveState();
                return;
            }
        }

        if (bedAnim != null)
            bedAnim.UpdateBedAnim();
        
        currRoleAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.SleepOne.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.SleepOne);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isSleepOne)
                {
                    isSleepOne = true;
                    animator.SetBool(ToAnimatorCondition.ToSleepOne.ToString(), false);
                    animator.SetBool(ToAnimatorCondition.ToSleepTwo.ToString(), true);
                    
                    if (bedAnim != null)
                        bedAnim.PlayAnim(BedAnimatorName.BedTwo);
                }
            }
        }

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.SleepTwo.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.SleepTwo);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isSleepTwo)
                {
                    isSleepTwo = true;
                    isUpdateSleep = true;
                    animator.SetBool(ToAnimatorCondition.ToSleepTwo.ToString(), false);
                }
            }
        }

        if (isUpdateSleep)
        {
            durTime += deltaTime;
            if (durTime > nextSleepTime)
            {
                if (!isStartSleep)
                {
                    isStartSleep = true;
                    isUpdateSleep = false;
                    durTime = 0;
                    animator.SetBool(ToAnimatorCondition.ToSleepThree.ToString(), true);
                    
                    if (bedAnim != null)
                        bedAnim.PlayAnim(BedAnimatorName.BedThree);
                }
            }
        }
        
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.SleepThree.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.SleepThree);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isSleepThree)
                {
                    isSleepThree = true;
                    animator.SetBool(ToAnimatorCondition.ToSleepThree.ToString(), false);
                    animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), true);
                    leaveSleep();
                }
            }
        }

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Walk_01.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Run);
        }
    }

    private void EnterSleep()
    {
        if (curRandomPath == EventRandomPath.None) return;
        
        SetAIComponent(false);

        sleepRotate = 180;
        // if (currRandomEventAct == RandomEventAct.Event3Sleep)
        //     sleepRotate = 90;

        switch (curRandomPath)
        {
            case EventRandomPath.Path1:
                sleepTime = 2f;
                break;
            case EventRandomPath.Path2:
                sleepTime = 2f;
                break;
            case EventRandomPath.Path3:
                sleepTime = 3f;
                break;
            case EventRandomPath.Path4:
                sleepTime = 3f;
                break;
        }

        go.transform.DOPath(sleepPath, sleepTime, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            go.transform.DORotate(new Vector3(0, sleepRotate, 0), 0.2f, RotateMode.Fast).onComplete = () =>
            {
                animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
                animator.SetBool(ToAnimatorCondition.ToSleepOne.ToString(), true);
                
                if (bedAnim != null)
                    bedAnim.PlayAnim(BedAnimatorName.BedOne);
            };
        };
    }


    /// <summary>
    /// 退出睡觉
    /// </summary>
    private void leaveSleep()
    {
        var leavePoint = ScenePoint.Instance.GetRandomEventActPoint(RandomEventAct.Event5Sleep);
        if (currRandomEventAct == RandomEventAct.Event3Sleep)
        {
            leavePoint = ScenePoint.Instance.GetRandomEventActPoint(RandomEventAct.Event3Sleep);
        }

        go.transform.DOPath(ScenePath.Instance.GetBackPath(sleepPath, leavePoint), sleepTime, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            currRandomEventAct = RandomEventAct.Idle;
            GetAllRandom();
            animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
            SMachine.TranslateState(RoleState.Run);
        };
    }


    // ReSharper disable Unity.PerformanceAnalysis
    private void GetSleepPath()
    {
       GetRolePath(5);
       if (curRandomPath == EventRandomPath.None) return;

       
       Vector3[] randomArry = null;
        if (currRandomEventAct == RandomEventAct.Event3Sleep)
        {
            bedAnim = GameManager.Instance.GetBedAnim(curRandomPath);
            randomArry = ScenePath.Instance.GeEvent3SleepPath(curRandomPath);
        }
        else if (currRandomEventAct == RandomEventAct.Event5Sleep)
        {
            randomArry = ScenePath.Instance.GeEvent5SleepPath(curRandomPath);
        }

        sleepPath = randomArry;
    }
    
    public void ExitSleepAct()
    {
    }

    #endregion

    #region 上课

    private Vector3[] currAttendClassPath;
    private bool isEventAttendClass;
    private bool isEventAttendClassIdle;
    private bool isleaveAttendClass;
    private bool isStudy;
    private float attendClassTime = 3;
    private float nextAttendClassTime = 5;
    private int attendClassRotate = 0;
    private string studyIdleStr;
    
    public void EnterAttendClassAct()
    {
        durTime = 0;
        isEventAttendClass = false;
        isleaveAttendClass = false;
        isEventAttendClassIdle = false;
        isPathLeaveState = false;
        isStudy = false;
        GetAttendClassPath();
        EntryAttendClass();
    }

    public void UpdateAttendClassAct(float deltaTime)
    {
        if (curRandomPath == EventRandomPath.None)
        {
            if (!isPathLeaveState)
            {
                isPathLeaveState = true;
                leaveState();
                return;
            }
        }
        
        currRoleAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Sitdown.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Sitdown);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                isEventAttendClass = true;
                animator.SetBool(ToAnimatorCondition.ToSitdown.ToString(), false);
                animator.SetBool(studyIdleStr, true);
            }
        }

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Walk_01.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Run);
        }

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.StudyIdle.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.StudyIdle);
        }
        
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.StudyCourseIdle.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.StudyIdle);
        }
        
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Study.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Study);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                isStudy = true;
                animator.SetBool(ToAnimatorCondition.ToStudy.ToString(), false);
                animator.SetBool(studyIdleStr, true);
            }
        }

        if (isEventAttendClass)
        {
            durTime += deltaTime;
            if (durTime > nextAttendClassTime)
            {
                if (!isEventAttendClassIdle)
                {
                    isEventAttendClass = false;
                    isEventAttendClassIdle = true;
                    durTime = 0;
                    animator.SetBool(studyIdleStr, false);
                    animator.SetBool(ToAnimatorCondition.ToStudy.ToString(), true);
                }
            }
        }

        if (isStudy)
        {
            durTime += deltaTime;
            if (durTime > nextAttendClassTime)
            {
                if (!isleaveAttendClass)
                {
                    isStudy = false;
                    isleaveAttendClass = true;
                    durTime = 0;
                    leaveAttendClass();
                }
            }
        }
    }

    private void EntryAttendClass()
    {
        if (curRandomPath == EventRandomPath.None) return;;

        studyIdleStr = ToAnimatorCondition.ToStudyCourseIdle.ToString();
        attendClassRotate = 0;
        if (currRandomEventAct == RandomEventAct.Event2AttendClass)
        {
            studyIdleStr = ToAnimatorCondition.ToStudyIdle.ToString();
            attendClassRotate = 90;
        }
        
        GetAttendClassPathTime();
        SetAIComponent(false);
        animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), true);
        go.transform.DOPath(currAttendClassPath, attendClassTime, PathType.CatmullRom).SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            go.transform.DORotate(new Vector3(0, attendClassRotate, 0), 0.2f, RotateMode.Fast).onComplete = () =>
            {
                animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
                animator.SetBool(ToAnimatorCondition.ToSitdown.ToString(), true);
            };
        };
    }

    private void leaveAttendClass()
    {
        animator.SetBool(studyIdleStr, false);
        animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), true);
        var leavePoint = ScenePoint.Instance.GetRandomEventActPoint(RandomEventAct.Event2AttendClass);
        if (currRandomEventAct == RandomEventAct.Event4AttendClass)
        {
            leavePoint = ScenePoint.Instance.GetRandomEventActPoint(RandomEventAct.Event4AttendClass);
        }

        go.transform.DOPath(ScenePath.Instance.GetBackPath(currAttendClassPath, leavePoint), attendClassTime,
                PathType.CatmullRom).SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
            currRandomEventAct = RandomEventAct.Idle;
            GetAllRandom();
            SMachine.TranslateState(RoleState.Run);
        };
    }


    // ReSharper disable Unity.PerformanceAnalysis
    private void GetAttendClassPath()
    {
        GetRolePath(5);
        if (curRandomPath == EventRandomPath.None) return;
        
        Vector3[] randomAry = null;
        if (currRandomEventAct == RandomEventAct.Event2AttendClass)
        {
            randomAry = ScenePath.Instance.GetEvent2AttendClassPath(curRandomPath);
        }
        else if (currRandomEventAct == RandomEventAct.Event4AttendClass)
        {
            randomAry = ScenePath.Instance.GetEvent4AttendClassPath(curRandomPath);
        }
        
        currAttendClassPath = randomAry;
    }

    private void GetAttendClassPathTime()
    {
        if (currRandomEventAct == RandomEventAct.Event2AttendClass)
        {
            switch (curRandomPath)
            {
                case EventRandomPath.Path1:
                    attendClassTime = 0.5f;
                    break;
                case EventRandomPath.Path2:
                    attendClassTime = 2.5f;
                    break;
                case EventRandomPath.Path3:
                    attendClassTime = 1.5f;
                    break;
                case EventRandomPath.Path4:
                    attendClassTime = 3.5f;
                    break;
            }
        }
        else if (currRandomEventAct == RandomEventAct.Event4AttendClass)
        {
            switch (curRandomPath)
            {
                case EventRandomPath.Path1:
                    attendClassTime = 3f;
                    break;
                case EventRandomPath.Path2:
                    attendClassTime = 2f;
                    break;
                case EventRandomPath.Path3:
                    attendClassTime = 1.5f;
                    break;
                case EventRandomPath.Path4:
                    attendClassTime = 1.2f;
                    break;
            }
        }
    }

    public void ExitAttendClassAct()
    {
    }

    #endregion

    #region 开关柜门

    private Vector3[] switchDoorPath;
    private int switchDoorRotate = 180;
    private bool isSwitchDoorLeave = false;
    private bool isSwitchDoorPath = false;
    private float switchDoorTime = 2.5f;
    public void EnterSwitchDoorAct()
    {
        isSwitchDoorLeave = false;
        isSwitchDoorPath = false;
        isPathLeaveState = false;
        EnterSwitchDoor();
    }

    public void UpdateSwitchDoorAct(float deltaTime)
    {
        if (isSwitchDoorPath)
        {
            if (!isPathLeaveState)
            {
                isPathLeaveState = true;
                leaveState();
            }
        }
        
        currRoleAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.OpenSwitchDoor.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.SwitchDoor);
        }

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.CloseSwitchDoor.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.SwitchDoor);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                if (!isSwitchDoorLeave)
                {
                    isSwitchDoorLeave = true;
                    leaveSwitchDoor();
                }
            }
        }

        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Walk_01.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Run);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void EnterSwitchDoor()
    {
        isSwitchDoorPath = RoleSwitchDoor();
        if (isSwitchDoorPath) return;
        SetAIComponent(false);
        switchDoorPath = ScenePath.Instance.GetEvent3SwitchDoorPath();
        switchDoorRotate = 0;
        if (currRandomEventAct == RandomEventAct.Event5SwitchDoor)
        {
            switchDoorRotate = 90;
            switchDoorPath = ScenePath.Instance.GetEvent5SwitchDoorPath();
        }

        go.transform.DOPath(switchDoorPath, switchDoorTime, PathType.CatmullRom).SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            go.transform.DORotate(new Vector3(0, switchDoorRotate, 0), 0.2f, RotateMode.Fast).onComplete = () =>
            {
                animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
                animator.SetBool(ToAnimatorCondition.ToSwitchDoor.ToString(), true);
            };
        };
    }

    private void leaveSwitchDoor()
    {
        animator.SetBool(ToAnimatorCondition.ToSwitchDoor.ToString(), false);
        animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), true);
        var leavePoint = ScenePoint.Instance.GetRandomEventActPoint(RandomEventAct.Event3SwitchDoor);

        if (currRandomEventAct == RandomEventAct.Event5SwitchDoor)
        {
            leavePoint = ScenePoint.Instance.GetRandomEventActPoint(RandomEventAct.Event5SwitchDoor);
        }

        go.transform.DOPath(ScenePath.Instance.GetBackPath(switchDoorPath, leavePoint), switchDoorTime, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetLookAt(0).onComplete = () =>
        {
            currRandomEventAct = RandomEventAct.Idle;
            GetAllRandom();
            animator.SetBool(ToAnimatorCondition.ToWalk_01.ToString(), false);
            SMachine.TranslateState(RoleState.Run);
        };
    }

    public void ExitSwitchDoorAct()
    {
    }

    #endregion

    #region 交谈状态

    public void EnterTalk()
    {
        animator.SetBool(ToAnimatorCondition.ToInteraction.ToString(), true);
    }

    public void UpdateTalk(float deltaTime)
    {
        currRoleAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.Interaction.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Talk);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                SMachine.TranslateState(RoleState.Urination);
            }
        }
    }

    public void ExitTalk()
    {
        animator.SetBool(ToAnimatorCondition.ToInteraction.ToString(), false);
    }

    #endregion
    
    #region 维修

    public void ExitRepairAct()
    {
    }

    public void EnterRepairAct()
    {
        PlayAnim(RoleState.Repair);
    }

    public void UpdateRepairAct(float deltaTime)
    {
    }

    #endregion

    #region 上班

    public void ExitGotoWorkAct()
    {
    }

    public void EnterGotoWorkAct()
    {
        PlayAnim(RoleState.GotoWork);
    }

    public void UpdateGotoWorkAct(float deltaTime)
    {
    }

    #endregion
    
    #region 尿急

    public void EnterUrinationAct()
    {
        animator.SetBool(ToAnimatorCondition.ToNiaoji.ToString(), true);
    }

    public void UpdateUrinationAct(float deltaTime)
    {
        currRoleAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (currRoleAnimatorStateInfo.IsName(RoleAnimatorName.niaoji.ToString()))
        {
            animator.SetInteger(ToAnimatorCondition.CurrState.ToString(), (int)RoleAniState.Urination);
            if (currRoleAnimatorStateInfo.normalizedTime > 1)
            {
                SMachine.TranslateState(RoleState.Talk);
            }
        }
    }

    public void ExitUrinationAct()
    {
        animator.SetBool(ToAnimatorCondition.ToNiaoji.ToString(), false);
    }

    #endregion
    
    #region 头部动作

    public void ExitHeadAct()
    {
    }

    public void EnterHeadAct()
    {
    }

    public void UpdateHeadAct(float deltaTime)
    {
    }

    #endregion

    #region 丢垃圾

    public void ExitLoseGarbageAct()
    {
    }

    public void EnterLoseGarbageAct()
    {
        PlayAnim(RoleState.LoseGarbage);
    }

    public void UpdateLoseGarbageAct(float deltaTime)
    {
    }

    #endregion
    
    #region 打扫垃圾

    public void ExitCleanGarbageAct()
    {
    }

    public void EnterCleanGarbageAct()
    {
        PlayAnim(RoleState.CleanGarbage);
    }

    public void UpdateCleanGarbageAct(float deltaTime)
    {
    }

    #endregion
    
}