using UnityEngine;
using UnityEngine.UI;

public class DropdownGridMode : MonoBehaviour
{
    public static DropdownGridMode Instance;
    private int grid_mode = 9;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }
    void Start()
    {
        GetComponent<Dropdown>().value = PlayerPrefs.GetInt("grid_mode_data");
    }

    public int GetGridMode()
    {
        return grid_mode;
    }
    public void SetGridMode(int grid_mode_)
    {
        grid_mode = grid_mode_;
    }
    public void SetDropdownValue()
    {
        int value = this.GetComponent<Dropdown>().value;
        switch (value)
        {
            case 0:
                grid_mode = 4;
                break;
            case 1:
                grid_mode = 6;
                break;
            case 2:
                grid_mode = 9;
                break;
        }
        PlayerPrefs.SetInt("grid_mode_data", value);
    }
}
