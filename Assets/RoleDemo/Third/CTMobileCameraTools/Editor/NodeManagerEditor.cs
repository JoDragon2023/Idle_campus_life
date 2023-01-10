using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(NodeManager))]
public class NodeManagerEditor : Editor
{
    [MenuItem("Tools/CTMobileCameraTools/Init Scene")]
    static void InitPrefabs()
    {


        if (!GameObject.Find("CTCameraController"))
        {
            //disable main camera 
            if (Camera.main)
            {
                Camera.main.name = "OLD CAMERA";
                Camera.main.gameObject.SetActive(false);
            }

            GameObject camControlerGO = Instantiate(Resources.Load("Prefabs/CTCameraController") as GameObject, Vector3.zero, Quaternion.identity) as GameObject;
            camControlerGO.name = "CTCameraController";
        }
        else
        {
            Debug.LogWarning("CTCameraController is already in the scene");
        }

        if (!GameObject.Find("CTNodeController"))
        {
            GameObject nodesGO = Instantiate(Resources.Load("Prefabs/CTNodeController") as GameObject, Vector3.zero, Quaternion.identity) as GameObject;
            nodesGO.name = "CTNodeController";
        }
        else
        {
            Debug.LogWarning("CTNodeController is already in the scene");
        }

    }

    void OnSceneGUI()
    {

        if (Application.isPlaying)
            return;

        NodeManager nodeManager = (NodeManager)target;
        Handles.color = Color.red;
        float minSize = 10f;

        //DRAW CAMERA AREA LINES
        Handles.DrawDottedLine(nodeManager.nodePositions[0], nodeManager.nodePositions[1],5);
        Handles.DrawDottedLine(nodeManager.nodePositions[1], nodeManager.nodePositions[2],5);
        Handles.DrawDottedLine(nodeManager.nodePositions[2], nodeManager.nodePositions[3],5);
        Handles.DrawDottedLine(nodeManager.nodePositions[3], nodeManager.nodePositions[0],5);
     
        Handles.color = Color.green;

        //ADD HANDLES AND CONTROL POSITION
        for (int i = 0; i < 4; i++) 
        {

            EditorGUI.BeginChangeCheck();
            Vector3 nodePosition = Handles.FreeMoveHandle(nodeManager.nodePositions[i], Quaternion.identity,5,Vector3.one * 0.5f,Handles.SphereHandleCap);

            //CLAMP Y TO 0
            nodePosition.y = 0;

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(nodeManager, "Change Node Position");
                if (nodeManager.nodePositions.Length == 0)
                {
                    nodeManager.nodePositions = new Vector3[4];
                }
                //CLAMP POSITION TO AVOID CREATING INVERSE SHAPE
                if (i == 0)
                {
                    if (nodePosition.x + minSize >= nodeManager.nodePositions[1].x) nodePosition.x = nodeManager.nodePositions[1].x - minSize;
                    if (nodePosition.z - minSize <= nodeManager.nodePositions[3].z) nodePosition.z = nodeManager.nodePositions[3].z + minSize;
                }
                else if (i == 1)
                {
                    if (nodePosition.x - minSize <= nodeManager.nodePositions[0].x) nodePosition.x = nodeManager.nodePositions[0].x + minSize;
                    if (nodePosition.z - minSize <= nodeManager.nodePositions[3].z) nodePosition.z = nodeManager.nodePositions[3].z + minSize;
                }
                else if (i == 2)
                {
                    if (nodePosition.x - minSize <= nodeManager.nodePositions[3].x) nodePosition.x = nodeManager.nodePositions[3].x + minSize;
                    if (nodePosition.z + minSize >= nodeManager.nodePositions[1].z) nodePosition.z = nodeManager.nodePositions[1].z - minSize;
                }
                else if (i == 3)
                {
                    if (nodePosition.x + minSize >= nodeManager.nodePositions[1].x) nodePosition.x = nodeManager.nodePositions[1].x - minSize;
                    if (nodePosition.z + minSize >= nodeManager.nodePositions[1].z) nodePosition.z = nodeManager.nodePositions[1].z - minSize;
                }
                //ADJUST CORRESPONDING NODES TO KEEP SQUARE SHAPE
                nodeManager.nodePositions[i] = nodePosition;
                if (i == 0)
                {
                    nodeManager.nodePositions[1] = new Vector3(nodeManager.nodePositions[1].x, 0, nodeManager.nodePositions[0].z);
                    nodeManager.nodePositions[3] = new Vector3(nodeManager.nodePositions[0].x, 0, nodeManager.nodePositions[3].z);
                }
                else if (i == 1)
                {
                    nodeManager.nodePositions[0] = new Vector3(nodeManager.nodePositions[0].x, 0, nodeManager.nodePositions[1].z);
                    nodeManager.nodePositions[2] = new Vector3(nodeManager.nodePositions[1].x, 0, nodeManager.nodePositions[2].z);
                }
                else if (i == 2)
                {
                    nodeManager.nodePositions[1] = new Vector3(nodeManager.nodePositions[2].x, 0, nodeManager.nodePositions[1].z);
                    nodeManager.nodePositions[3] = new Vector3(nodeManager.nodePositions[3].x, 0, nodeManager.nodePositions[2].z);
                }
                else if (i == 3)
                {
                    nodeManager.nodePositions[0] = new Vector3(nodeManager.nodePositions[3].x, 0, nodeManager.nodePositions[0].z);
                    nodeManager.nodePositions[2] = new Vector3(nodeManager.nodePositions[2].x, 0, nodeManager.nodePositions[3].z);
                }

            }
        }
    }
 
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        NodeManager nodeManager = (NodeManager)target;
        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space();

        EditorGUILayout.HelpBox("Reset nodes positions to initial values", MessageType.Info);
        EditorGUILayout.Space();

        EditorGUI.BeginChangeCheck();
        if (GUILayout.Button("Initialize Nodes"))
        {
            Undo.RecordObject(nodeManager, "Reset Nodes");
            nodeManager.nodePositions[0] = nodeManager.transform.position + new Vector3(-5, 0, 5);
            nodeManager.nodePositions[1] = nodeManager.transform.position + new Vector3(5, 0, 5);
            nodeManager.nodePositions[2] = nodeManager.transform.position + new Vector3(5, 0, -5);
            nodeManager.nodePositions[3] = nodeManager.transform.position + new Vector3(-5, 0, -5);
        }
        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();
    }
}
