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

    public void SetLevelGrid()
    {
        level_grid = GameSetting.Instance.GetGameMode();
    }
    public string GetLevelGrid()
    {
        return level_grid;
    }
    public void Display()
    {
        level_text.GetComponent<Text>().text = level_grid;
    }

}
