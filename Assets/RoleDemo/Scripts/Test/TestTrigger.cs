using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    public Action<string> TriggerEnter;

    // 开始接触
    void OnTriggerEnter(Collider collider) {
        TriggerEnter?.Invoke(collider.transform.name);
    }
    //
    // // 接触结束
    // void OnTriggerExit(Collider collider) {
    //     Debug.Log("接触结束");
    // }
    //
    // // 接触持续中
    // void OnTriggerStay(Collider collider) {
    //     Debug.Log("接触持续中");
    // }
}
