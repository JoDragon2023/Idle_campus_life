
public enum GuideStep
{
    None,
    /// <summary>
    /// 移动摄像头
    /// </summary>
    Panning,    
    /// <summary>
    /// 旋转
    /// </summary>
    Rotating,
    /// <summary>
    /// 缩放
    /// </summary>
    Zooming,
    /// <summary>
    /// 俯视
    /// </summary>
    Lift,
    /// <summary>
    /// 完成新手引导
    /// </summary>
    Complete,
}


public enum PlayAnimType
{
    None,
    One,
    Two,
    Three
}

public enum DoorRayType
{
    None,
    Forward,
    Back,
    Down,
    Up,
    Left,
    Right
}

public enum DoorAnimType
{
    None,
    /// <summary>
    /// 通用
    /// </summary>
    Common,
    /// <summary>
    /// 教室
    /// </summary>
    Classroom,
}

//角色类型
public enum RoleType
{
    None,
    /// <summary>
    /// 学生
    /// </summary>
    Student,
    /// <summary>
    /// 老师
    /// </summary>
    Teacher,
    /// <summary>
    /// 勤杂工
    /// </summary>
    Handyman,
    /// <summary>
    /// 维修工
    /// </summary>
    Fettler
}

public enum CreateRoleType
{
    None,
    Role1,
    Role2,
    Role3,
    Role4,
    Role5,
    Role6,
    Role7,
    Role8,
    Role9,
    Role10,
    Role11,
    Role12,
    Role13,
    Role14,
    Role15,
    Role16,
    Role17,
    Role18
}


/// <summary>
/// 随机区域类型
/// </summary>
public enum RandomArea
{
    None,
    /// <summary>
    /// 闲置点
    /// </summary>
    Idle,
    /// <summary>
    /// 事件区域 1
    /// </summary>
    EventOne,
    /// <summary>
    /// 事件区域 2
    /// </summary>
    EventTwo
}


/// <summary>
/// 区分当前角色进入了那个区域
/// </summary>
public enum SceneArea
{
    None,
    /// <summary>
    /// 上厕所区域 1
    /// </summary>
    ToiletOne,
    /// <summary>
    /// 上厕所区域 2
    /// </summary>
    ToiletTwo,
    /// <summary>
    /// 休闲区域 1
    /// </summary>
    LeisureOne,
    /// <summary>
    /// 休闲区域 2
    /// </summary>
    LeisureTwo
}

/// <summary>
/// 做事件的随机用
/// </summary>
public enum RandomEvent
{
    None,
    Event1,
    Event2,
    Event3,
    Event4,
    Event5,
    Event6,
    Event7,
    Event8,
    Event9,
    Event10,
    Event11,
}

/// <summary>
/// 事件对应的行为  不同的事件，有不同的行为。具体的不同的行为，做状态切换，然后执行行为。
/// </summary>
public enum RandomEventAct
{
    None,
    /// <summary>
    /// 出生后要走的路线
    /// </summary>
    Main,
    /// <summary>
    /// 去闲置点
    /// </summary>
    Idle,
    /// <summary>
    /// 事件1 坐下 随机一个座椅
    /// </summary>
    Event1Sit,
    /// <summary>
    /// 事件1 交谈
    /// </summary>
    Event1Talk,
    /// <summary>
    /// 事件1 吃喝
    /// </summary>
    Event1EatDrink,
    /// <summary>
    /// 事件2 上课  随机一个座椅
    /// </summary>
    Event2AttendClass,
    /// <summary>
    /// 事件2 站立（随机一个书架）
    /// </summary>
    Event2Stand,
    /// <summary>
    /// 事件3 睡觉  随机一个床位
    /// </summary>
    Event3Sleep,
    /// <summary>
    /// 事件3 开关柜门
    /// </summary>
    Event3SwitchDoor,
    /// <summary>
    /// 事件4  上课 学生随机一个座位，老师在讲台的位置
    /// </summary>
    Event4AttendClass,
    /// <summary>
    /// 事件5 睡觉  随机一个床位
    /// </summary>
    Event5Sleep,
    /// <summary>
    /// 事件5 开关柜门
    /// </summary>
    Event5SwitchDoor,
    /// <summary>
    /// 事件6 上厕所  随机一个厕所格
    /// </summary>
    Event6Toilet,
    /// <summary>
    /// 事件7 洗澡  随机一个淋浴格
    /// </summary>
    Event7Bathe,
    /// <summary>
    /// 事件8 上厕所  随机一个厕所格
    /// </summary>
    Event8Toilet,
    /// <summary>
    /// 事件9 洗澡  随机一个淋浴格
    /// </summary>
    Event9Bathe,
    /// <summary>
    /// 事件10 上厕所  随机一个厕所格
    /// </summary>
    Event10Toilet,
    /// <summary>
    /// 事件11 洗澡  随机一个淋浴格
    /// </summary>
    Event11Bathe,
    /// <summary>
    /// 科学实验室
    /// </summary>
    Event12Laboratory,
}

