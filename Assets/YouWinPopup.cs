using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouWinPopup : MonoBehaviour
{
    private int wrong;
    public static YouWinPopup Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);
    }
    public Text text_time;
    public Text text_wrong;
    public Text text_game_mode;
    void Start()
    {
        GetGameResults();
    }
    public void GetGameResults()
    {
        text_time.text = Clock.Instance.GetCurrentTimeText().text;
        text_wrong.text = Lives.Instance.error_number_.ToString();
        text_game_mode.text = GameSetting.Instance.GetGameMode();
    }
     private void SetStatisticsData()
    {
        int level = Dropdown.Instance.grid_mode; //4, 6, 9
        string game_mode = GameSetting.Instance.GetGameMode();// Easy 4x4, Medium 4x4, ... Very Hard 9x9
        if (wrong <= PlayerPrefs.GetInt(""));
        {
            PlayerPrefs.SetString(game_mode + "_Time", text_time.text);
            PlayerPrefs.SetString(game_mode + "_Wrong", text_wrong.text);
        }
    }
}
