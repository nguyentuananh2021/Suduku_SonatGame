using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouWinPopup : MonoBehaviour
{
    public static YouWinPopup Instance;
    public List<GameObject> trophy_icons;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);
    }
    private string time_data;
    private string wrong_data;
    private string game_mode;
    private string level_mode;

    public Text text_time;
    public Text text_wrong;
    public Text text_game_mode; // Easy 4x4, Medium 6x6, ...
    void Start()
    {

        GetValuePopup();
        GetData();
        SaveData();
        DisplayGameResults();
    }
    public void GetValuePopup()
    {
        text_time.text = Clock.Instance.GetCurrentTimeText().text;
        text_wrong.text = Lives.Instance.error_number_.ToString();
        text_game_mode.text = GameSetting.Instance.GetGameMode();
    }
    public void GetData()
    {
        game_mode = GameSetting.Instance.GetGameMode().Split(' ')[1];// 4x4 , 6x6 , 9x9
        level_mode = GameSetting.Instance.GetGameMode().Split(' ')[0];
        time_data = PlayerPrefs.GetString(game_mode + "_Times_" + LevelToInt(level_mode));
        if (wrong_data == "") wrong_data = "3";
        if (time_data == "") time_data = "99:99:99";
        wrong_data = PlayerPrefs.GetString(game_mode + "_wrongs_" + LevelToInt(level_mode));
        //Debug.Log(wrong_data + "-" + time_data);
    }

    public bool IsBest()
    {
        var r = TimeToInt(time_data) - TimeToInt(text_time.text);
        if (r > 0)
        {
            return true;
        }
        if(r < 0)
        {
            return false;
        } 
        if(r == 0)
        {
            if (int.Parse(wrong_data) > int.Parse(text_wrong.text))
                return true;
            else return false;
        }
        return false;
    }
     private void SaveData()
    {
        if (IsBest() == true)
        {
           // Debug.Log("is best = " + IsBest());
            PlayerPrefs.SetString(game_mode + "_times_" + LevelToInt(level_mode),text_time.text); // 4x4_times_0
            PlayerPrefs.SetString(game_mode + "_wrongs_" + LevelToInt(level_mode),text_wrong.text);
            PlayerPrefs.Save();
        }
    }
    private int LevelToInt(string level_mode)
    {
        switch (level_mode)
        {
            case "Easy":
                return 0;
            case "Medium":
                return 1;
            case "Hard":
                return 2;
            case "VeryHard":
                return 3;
        }
        return 5;
    }
    private int TimeToInt(string str)
    {
        string[] arrStr = str.Split(':');
        string s = "";
        foreach (var item in arrStr)
        {
            s += item;
        }
        return int.Parse(s);
    }

    private void DisplayGameResults()
    {
        //trophy_icons[0].SetActive(true);
        if (IsBest() == true)
        {
            trophy_icons[0].SetActive(false);
            text_game_mode.text = "Best " + GameSetting.Instance.GetGameMode();
        }
        else trophy_icons[0].SetActive(true);
    }
 
}
