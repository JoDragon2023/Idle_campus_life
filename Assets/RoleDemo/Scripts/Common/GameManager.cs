using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Action<float> UpdateAct;
    /// <summary>
    /// 到达目标点事件
    /// </summary>
    public Action ArriveAct;
    public float currentStateTime;
    [HideInInspector]
    public GameObject roleRoot;
    [HideInInspector]
    public GameObject targetRoot;
    [HideInInspector]
    public DoorEvent[] doorEventAry;
   
    private BedAnim[] bedAniAry;
    public Action<DoorAnim, Collider> DoorRaycastEvent;

    private void Awake()
    {
        Instance = this;
        roleRoot = GameObject.Find("__roleRoot__");
        if (roleRoot == null)
        {
            roleRoot = new GameObject("__roleRoot__");
        }
        
        targetRoot = GameObject.Find("__targetRoot__");
        if (targetRoot == null)
        {
            targetRoot = new GameObject("__targetRoot__");
        }

        doorEventAry =  Resources.FindObjectsOfTypeAll<DoorEvent>();
        bedAniAry =  Resources.FindObjectsOfTypeAll<BedAnim>();
    }

    void Start()
    {
        SceneObjMgr.Instance.Init();
        InitDoorRayEvent();
    }

    void Update()
    {
        OnUpdate(Time.deltaTime);
    }

    private void InitDoorRayEvent()
    {
        if (doorEventAry == null|| doorEventAry.Length <= 0 ) return;

        foreach (var t in doorEventAry)
        {
            t.rayEvent += TriggerDoorRayEvent;
        }
    }

    private void TriggerDoorRayEvent(DoorAnim doorAnim, Collider collider)
    {
        DoorRaycastEvent?.Invoke(doorAnim,collider);
    }
    
    private void OnUpdate(float deltaTime)
    {
        currentStateTime += deltaTime;
        UpdateAct?.Invoke(deltaTime);
    }

    /// <summary>
    /// 获取床播放动画对象
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public BedAnim GetBedAnim(EventRandomPath path)
    {
        for (var i = 0; i < bedAniAry.Length; i++)
        {
            if (bedAniAry[i].eventRandomPath == path)
            {
                return bedAniAry[i];
            }
        }

        return null;
    }
    
}
