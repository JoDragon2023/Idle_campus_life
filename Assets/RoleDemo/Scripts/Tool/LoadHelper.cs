using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadHelper :MonoSingleton<LoadHelper>
{
    public static GameObject LoadPrefab(string path)
    {
        //用加载得到的资源对象，实例化游戏对象，实现游戏物体的动态加载
        return Resources.Load(path, typeof(GameObject)) as GameObject;
    }

  
}
