using System;
using System.Collections.Generic;
using UnityEngine;

public class ScenePath : MonoBehaviour
{
    public static ScenePath Instance;
    public int len = 0;

    //Event8Toilet  上厕所 路线
    public Transform[] Event8ToiletPath;
    
    private Vector3[] Event8ToiletArry;
    
    public Transform[] Event6ToiletPath;
    
    private Vector3[] Event6ToiletArry;

    public Transform[] Event10ToiletPath;
    
    private Vector3[] Event10ToiletArry;
    
    //Event1Sit  坐下 随机一个座椅
    public Transform[] Event1SitOnePath;
    
    private Vector3[] Event1SitOneArry;
    
    public Transform[] Event1SitTwoPath;
    
    private Vector3[] Event1SitTwoArry;
    
    public Transform[] Event1SitThreePath;
    
    private Vector3[] Event1SitThreeArry;

    //Event2AttendClass  上课 随机一个座椅
    public Transform[] Event2AttendClassOne;
    
    private Vector3[] Event2AttendClassOneArry;
    
    public Transform[] Event2AttendClassTwo;
    
    private Vector3[] Event2AttendClassTwoArry;
    
    public Transform[] Event2AttendClassThree;
    
    private Vector3[] Event2AttendClassThreeArry;
    
    public Transform[] Event2AttendClassFour;
    
    private Vector3[] Event2AttendClassFourArry;
    
    //Event4AttendClass  上课 随机一个座椅
    public Transform[] Event4AttendClassOne;
    
    private Vector3[] Event4AttendClassOneArry;
    
    public Transform[] Event4AttendClassTwo;
    
    private Vector3[] Event4AttendClassTwoArry;
    
    public Transform[] Event4AttendClassThree;
    
    private Vector3[] Event4AttendClassThreeArry;
    
    public Transform[] Event4AttendClassFour;
    
    private Vector3[] Event4AttendClassFourArry;
    
    //老师上课路线
    public Transform[] Event4TeacherClass;
    
    private Vector3[] Event4TeacherClassArry;
    
    // Event7Bathe 洗澡  随机一个淋浴格
    public Transform[] Event7BathePath;
    
    private Vector3[] Event7BatheArry;
    
    public Transform[] Event9BathePath;
    
    private Vector3[] Event9BatheArry;

    public Transform[] Event11BathePath;
    
    private Vector3[] Event11BatheArry;
    
    //Event1EatDrink  吃喝
    public Transform[] Event1EatDrinkOnePath;
    
    private Vector3[] Event1EatDrinkOneArry;
    
    public Transform[] Event1EatDrinkTwoPath;
    
    private Vector3[] Event1EatDrinkTwoArry;
    
    //Event2Stand  站立
    public Transform[] Event2StandOnePath;
    
    private Vector3[] Event2StandOneArry;
    
    public Transform[] Event2StandTwoPath;
    
    private Vector3[] Event2StandTwoArry;
    
    //事件 3 5 开关柜门
    public Transform[] Event3SwitchDoorPath;
    
    private Vector3[] Event3SwitchDoorArry;
    
    public Transform[] Event5SwitchDoorPath;
    
    private Vector3[] Event5SwitchDoorArry;
    
    //事件3 5 睡觉  随机一个床位
    public Transform[] Event3SleepOne;
    
    private Vector3[] Event3SleepOneArry;
    
    public Transform[] Event3SleepTwo;
    
    private Vector3[] Event3SleepTwoArry;
    
    public Transform[] Event3SleepThree;
    
    private Vector3[] Event3SleepThreeArry;
    
    public Transform[] Event3SleepFour;
    
    private Vector3[] Event3SleepFourArry;
    
    public Transform[] Event5SleepOne;
    
    private Vector3[] Event5SleepOneArry;
    
    public Transform[] Event5SleepTwo;
    
    private Vector3[] Event5SleepTwoArry;
    
    public Transform[] Event5SleepThree;
    
    private Vector3[] Event5SleepThreeArry;
    
    public Transform[] Event5SleepFour;
    
    private Vector3[] Event5SleepFourArry;
    
    public Transform[] Event12Laboratory;
    
