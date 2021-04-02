using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
public partial class SudukuJSON : MonoBehaviour
{
    public static SudukuJSON Instance;
    private string grid_mode;
    private string times_;
    private string wrongs_;
    private string solved_data;
    private string unsolved_data;
    private string unsolved_data_;
    private static string json_data;
    private void Awake()
    {
        if (Instance) Destroy(this);
        Instance = this;

    }
    private void Start()
    {
        SetJsonData();
    }
    public string GetValueContinueGame()
    {
        return grid_mode + " " + times_;
    }
    public void SetJsonData()
    {
        grid_mode = Level.Instance.GetLevelGrid();
        times_ = Clock.Instance.GetCurrentTimeText().ToString();
        wrongs_ = Lives.Instance.GetWrong().ToString();
        solved_data = ArrayToString(SudukuData.Instance.data.solved_data);
        unsolved_data = ArrayToString(SudukuData.Instance.data.unsolved_data);
        unsolved_data = ArrayToString(SudukuData.Instance.unsolve_data_base);

        PlayerPrefs.SetInt("continue", 1);
        PlayerPrefs.Save();
    }
    private string ArrayToString(int[] arr)
    {
        string str = "";
        foreach (var item in arr)
        {
            str += item + ",";
        }
        return str;
    }
    public void SaveData()
    {
        JsonData suduku_json_data = new JsonData(grid_mode, times_, wrongs_, solved_data, unsolved_data, unsolved_data_);
        json_data = JsonUtility.ToJson(suduku_json_data);
    }
    public JsonData GetJsonData()
    {
        var data = JsonUtility.FromJson<JsonData>(json_data);
        return data;
    }
}

