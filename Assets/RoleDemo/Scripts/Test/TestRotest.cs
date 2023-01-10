using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TestRotest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       transform.DORotate(new Vector3(0, 25, 0), 0.2f, RotateMode.Fast);
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
