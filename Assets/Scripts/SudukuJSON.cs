using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
public class SudukuJSON : MonoBehaviour
{
    public void Start()
    {
        SudukuData sudukuData = new SudukuData();
        sudukuData.grid_mode = 4;
        sudukuData.level = "Easy";
        sudukuData.square_notes.Add("",new string[2]{"1", "2"});
       
        //Debug.Log(sudukuData.square_notes.Count);
        string json = JsonUtility.ToJson(sudukuData, true);
        //Debug.Log(json);

        //SudukuData su = JsonUtility.FromJson<SudukuData>(json);
        //Debug.Log("grid mode: "+su.grid_mode);
        //Debug.Log("level: "+su.level);
        //Debug.Log("index: "+su.square_notes.Count);
    }
    
    public class SudukuData
    {
        public int grid_mode;
        public string level;
        public int[] indexs;
       
        public Dictionary<string, string[]> square_notes = new Dictionary<string, string[]>();
        
    }
}

