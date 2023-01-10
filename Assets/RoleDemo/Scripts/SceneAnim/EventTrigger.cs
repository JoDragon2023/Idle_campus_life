using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public Action<string> TriggerEnter;

    // 开始接触
    void OnTriggerEnter(Collider collider) {
        TriggerEnter?.Invoke(collider.transform.name);
    }
}
