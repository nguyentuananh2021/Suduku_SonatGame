using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameSetting : MonoBehaviour
{
    public int dropdown_value = 0;
    public TMP_Dropdown dropdown;
    public enum EGameMode
    {
        NOT_SET,
        EASY_9x9, EASY_6x6, EASY_4x4,
        MEDIUM_9x9, MEDIUM_6x6, MEDIUM_4x4,
        HARD_9x9, HARD_6x6, HARD_4x4, 
        VERY_HARD_9x9, VERY_HARD_6x6, VERY_HARD_4x4
    }
    public static GameSetting Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
            Destroy(this);
    }
    private EGameMode _GameMode;
    private void Start()
    {
        _GameMode = EGameMode.NOT_SET;
        dropdown_value = 0;
    }

    public void SetGameMode(EGameMode mode)
    {
        _GameMode = mode;
    }
    public void SetGameMode(string mode)
    {
        switch (dropdown_value)
        {
            case 0:
                if (mode == "Easy_4x4") SetGameMode(EGameMode.EASY_4x4);
                else if (mode == "Medium_4x4") SetGameMode(EGameMode.MEDIUM_4x4);
                else if (mode == "Hard_4x4") SetGameMode(EGameMode.HARD_4x4);
                else if (mode == "Very_Hard_4x4") SetGameMode(EGameMode.VERY_HARD_4x4);
                else SetGameMode(EGameMode.NOT_SET);
                break;
            case 1:
                if (mode == "Easy_6x6") SetGameMode(EGameMode.EASY_6x6);
                else if (mode == "Medium_6x6") SetGameMode(EGameMode.MEDIUM_6x6);
                else if (mode == "Hard_6x6") SetGameMode(EGameMode.HARD_6x6);
                else if (mode == "Very_Hard_6x6") SetGameMode(EGameMode.VERY_HARD_6x6);
                else SetGameMode(EGameMode.NOT_SET);
                break;
            case 2:
                if (mode == "Easy_9x9") SetGameMode(EGameMode.EASY_9x9);
                else if (mode == "Medium_9x9") SetGameMode(EGameMode.MEDIUM_9x9);
                else if (mode == "Hard_9x9") SetGameMode(EGameMode.HARD_9x9);
                else if (mode == "Very_Hard_9x9") SetGameMode(EGameMode.VERY_HARD_9x9);
                else SetGameMode(EGameMode.NOT_SET);
                break;
        }
        
    }

    public string GetGameMode()
    {
        switch (_GameMode)
        {

            case EGameMode.EASY_4x4: return "Easy_4x4";
            case EGameMode.MEDIUM_4x4: return "Medium_4x4";
            case EGameMode.HARD_4x4: return "Hard_4x4";
            case EGameMode.VERY_HARD_4x4: return "Very_Hard_4x4";

            case EGameMode.EASY_6x6: return "Easy_6x6";
            case EGameMode.MEDIUM_6x6: return "Medium_6x6";
            case EGameMode.HARD_6x6: return "Hard_6x6";
            case EGameMode.VERY_HARD_6x6: return "Very_Hard_6x6";
            
            case EGameMode.EASY_9x9: return "Easy_9x9";
            case EGameMode.MEDIUM_9x9: return "Medium_9x9";
            case EGameMode.HARD_9x9: return "Hard_9x9";
            case EGameMode.VERY_HARD_9x9: return "Very_Hard_9x9";
        }
        Debug.Log("Error: game level is not set...!");
        return "";
    }

    public void GetDropdownvalue()
    {
        dropdown_value = dropdown.GetComponent<TMP_Dropdown>().value;
    }
    public int SetGridMode()
    {
        var value = 0;
        switch (dropdown_value)
        {
            case 0:
                value = 4;
                break;
            case 1:
                value = 6;
                break;
            case 2:
                value = 9;
                break;
        }
        return value;
    }
}
