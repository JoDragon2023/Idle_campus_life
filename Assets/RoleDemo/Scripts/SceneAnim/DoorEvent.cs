using System;
using UnityEngine;

public class DoorEvent: MonoBehaviour
{
    public DoorRayType doorRayType = DoorRayType.Forward;
    /// <summary>
    /// 当前门的信息与操作
    /// </summary>
    public DoorAnim doorAnim;
    /// <summary>
    /// 检测距离
    /// </summary>
    public float maxDistance = 3;
    public Action<DoorAnim, Collider> rayEvent;

    /// <summary>
    /// 只检测 Role层
    /// </summary>
    private int layerMask = 6;
    
    private RaycastHit hit;
    private void Awake()
    {
        //如果不想让某一层参与检测，比如Player层=5
        layerMask = 1 << 5;
        //~(1 << 5) 检测除了第5之外的层。
        layerMask = ~layerMask;
    }

    void FixedUpdate()
    {
        //参数：当前物体，世界空间的方向，碰撞信息，最大距离，检测层
        if (Physics.Raycast(transform.position, transform.TransformDirection(GetDoorRayTypePos()), out hit, maxDistance, layerMask))
        {
            //返回检测到的物体的名字
            //Debug.Log(hit.transform.name);
            rayEvent?.Invoke(doorAnim, hit.collider);
        }
    }

    private Vector3 GetDoorRayTypePos()
    {
        switch (doorRayType)
        {
            case DoorRayType.Forward:
                return Vector3.forward;
                break;
            case DoorRayType.Back:
                return Vector3.back;
                break;
            case DoorRayType.Down:
                return Vector3.down;
                break;
            case DoorRayType.Up:
                return Vector3.up;
                break;
            case DoorRayType.Left:
                return Vector3.left;
                break;
            case DoorRayType.Right:
                return Vector3.right;
                break;
        }
        return Vector3.forward;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, GetDoorRayTypePos() * maxDistance);
    }
}