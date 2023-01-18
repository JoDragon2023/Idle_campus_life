using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class TestMove : MonoBehaviour
{
    public GameObject Player;
    public float m_speed = 5f;
 
    void Update()
    {
        //键盘控制移动
        PlayerMove_KeyTransform2();
    }
 
    //通过Transform组件 键盘控制移动 另一种写法
    public void PlayerMove_KeyTransform2()
    {
        float horizontal = Input.GetAxis("Horizontal"); //A D 左右
        float vertical = Input.GetAxis("Vertical"); //W S 上 下
 
        Player.transform.Translate(Vector3.forward * vertical * m_speed * Time.deltaTime);//W S 上 下
        Player.transform.Translate(Vector3.right * horizontal * m_speed * Time.deltaTime);//A D 左右
    }
}
