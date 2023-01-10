using UnityEngine;

///<summary>
///脚本单例类,负责为唯一脚本创建实例
///<summary>

public class MonoSingleton<T> : MonoBehaviour where T:MonoSingleton<T> //注意此约束为T必须为其本身或子类
{
    private static T instance; //创建私有对象记录取值，可只赋值一次避免多次赋值

    public static T Instance
    {
        //实现按需加载
        get
        {
            //当已经赋值，则直接返回即可
            if (instance != null) return instance;

            GameObject _mgrRoot = GameObject.Find("__Manager__");
            
            if (_mgrRoot == null)
            {
                _mgrRoot = new GameObject("__Manager__");
                DontDestroyOnLoad(_mgrRoot);
            }
            
            instance = _mgrRoot.GetComponent<T>();
            if (instance == null)
            {
                instance = _mgrRoot.AddComponent<T>();
            } 
           
            return instance;
        }
    }

    public virtual void Dispose()
    {
    }
    
}