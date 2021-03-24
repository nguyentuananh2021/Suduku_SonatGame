using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudukuData : MonoBehaviour
{
    public GameObject you_win;
    public SudukuBoardData data = new SudukuBoardData();
    public static SudukuData Instance;
    public int square_empty;
    
    public struct SudukuBoardData
    {
        public int[] unsolve_data;
        public int[] solve_data;

        public SudukuBoardData(int[] unsolve_data, int[] solve_data) : this()
        {
            this.unsolve_data = unsolve_data;
            this.solve_data = solve_data;
        }
    }
    public void CheckForYouWin()
    {
        if (GetSquareEmpty() == 0)
        {
            you_win.SetActive(true);
            GameEvents.OnYouWinMethod();
        }
    }
    public int GetSquareEmpty()
    {
        square_empty = 0;
        foreach (var item in data.unsolve_data)
        {
            if (item == 0)
                square_empty++;
        }
        return square_empty;
    }
   public SudukuBoardData GetDataGameMode(string level)
    {
        switch (level)
        {
            case "Easy_4x4":
                SetData(Dropdown.Instance.grid_mode, 7);
                break;
            case "Medium_4x4":
                SetData(Dropdown.Instance.grid_mode, 8);
                break;
            case "Hard_4x4":
                SetData(Dropdown.Instance.grid_mode, 9);
                break;
            case "Very_Hard_4x4":
                SetData(Dropdown.Instance.grid_mode, 10);
                break;

            case "Easy_6x6":
                SetData(Dropdown.Instance.grid_mode, 15);
                break;
            case "Medium_6x6":
                SetData(Dropdown.Instance.grid_mode, 18);
                break;
            case "Hard_6x6":
                SetData(Dropdown.Instance.grid_mode, 20);
                break;
            case "Very_Hard_6x6":
                SetData(Dropdown.Instance.grid_mode, 24);
                break;

           case "Easy_9x9":
                SetData(Dropdown.Instance.grid_mode, 40);
                break;
            case "Medium_9x9":
                SetData(Dropdown.Instance.grid_mode, 50);
                break;
            case "Hard_9x9":
                SetData(Dropdown.Instance.grid_mode, 55);
                break;
            case "Very_Hard_9x9":
                SetData(Dropdown.Instance.grid_mode, 64);
                break;
        }
        return data;
    }
    public SudukuBoardData SetData(int n, int k)
    {
        int[] data_solve = new int[n * n];
        int[] data_unsolve = new int[n * n];
        data_solve = SudukuGenerator.GetDataSolve(n);
        data_unsolve = SudukuGenerator.GetDataUnsolve(data_solve, k);
        data.solve_data = data_solve;
        data.unsolve_data = data_unsolve;
        //foreach (var item in data_solve)
        //{
        //    Debug.Log("------------------------" + item);
        //}
        return data;
    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } 
        else
            Destroy(this);
    }
}
