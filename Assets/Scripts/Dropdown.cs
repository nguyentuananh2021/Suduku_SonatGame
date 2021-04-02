using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dropdown : MonoBehaviour
{
    public static Dropdown Instance;
    public int grid_mode;
    
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
        GetComponent<TMP_Dropdown>().value = PlayerPrefs.GetInt("grid_mode_data");
    }

    public int GetGridMode()
    {
        return grid_mode;
    }
    public void SetDropdownValue()
    {
        int value = this.GetComponent<TMP_Dropdown>().value;
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