    private Vector3[] Event12SLaboratoryArry;
    
    public Transform[] CarPath;
    
    private Vector3[] CarPathArry;
    
    
    private void Awake()
    {
        Instance = this;
        InitPath();
    }

    private void InitPath()
    {
        len = Event8ToiletPath.Length;
        Event8ToiletArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event8ToiletArry[i] = Event8ToiletPath[i].position;
        }

        len = Event6ToiletPath.Length;
        Event6ToiletArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event6ToiletArry[i] = Event6ToiletPath[i].position;
        }

        len = Event10ToiletPath.Length;
        Event10ToiletArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event10ToiletArry[i] = Event10ToiletPath[i].position;
        }
        
        len = Event1SitOnePath.Length;
        Event1SitOneArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event1SitOneArry[i] = Event1SitOnePath[i].position;
        }
        
        len = Event1SitTwoPath.Length;
        Event1SitTwoArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event1SitTwoArry[i] = Event1SitTwoPath[i].position;
        }
        
        len = Event1SitThreePath.Length;
        Event1SitThreeArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event1SitThreeArry[i] = Event1SitThreePath[i].position;
        }
        
        len = Event2AttendClassOne.Length;
        Event2AttendClassOneArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event2AttendClassOneArry[i] = Event2AttendClassOne[i].position;
        }
        
        len = Event2AttendClassTwo.Length;
        Event2AttendClassTwoArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event2AttendClassTwoArry[i] = Event2AttendClassTwo[i].position;
        }
        
        len = Event2AttendClassThree.Length;
        Event2AttendClassThreeArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event2AttendClassThreeArry[i] = Event2AttendClassThree[i].position;
        }
        
        len = Event2AttendClassFour.Length;
        Event2AttendClassFourArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event2AttendClassFourArry[i] = Event2AttendClassFour[i].position;
        }
        
        len = Event4AttendClassOne.Length;
        Event4AttendClassOneArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event4AttendClassOneArry[i] = Event4AttendClassOne[i].position;
        }
        
        len = Event4AttendClassTwo.Length;
        Event4AttendClassTwoArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event4AttendClassTwoArry[i] = Event4AttendClassTwo[i].position;
        }
        
        len = Event4AttendClassThree.Length;
        Event4AttendClassThreeArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event4AttendClassThreeArry[i] = Event4AttendClassThree[i].position;
        }
        
        len = Event4AttendClassFour.Length;
        Event4AttendClassFourArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event4AttendClassFourArry[i] = Event4AttendClassFour[i].position;
        }
        
        len = Event4TeacherClass.Length;
        Event4TeacherClassArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event4TeacherClassArry[i] = Event4TeacherClass[i].position;
        }
        
        len = Event7BathePath.Length;
        Event7BatheArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event7BatheArry[i] = Event7BathePath[i].position;
        }
        
        len = Event9BathePath.Length;
        Event9BatheArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event9BatheArry[i] = Event9BathePath[i].position;
        }

        len = Event11BathePath.Length;
        Event11BatheArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event11BatheArry[i] = Event11BathePath[i].position;
        }
        
        len = Event1EatDrinkOnePath.Length;
        Event1EatDrinkOneArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event1EatDrinkOneArry[i] = Event1EatDrinkOnePath[i].position;
        }
        
        len = Event1EatDrinkTwoPath.Length;
        Event1EatDrinkTwoArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event1EatDrinkTwoArry[i] = Event1EatDrinkTwoPath[i].position;
        }
        
        len = Event2StandOnePath.Length;
        Event2StandOneArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event2StandOneArry[i] = Event2StandOnePath[i].position;
        }
        
        len = Event2StandTwoPath.Length;
        Event2StandTwoArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event2StandTwoArry[i] = Event2StandTwoPath[i].position;
        }
        
        len = Event3SwitchDoorPath.Length;
        Event3SwitchDoorArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event3SwitchDoorArry[i] = Event3SwitchDoorPath[i].position;
        }
        
        len = Event5SwitchDoorPath.Length;
        Event5SwitchDoorArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event5SwitchDoorArry[i] = Event5SwitchDoorPath[i].position;
        }
        
        len = Event3SleepOne.Length;
        Event3SleepOneArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event3SleepOneArry[i] = Event3SleepOne[i].position;
        }
        
        len = Event3SleepTwo.Length;
        Event3SleepTwoArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event3SleepTwoArry[i] = Event3SleepTwo[i].position;
        }
        
        len = Event3SleepThree.Length;
        Event3SleepThreeArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event3SleepThreeArry[i] = Event3SleepThree[i].position;
        }
        
        len = Event3SleepFour.Length;
        Event3SleepFourArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event3SleepFourArry[i] = Event3SleepFour[i].position;
        }
        
        len = Event5SleepOne.Length;
        Event5SleepOneArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event5SleepOneArry[i] = Event5SleepOne[i].position;
        }
        
        len = Event5SleepTwo.Length;
        Event5SleepTwoArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event5SleepTwoArry[i] = Event5SleepTwo[i].position;
        }
        
        len = Event5SleepThree.Length;
        Event5SleepThreeArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event5SleepThreeArry[i] = Event5SleepThree[i].position;
        }
        
        len = Event5SleepFour.Length;
        Event5SleepFourArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event5SleepFourArry[i] = Event5SleepFour[i].position;
        }
        
        len = Event12Laboratory.Length;
        Event12SLaboratoryArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            Event12SLaboratoryArry[i] = Event12Laboratory[i].position;
        }
        
        len = CarPath.Length;
        CarPathArry = new Vector3[len];
        for (var i = 0; i < len; i++)
        {
            CarPathArry[i] = CarPath[i].position;
        }
    }

    public Vector3[] GetCarPath(EventRandomPath sitRandom)
    {
        Vector3[] pos = null;
        List<Vector3> pathList = new List<Vector3>();
        switch (sitRandom)
        {
            case EventRandomPath.Path1:
                pathList.Add(CarPathArry[0]);
                pathList.Add(CarPathArry[1]);
                pathList.Add(CarPathArry[2]);
                break;
            case EventRandomPath.Path2:
                pathList.Add(CarPathArry[3]);
                pathList.Add(CarPathArry[4]);
                pathList.Add(CarPathArry[5]);
                break;
        }

        pos = pathList.ToArray();
        
        return pos;
    }
    
  
    /// <summary>
    /// 事件12  科学实验路线
    /// </summary>
    /// <returns></returns>
    public Vector3[] GeEvent12LaboratoryPathVoide(EventRandomPath sitRandom)
    {
        List<Vector3> pathList = new List<Vector3>();
        switch (sitRandom)
        {
            case EventRandomPath.Path1:
                pathList.Add(Event12SLaboratoryArry[0]);
                pathList.Add(Event12SLaboratoryArry[3]);
                pathList.Add(Event12SLaboratoryArry[5]);
                break;
            case EventRandomPath.Path2:
                pathList.Add(Event12SLaboratoryArry[0]);
                pathList.Add(Event12SLaboratoryArry[6]);
                pathList.Add(Event12SLaboratoryArry[7]);
                break;
        }
        
        return pathList.ToArray();
    }
    
    
    /// <summary>
    /// 事件12  科学实验路线
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, Vector3[]> GeEvent12LaboratoryPath()
    {
        Dictionary<int, Vector3[]> randomInfoDic = new Dictionary<int, Vector3[]>();
        var index = UnityEngine.Random.Range(1 , 7);
        var sitRandom = (EventRandomPath)index;
        Vector3[] pos = null;
        List<int> pathList = new List<int>();
        switch (sitRandom)
        {
            case EventRandomPath.Path1:
                pathList.Add(0);
                pathList.Add(1);
                break;
            case EventRandomPath.Path2:
                pathList.Add(0);
                pathList.Add(2);
                break;
            case EventRandomPath.Path3:
                pathList.Add(0);
                pathList.Add(3);
                break;
            case EventRandomPath.Path4:
                pathList.Add(0);
                pathList.Add(3);
                pathList.Add(4);
                break;
            case EventRandomPath.Path5:
                pathList.Add(0);
                pathList.Add(3);
                pathList.Add(5);
                break;
            case EventRandomPath.Path6:
                pathList.Add(0);
                pathList.Add(6);
                pathList.Add(7);
                break;
            case EventRandomPath.Path7:
                pathList.Add(0);
                pathList.Add(6);
                pathList.Add(7);
                pathList.Add(8);
                pathList.Add(9);
                break;
        }

        
        if (randomInfoDic.ContainsKey(index))
        {
            randomInfoDic[index] = pos;
        }
        else
        {
            randomInfoDic.Add(index, pos);
        }
        
        return randomInfoDic;
    }
    
    /// <summary>
    /// 事件3 睡觉路线
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, Vector3[]> GeEvent3SleepPath()
    {
        Dictionary<int, Vector3[]> randomInfoDic = new Dictionary<int, Vector3[]>();
        var index = UnityEngine.Random.Range(1 , 5);
        var sitRandom = (EventRandomPath)index;
        Vector3[] pos = null;
        
        switch (sitRandom)
        {
            case EventRandomPath.Path1:
                pos = Event3SleepOneArry;
                break;
            case EventRandomPath.Path2:
                pos = Event3SleepTwoArry;
                break;
            case EventRandomPath.Path3:
                pos = Event3SleepThreeArry;
                break;
            case EventRandomPath.Path4:
                pos = Event3SleepFourArry;
                break;
        }

        if (randomInfoDic.ContainsKey(index))
        {
            randomInfoDic[index] = pos;
        }
        else
        {
            randomInfoDic.Add(index, pos);
        }
        
        return randomInfoDic;
    }
    
    public Vector3[] GeEvent3SleepPath(EventRandomPath path)
    {
        Vector3[] pos = null;
        switch (path)
        {
            case EventRandomPath.Path1:
                pos = Event3SleepOneArry;
                break;
            case EventRandomPath.Path2:
                pos = Event3SleepTwoArry;
                break;
            case EventRandomPath.Path3:
                pos = Event3SleepThreeArry;
                break;
            case EventRandomPath.Path4:
                pos = Event3SleepFourArry;
                break;
        }
        return pos;
    }
    
    public Vector3[] GeEvent5SleepPath(EventRandomPath path)
    {
        Vector3[] pos = null;
        switch (path)
        {
            case EventRandomPath.Path1:
                pos = Event5SleepOneArry;
                break;
            case EventRandomPath.Path2:
                pos = Event5SleepTwoArry;
                break;
            case EventRandomPath.Path3:
                pos = Event5SleepThreeArry;
                break;
            case EventRandomPath.Path4:
                pos = Event5SleepFourArry;
                break;
        }
        return pos;
    }
    
    
    /// <summary>
    /// 事件5 睡觉路线
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, Vector3[]> GeEvent5SleepPath()
    {
        Dictionary<int, Vector3[]> randomInfoDic = new Dictionary<int, Vector3[]>();
        //var index = UnityEngine.Random.Range(1 , 5);
        var index = 3;
        var sitRandom = (EventRandomPath)index;
        Vector3[] pos = null;
        
        switch (sitRandom)
        {
            case EventRandomPath.Path1:
                pos = Event5SleepOneArry;
                break;
            case EventRandomPath.Path2:
                pos = Event5SleepTwoArry;
                break;
            case EventRandomPath.Path3:
                pos = Event5SleepThreeArry;
                break;
            case EventRandomPath.Path4:
                pos = Event5SleepFourArry;
                break;
        }

        if (randomInfoDic.ContainsKey(index))
        {
            randomInfoDic[index] = pos;
        }
        else
        {
            randomInfoDic.Add(index, pos);
        }
        
        return randomInfoDic;
    }
    
    
    /// <summary>
    /// //事件 3 开关柜门
    /// </summary>
    /// <returns></returns>
    public Vector3[] GetEvent3SwitchDoorPath()
    {
        return Event3SwitchDoorArry;
    }

    /// <summary>
    /// //事件 5 开关柜门
    /// </summary>
    /// <returns></returns>
    public Vector3[] GetEvent5SwitchDoorPath()
    {
        return Event5SwitchDoorArry;
    }
    
    public Vector3[] GetEvent2StandPath(EventRandomPath sitRandom)
    {
        Vector3[] pos = null;
        
        switch (sitRandom)
        {
            case EventRandomPath.Path1:
                pos = Event2StandOneArry;
                break;
            case EventRandomPath.Path2:
                pos = Event2StandTwoArry;
                break;
        }

        return pos;
    }
    
    /// <summary>
    /// 事件2 站立路线
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, Vector3[]> GetEvent2StandPath()
    {
        Dictionary<int, Vector3[]> randomInfoDic = new Dictionary<int, Vector3[]>();
        var index = UnityEngine.Random.Range(1 , 3);
        var sitRandom = (EventRandomPath)index;
        Vector3[] pos = null;
        
        switch (sitRandom)
        {
            case EventRandomPath.Path1:
                pos = Event2StandOneArry;
                break;
            case EventRandomPath.Path2:
                pos = Event2StandTwoArry;
                break;
        }

        if (pos == null)
        {
            pos = Event2StandOneArry;
        }
        
        if (randomInfoDic.ContainsKey(index))
        {
            randomInfoDic[index] = pos;
        }
        else
        {
            randomInfoDic.Add(index, pos);
        }
        
        return randomInfoDic;
    }
    
    public Vector3[] GetEvent1EatDrinkPath(EventRandomPath sitRandom)
    {
        Vector3[] pos = null;
        switch (sitRandom)
        {
            case EventRandomPath.Path1:
                pos = Event1EatDrinkOneArry;
                break;
            case EventRandomPath.Path2:
                pos = Event1EatDrinkTwoArry;
                break;
        }
        return pos;
    }
    
    public Dictionary<int, Vector3[]> GetEvent1EatDrinkPath()
    {
        Dictionary<int, Vector3[]> randomInfoDic = new Dictionary<int, Vector3[]>();
        var index = UnityEngine.Random.Range(1 , 3);
        var sitRandom = (EventRandomPath)index;
        Vector3[] pos = null;
        
        switch (sitRandom)
        {
            case EventRandomPath.Path1:
                pos = Event1EatDrinkOneArry;
                break;
            case EventRandomPath.Path2:
                pos = Event1EatDrinkTwoArry;
                break;
        }

        if (pos == null)
        {
            pos = Event1EatDrinkOneArry;
        }
        
        if (randomInfoDic.ContainsKey(index))
        {
            randomInfoDic[index] = pos;
        }
        else
        {
            randomInfoDic.Add(index, pos);
        }
        
        return randomInfoDic;
    }
    
    public Vector3[] GetEvent1SitPath(EventRandomPath sitRandom)
    {
        Vector3[] pos = null;
        switch (sitRandom)
        {
            case EventRandomPath.Path1:
                pos = Event1SitOneArry;
                break;
            case EventRandomPath.Path2:
                pos = Event1SitTwoArry;
                break;
            case EventRandomPath.Path3:
                pos = Event1SitThreeArry;
                break;
        }

        return pos;
    }
    
    public Dictionary<int, Vector3[]> GetEvent1SitPath()
    {
        Dictionary<int, Vector3[]> randomInfoDic = new Dictionary<int, Vector3[]>();
        var index = UnityEngine.Random.Range(1 , 4);
        var sitRandom = (EventRandomPath)index;
        Vector3[] pos = null;
        
        switch (sitRandom)
        {
            case EventRandomPath.Path1:
                pos = Event1SitOneArry;
                break;
            case EventRandomPath.Path2:
                pos = Event1SitTwoArry;
                break;
            case EventRandomPath.Path3:
                pos = Event1SitThreeArry;
                break;
        }

        if (pos == null)
        {
            pos = Event1SitOneArry;
        }
        
        if (randomInfoDic.ContainsKey(index))
        {
            randomInfoDic[index] = pos;
        }
        else
        {
            randomInfoDic.Add(index, pos);
        }
        
        return randomInfoDic;
    }

    
    public Vector3[] GetEvent4AttendClassPath(EventRandomPath randomPath)
    {
        Vector3[] pos = null;
        switch (randomPath)
        {
            case EventRandomPath.Path1:
                pos = Event4AttendClassOneArry;
                break;
            case EventRandomPath.Path2:
                pos = Event4AttendClassTwoArry;
                break;
            case EventRandomPath.Path3:
                pos = Event4AttendClassThreeArry;
                break;
            case EventRandomPath.Path4:
                pos = Event4AttendClassFourArry;
                break;
        }
        
        return pos;
    }
    
    public Vector3[] GetEventGotoClassPath(EventRandomPath randomPath)
    {
        Vector3[] pos = null;
        switch (randomPath)
        {
            case EventRandomPath.Path1:
                pos = Event4TeacherClassArry;
                break;
        }
        
        return pos;
    }
    
    public Vector3[] GetEvent2AttendClassPath(EventRandomPath randomPath)
    {
        Vector3[] pos = null;
        
        switch (randomPath)
        {
            case EventRandomPath.Path1:
                pos = Event2AttendClassOneArry;
                break;
            case EventRandomPath.Path2:
                pos = Event2AttendClassTwoArry;
                break;
            case EventRandomPath.Path3:
                pos = Event2AttendClassThreeArry;
                break;
            case EventRandomPath.Path4:
                pos = Event2AttendClassFourArry;
                break;
        }
        
        return pos;
    }
    
    
    public Vector3[] GetEvent9BathePath(EventRandomPath randomPath)
    {
        return GetEventToiletPath(Event9BatheArry, randomPath);
    }
    
    public Vector3[] GetEvent7BathePath(EventRandomPath randomPath)
    {
        return GetEventToiletPath(Event7BatheArry, randomPath);
    }

    public Vector3[] GetEvent11BathePath(EventRandomPath randomPath)
    {
        return GetEventToiletPath(Event11BatheArry, randomPath);
    }
    
    public Vector3[] GetEvent10ToiletPath(EventRandomPath randomPath)
    {
        return GetEventToiletPath(Event10ToiletArry, randomPath);
    }

    public Vector3[] GetEvent8ToiletPath(EventRandomPath randomPath)
    {
        return GetEventToiletPath(Event8ToiletArry, randomPath);
    }
    
    public Vector3[] GetEvent6ToiletPath(EventRandomPath randomPath)
    {
        return GetEventToiletPath(Event6ToiletArry, randomPath);
    }

    private Vector3[] GetEventToiletPath(Vector3[] toiletArry, EventRandomPath randomPath)
    {
        var index = (int)randomPath;
        Vector3[] pos = new Vector3[3];

        var ent = index * 3;
        var start = ent - 3;
        var count = 0; 
        for (var i = start; i < ent; i++)
        {
            pos[count] = toiletArry[i];
            count++;
        }
        
        return pos;
    }
    
    public Vector3[] GetEventToiletActPath(Vector3[] path)
    {
        Vector3[] pos = new Vector3[2];
        for (int i = 0; i < path.Length - 1; i++)
        {
            pos[i] = path[i];
        }
        
        return pos;
    }

    public Vector3[] GetEventToiletActPoint(Vector3[] path)
    {
        Vector3[] pos = new Vector3[1];
        pos[0] = path[path.Length - 1];
        return pos;
    }
    
    public Vector3[] GetBackBathePath(Vector3[] pathArry, Vector3 target)
    {
        Vector3[] pos = new Vector3[pathArry.Length];
        int index = 0;
        for (int i = pathArry.Length - 1; i >= 0 ; i--)
        {
            if (i == pathArry.Length - 1)
            {
                continue;
            }
            
            pos[index] = pathArry[i];
            index++;
        }

        pos[pathArry.Length - 1] = target;
        return pos;
    }
    
    public Vector3[] GetBackPath(Vector3[] pathArry, Vector3 target)
    {
        Vector3[] pos = new Vector3[pathArry.Length + 1];
        int index = 0;
        for (int i = pathArry.Length - 1; i >= 0 ; i--)
        {
            pos[index] = pathArry[i];
            index++;
        }

        pos[pathArry.Length] = target;
        return pos;
    }
}