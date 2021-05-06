using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineNumber : MonoBehaviour
{
    public static LineNumber Instance;
    public List<NumberButton> numbers_;
    private void Awake()
    {
        if (Instance) Destroy(this);
        Instance = this;
    }
    private void Start()
    {
        SetDeActivateAll();
        SetActivate(DropdownGridMode.Instance.GetGridMode());
        SetDisableAllNumber();
    }
    public void SetActivate(int grid_mode)
    {
        switch (grid_mode)
        {
            case 4:
                GetComponent<GridLayoutGroup>().spacing = new Vector2(225,0);
                Activate(4);
                break;
            case 6:
                GetComponent<GridLayoutGroup>().spacing = new Vector2(100, 0);
                Activate(6);
                break;
            case 9:
                GetComponent<GridLayoutGroup>().spacing = new Vector2(25, 0);
                Activate(9);
                break;
        }
    }
    private void SetDeActivateAll()
    {
        foreach (var item in numbers_)
        {
            item.gameObject.SetActive(false);
        }
    }
    private void Activate( int n)
    {
        for (int i = 0; i < n; i++)
        {
            numbers_[i].gameObject.SetActive(true);
        }
    }
    public void SetActiveButton(List<int> numbers)
    {
        SetDisableAllNumber();
        foreach (var item in numbers)
        {
            numbers_[item - 1].interactable = true;
        }
    }
    private void SetDisableAllNumber()
    {
        foreach (var item in numbers_)
        {
            item.interactable = false;
        }
    }
}
