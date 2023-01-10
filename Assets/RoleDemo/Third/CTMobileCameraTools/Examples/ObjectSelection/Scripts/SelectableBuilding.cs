//SIMPLE SELECTABLE BUILDING 
using UnityEngine;

public class SelectableBuilding : MonoBehaviour
{
    //COLOR WHEN SELECTED
    public Color selectedColor;
    //CACHE INITIAL COLOR TO RESTORE IT WHEN DESELECTING
    private Color initialColor;
    public Renderer rend;
    private bool isSelected = false;
    void Start()
    {
        initialColor = rend.material.color;
    }
    void OnMouseDown()
    {
        //SET SELECTED AND REPORT SELF TO MANAGER 
        if (!isSelected)
        {
            isSelected = true;
            rend.material.color = selectedColor;
            SelectionController.instance.SetSelected(this);
        }
    }
    public void Deselect()
    {
        isSelected = false;
        rend.material.color = initialColor;
    }
}
