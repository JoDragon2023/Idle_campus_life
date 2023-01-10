using UnityEngine;

public class NodeManager : MonoBehaviour
{

    public static NodeManager instance;
    //CAMERA AREA CORNER POSITIONS
    [HideInInspector]
    public Vector3[] nodePositions = new Vector3[4];
    //DISPLAY GIZMOS IN SCENE VIEW DURING GAMEPLAY 
    public bool showCamAreaGizmos = false;
    void Awake()
    {
        instance = this;
    }
    void OnDrawGizmos()
    {
        if (showCamAreaGizmos)
        {
            Vector3 pos = new Vector3((nodePositions[1].x + nodePositions[0].x) / 2f, 10, (nodePositions[0].z + nodePositions[3].z) / 2f);
            Vector3 size = new Vector3(nodePositions[1].x - nodePositions[0].x, 20, nodePositions[0].z - nodePositions[3].z);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(pos, size);
        }
    }
}
