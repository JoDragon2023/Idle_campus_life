using System.Collections;
using System.Collections.Generic;
using Exoa.Events;
using UnityEngine;

public class TestSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        CameraEvents.OnRequestObjectFollow?.Invoke(gameObject, true, false);
        
    }
}
