using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    public Text level_text;
    public static Level Instance;
    private string level_grid;
    private void Awake()
    {
        if (Instance) Destroy(this);
        Instance = this;
    }
    private void Start()
    {
        SetLevelGrid();
        Display();
    }

    private void SetLevelGrid()
    {
        if (GameSetting.Instance.IsDaily())
        {
            var crr = Calendar.Instance.currDate;
            level_grid = crr.Year + "." + crr.Month + "." + crr.Day;
        }
        else
            level_grid = GameSetting.Instance.GetGameMode();
    }
    public string GetLevelGrid()
    {
        return level_grid;
    }
    private void Display()
    {
        level_text.GetComponent<Text>().text = level_grid;
    }

}
