using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Transform[] nodes;
    private Transform currentTarget;
    public int currentTargetID = 0;
    public float speed = 5f;
    void Start()
    {
        transform.position = nodes[currentTargetID].position;
        UpdateTarget();
        speed += Random.Range(-1f,1f);
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, currentTarget.position) > 0.3f)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, Time.deltaTime * speed);
        }
        else
        {
            UpdateTarget();
        }
    }
    private void UpdateTarget()
    {
        currentTargetID++;

        if (currentTargetID >= nodes.Length)
            currentTargetID = 0;

        currentTarget = nodes[currentTargetID];
        transform.LookAt(currentTarget);
    }
}