/// <summary>
/// 走路状态，区分不同类型，去不同的点的路线
/// </summary>
public enum RunSceneArea
{
    None,
    /// <summary>
    /// 出生后要走的路线
    /// </summary>
    Main,
    /// <summary>
    /// 去闲置点
    /// </summary>
    Idle,
    /// <summary>
    /// 上厕所区域 1
    /// </summary>
    ToiletOne,
    /// <summary>
    /// 上厕所区域 2
    /// </summary>
    ToiletTwo,
    /// <summary>
    /// 休闲区域 1
    /// </summary>
    IdleOne,
    /// <summary>
    /// 休闲区域 2
    /// </summary>
    IdleTwo,
    /// <summary>
    /// 洗澡区域 1
    /// </summary>
    BatheOne,
    /// <summary>
    /// 洗澡区域 2
    /// </summary>
    BatheTwo,
    /// <summary>
    /// 睡觉区域 1
    /// </summary>
    SleepOne,
    /// <summary>
    /// 睡觉区域 2
    /// </summary>
    SleepTwo,
    /// <summary>
    /// 上厕所 返回休闲区域 1
    /// </summary>
    ToiletBackIdleOne,
    /// <summary>
    /// 上厕所 返回休闲区域 2
    /// </summary>
    ToiletBackIdleTwo,
    /// <summary>
    /// 洗澡 返回休闲区域 1
    /// </summary>
    BatheBackIdleOne,
    /// <summary>
    /// 洗澡 返回休闲区域 2
    /// </summary>
    BatheBackIdleTwo
}

/// <summary>
/// 动画条件参数
/// </summary>
public enum ToAnimatorCondition
{
    ToStand,
    ToRun,
    ToUrineWalk, //尿急
    ToWalk_01,
    ToWalk_02,
    ToWalk_03,
    ToInteraction,
    ToNiaoji,
    ToOpenDoor_Toilet,
    ToToilet,
    ToToilet_GetUp,
    ToSitdown,
    ToStudyIdle,
    ToStudyCourseIdle,
    ToStudy,
    ToDrink,
    ToSwitchDoor,
    ToSleepOne,
    ToSleepTwo,
    ToSleepThree,
    ToBatheOne,
    ToBatheTwo,
    ToBatheThree,
    ToBatheFour,
    ToTeacherLecture,
    ToShakeHead,
    ToSleepIdle,
    CurrState
}


public enum BedAnimState
{
    BedOne = 1,
    BedTwo = 2,
    BedThree = 3,
}

/// <summary>
/// 床的动画名字
/// </summary>
public enum BedAnimatorName
{
    BedOne,
    BedTwo,
    BedThree,
}

public enum BedAnimatorCondition
{
    CurrState,
    ToBedOne,
    ToBedTwo,
    ToBedThree,
}


/// <summary>
/// 角色动画状态名称
/// </summary>
public enum RoleAnimatorName
{
    Run,
    Interaction,
    niaoji,
    UrineWalk,
    Walk_01,
    Walk_02,
    Walk_03,
    ToiletOne,
    ToiletTwo,
    ToiletThree,
    Sitdown,
    StudyIdle,
    StudyCourseIdle,
    Study,
    Drink,
    Stand,
    OpenSwitchDoor,
    SwitchDoor,
    CloseSwitchDoor,
    SleepOne,
    SleepTwo,
    SleepThree,
    BatheOne,
    BatheTwo,
    BatheThree,
    BatheFour,
    TeacherLecture,
    ShakeHead,
    SleepIdle,
}

