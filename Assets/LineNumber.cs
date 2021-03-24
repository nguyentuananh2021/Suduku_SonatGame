using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineNumber : MonoBehaviour
{
    public List<GameObject> numbers_;
    private void Start()
    {
        SetDeActivateAll();
        SetActivate(Dropdown.Instance.grid_mode);
        
    }
    public void SetActivate(int grid_mode)
    {
        switch (grid_mode)
        {
            case 4:
                Activate(4);
                break;
            case 6:
                Activate(6);
                break;
            case 9:
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
