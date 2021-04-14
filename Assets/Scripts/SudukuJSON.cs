using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Data
{
    public string grid_mode;
    public string times_;
    public int wrongs_;
    public int hint_;
    public int[] solved_data;
    public int[] unsolved_data;
    public int[] unsolved_data_base;
    public string[] arr_notes;
}
public class SudukuJSON : MonoBehaviour
{
    public static SudukuJSON Instance;
    public Data data = new Data();
     private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
            Destroy(this);
        GetDataFile();
    }
    public void ResetFile()
    {
        if (PlayerPrefs.GetString("json_data") != "")
        {
            PlayerPrefs.SetString("json_data", "");
        }
    }
    public void GetDataFile()
    {
        data = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("json_data"));
    }

    public void SetGamePlay()
    {
        DropdownGridMode.Instance.SetGridMode(int.Parse(data.grid_mode.Split('x')[1]));
        GameSetting.Instance.SetGameMode(GetGameMode());
        SudukuData.Instance.data = new SudukuData.SudukuBoardData(data.unsolved_data, data.solved_data, data.unsolved_data_base);
        SceneManager.LoadScene("GameScene");
    }
    public string GetGameMode()
    {
        string game_mode = "";
        var json_data = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("json_data"));
        if (PlayerPrefs.GetString("json_data") != null)
        {
           
            string[] str = json_data.grid_mode.Split(' ');
            for (int i = 0; i < str.Length; i++)
            {
                game_mode += str[i];
                if (i < str.Length - 1) game_mode += "_";
            }
        }
        return game_mode;
    }
}