public enum RoleAniState
{
    /// <summary>
    /// 未设置
    /// </summary>
    None = 0,
    /// <summary>
    /// 跑
    /// </summary>
    Run = 1,
    /// <summary>
    /// 交谈
    /// </summary>
    Talk = 2,
    /// <summary>
    /// 尿急
    /// </summary>
    Urination = 3,
    /// <summary>
    /// 上厕所
    /// </summary>
    Toilet = 4,
    /// <summary>
    /// 上完厕所开门
    /// </summary>
    ToiletGetUP = 5,
    /// <summary>
    /// 通用 坐下动画
    /// </summary>
    Sitdown = 6,
    /// <summary>
    /// 坐下动画 Idle
    /// </summary>
    StudyIdle = 7,
    /// <summary>
    /// 上课 学习动画
    /// </summary>
    Study = 8,
    /// <summary>
    /// 吃喝动画
    /// </summary>
    Drink = 9,
    /// <summary>
    /// 站立动画
    /// </summary>
    Stand = 10,
    /// <summary>
    /// 开关柜门动画
    /// </summary>
    SwitchDoor = 11,
    /// <summary>
    /// 睡觉动画
    /// </summary>
    SleepOne = 12,
    SleepTwo = 13,
    SleepThree = 14,
    /// <summary>
    /// 洗澡动画
    /// </summary>
    BatheOne = 15,
    BatheTwo = 16,
    BatheThree = 17,
    BatheFour = 18,
    UrineWalk = 19,
    /// <summary>
    /// 老师讲课 动画
    /// </summary>
    TeacherLecture = 20,
    /// <summary>
    /// 坐着左右看
    /// </summary>
    ShakeHead = 21,
    /// <summary>
    /// 床上待机动画
    /// </summary>
    SleepIdle = 22,
}

public enum EventRandomPath
{
    None,
    Path1,
    Path2,
    Path3,
    Path4,
    Path5,
    Path6,
    Path7
}

/// <summary>
/// 洗澡播放动画状态用
/// </summary>
public enum BatheStateAnim
{
    None,
    Open,
    Close
}

/// <summary>
/// 场景里面的对象类型
/// </summary>
public enum SceneOBj
{
    None,  //无类型
    Role,  //角色类型
    Point  //行走的点
}

/// <summary>
/// 角色状体
/// </summary>
public enum RoleState
{
    /// <summary>
    /// 休闲状态
    /// </summary>
    Idle,          
    /// <summary>
    /// 行走
    /// </summary>
    Run,           
    /// <summary>
    /// 交谈
    /// </summary>
    Talk,    
    /// <summary>
    /// 坐下
    /// </summary>
    Sit,            
    /// <summary>
    /// 头部动作
    /// </summary>
    HeadAct,  
    /// <summary>
    /// 丢垃圾
    /// </summary>
    LoseGarbage,  
    /// <summary>
    /// 打扫垃圾
    /// </summary>
    CleanGarbage,    
    /// <summary>
    /// 吃喝
    /// </summary>
    EatDrink,     
    /// <summary>
    /// 上厕所
    /// </summary>
    Toilet,  
    /// <summary>
    /// 尿急
    /// </summary>
    Urination,  
    /// <summary>
    /// 洗澡
    /// </summary>
    Bathe,           
    /// <summary>
    /// 睡觉
    /// </summary>
    Sleep,           
    /// <summary>
    /// 上课
    /// </summary>
    AttendClass,
    /// <summary>
    /// 去上课
    /// </summary>
    GoToClass,
    /// <summary>
    /// 维修
    /// </summary>
    Repair,         
    /// <summary>
    /// 上班
    /// </summary>
    GotoWork,        
    /// <summary>
    /// 开关柜门
    /// </summary>
    SwitchDoor,
    /// <summary>
    /// 站立
    /// </summary>
    Stand
}

