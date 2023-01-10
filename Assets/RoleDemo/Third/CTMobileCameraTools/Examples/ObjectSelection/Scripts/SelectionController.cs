//STORES CURRENTLY SELECTED BUILDING AND DESELECTS PREVIOUSLY SELECTED 
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    public static SelectionController instance;
    private SelectableBuilding currentlySelected; 
    void Awake()
    {
        instance = this;
    }
    public void SetSelected(SelectableBuilding selectedBuilding)
    {
        //IF HAD A BUILDING PREVIOUSLY SELECTED, DESELECT IT
        if(currentlySelected)
        currentlySelected.Deselect();

        //CACHE NEW BUILDING AS CURRENTLY SELECTED
        currentlySelected = selectedBuilding;
    }

}
