using UnityEngine;
using CTMobileCameraTools;

public class CityExample : MonoBehaviour
{
    //ARRAY OF CARS IN THE SCENE
    public Transform[] carsToFollow;
    private int currentCarID;
    //THESE METHODS ARE CALLED FROM UI    
    //SETS FOLLOW TARGET TO NEXT IN THE ARRAY
    public void SetNextTarget()
    {
        //INCREASE CURRENTLY FOLLOWED ID
        currentCarID++;
        //IF OUT OF BOUNDS RESET TO 0
        if (currentCarID >= carsToFollow.Length)
        {
            currentCarID = 0;
        }
        //SET THE CORRESPONDING TRANSFORMS
        CTController.SetTarget(carsToFollow[currentCarID]);
    }
    public void SetPreviousTarget()
    {
        currentCarID--;
        if (currentCarID < 0)
        {
            currentCarID = carsToFollow.Length - 1;
        }
        CTController.SetTarget(carsToFollow[currentCarID]);
    }
    public void ToggleCameraFollow()
    {
        if (CTController.IsCameraFollowPaused())
        {
            CTController.ResumeCameraFollow();
        }
        else
        {
            CTController.PauseCameraFollow();
        }
    }
    //DECREASES CURRENT MIN BOUNDS BY 5 IN X AND Z (MOVES THE CAMERA PAN BOUNDARY NODES)Î
    public void DecreseBounds()
    {

        //GET CURRENT BOUNDS
        Vector3 boundsMin = CTController.GetBoundsMin();
        Vector3 boundsMax = CTController.GetBoundsMax();

        //SET NEW BOUNDS MIN 
        CTController.SetBoundsMin(boundsMin.x + 5f, boundsMin.z + 5f);
        CTController.SetBoundsMax(boundsMax.x - 5f, boundsMax.z - 5f);

    }
    public void IncreaseBounds()
    {
        //INCREASES CURRENT MAX BOUNDS BY 5 IN X AND Z (MOVES THE CAMERA PAN BOUNDARY NODES)

        //GET CURRENT BOUNDS
        Vector3 boundsMin = CTController.GetBoundsMin();
        Vector3 boundsMax = CTController.GetBoundsMax();

        //SET NEW BOUNDS MIN 
        CTController.SetBoundsMin(boundsMin.x - 5f, boundsMin.z - 5f);
        CTController.SetBoundsMax(boundsMax.x + 5f, boundsMax.z + 5f);
    }
}
