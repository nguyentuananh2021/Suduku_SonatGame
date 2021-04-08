using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    public static BackButton Instance;
    
    private void Awake()
    {
        if (Instance) Destroy(this);
        Instance = this;
    }
    public void SetJsonData()
    {
       
    }
    private void SetDataNotes(Data data)
    {
        int n = Dropdown.Instance.GetGridMode();
        var grid_square = SudukuGrid.Instance.grid_squares_;
        data.arr_notes = new string[grid_square.Count]; // ["1 2 8 ...","",""]
        //data_notes = new Dictionary<int, List<string>>();
        for (int i = 0; i < grid_square.Count; i++)
        {
            for (int j = 0; j < n; j++)
            {
                var note = grid_square[i].GetComponent<GridSquare>().GetSquareNotes()[j];
                if ( note != "")
                {
                    data.arr_notes[i] += note + " ";
                }
            }
        }
    }

    public void SaveJsonData()
    {
        Data data = new Data();
        data.grid_mode = Level.Instance.GetLevelGrid();
        data.times_ = Clock.Instance.GetCurrentTimeText();
        data.wrongs_ = Lives.Instance.GetWrong();
        data.solved_data = SudukuData.Instance.data.solved_data;
        data.unsolved_data = SudukuData.Instance.data.unsolved_data;
        data.unsolved_data_base = SudukuData.Instance.data.unsolved_data_base;
        SetDataNotes(data);

        string json_data = JsonUtility.ToJson(data, true);
        
        PlayerPrefs.SetString("json_data", json_data);
        Debug.Log("save file done ---- playerPref: json_data");
        Debug.Log(PlayerPrefs.GetString("json_data"));
    }
}
