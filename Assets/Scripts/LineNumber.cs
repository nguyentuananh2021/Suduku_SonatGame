using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineNumber : MonoBehaviour
{
    public GameObject grid_layout;
    public List<GameObject> numbers_;
    private void Start()
    {
        SetDeActivateAll();
        SetActivate(DropdownGridMode.Instance.GetGridMode());
        
    }
    public void SetActivate(int grid_mode)
    {
        switch (grid_mode)
        {
            case 4:
                grid_layout.GetComponent<GridLayoutGroup>().spacing = new Vector2(250,0);
                
                Activate(4);
                break;
            case 6:
                grid_layout.GetComponent<GridLayoutGroup>().spacing = new Vector2(125, 0);
                Activate(6);
                break;
            case 9:
                grid_layout.GetComponent<GridLayoutGroup>().spacing = new Vector2(50, 0);
                Activate(9);
                break;
        }
    }
    private void SetDeActivateAll()
    {
        foreach (var item in numbers_)
        {
            item.SetActive(false);
        }
    }
    private void Activate( int n)
    {
        for (int i = 0; i < n; i++)
        {
            numbers_[i].SetActive(true);
        }
    }
}
