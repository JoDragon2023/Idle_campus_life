using System;
using UnityEngine;

public class BaseObj
{
    public int objId = -1;
    public GameObject go;
    public GameObject target;
    
    public RoleData m_roleData;
    public RoleData Data
    {
        get { return GetBaseData();}
        set { m_roleData = value; }
    }
    public bool isSelect;

    public BaseObj()
    {
        if (objId < 0)
            objId = SceneObjMgr.Instance.CreateObjId();
    }
    
    
    protected void LoadGo(string asset, Transform parent, Action<GameObject> loadCB = null)
    {
        if (go == null)
        {
            GameObject gameObject = LoadHelper.LoadPrefab(asset);
            if (gameObject == null)
            {
                Debug.LogError("LoadGo == null");
                return;
            }
            go = GameObject.Instantiate(gameObject);
        }

        go.transform.SetParent(parent);
      
        if (Data != null)
        {
            go.transform.position = Data.pos;
        }

        loadCB?.Invoke(go);
        LoadGoCallBack(go);
    }

    protected void LoadTarget(string asset, Transform parent, Action<GameObject> loadCB = null)
    {
        if (target == null)
        {
            GameObject gameObject = LoadHelper.LoadPrefab(asset);
            if (gameObject == null)
            {
                Debug.LogError("LoadGo == null");
                return;
            }
            target = GameObject.Instantiate(gameObject);
        }
        
        target.transform.SetParent(parent);
        loadCB?.Invoke(target);
    }
    
    
    internal void SetData(RoleData data)
    {
        this.Data = data;

        if (go == null)
            return;

        go.transform.position = data.pos;
    }

    public void SetPos(Vector3 pos)
    {
        this.go.transform.position = pos;
    }
    
    
    protected virtual RoleData GetBaseData()
    {
        return m_roleData;
    }

    public virtual void OnExit()
    {

    }
    
    protected virtual void LoadGoCallBack(GameObject go)
    {
        RefreshCellInfo(true);
    }

    public virtual void OnClick()
    {

    }

    public virtual void OnDelete()
    {
        RefreshCellInfo(false);
    }

    public virtual void RefreshCellInfo(bool isNew)
    {
            
    }
}