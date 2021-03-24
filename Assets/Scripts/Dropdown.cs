using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dropdown : MonoBehaviour
{
    public int grid_mode;
    public static Dropdown Instance;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
        GetDropdownvalue();
    }
    //void Start()
    //{
    //GetComponent<TMP_Dropdown>().value = PlayerPrefs.GetInt("grid_mode_data");
    //Debug.Log(PlayerPrefs.GetInt("grid_mode_data"));
    //}
    public void GetDropdownvalue()
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
        //PlayerPrefs.SetInt("grid_mode_data", value);
    }
}
