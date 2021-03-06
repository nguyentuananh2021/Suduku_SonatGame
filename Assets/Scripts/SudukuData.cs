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
        public int[] unsolved_data;
        public int[] solved_data;
        public int[] unsolved_data_base;
        
        public SudukuBoardData(int[] unsolve_data, int[] solve_data, int[] unsolve_data_base) : this()
        {
            unsolved_data = unsolve_data;
            unsolved_data_base = unsolve_data_base;
            solved_data = solve_data;
        }
    }
    void Awake()
    {
        if (Instance == null)
        {
            //DontDestroyOnLoad(this);
            Instance = this;
        }
        else
            Destroy(this);
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
        foreach (var item in data.unsolved_data)
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
            //4x4
            case "Easy 4x4":
                SetData(DropdownGridMode.Instance.GetGridMode(), 7);
                break;
            case "Medium 4x4":
                SetData(DropdownGridMode.Instance.GetGridMode(), 8);
                break;
            case "Hard 4x4":
                SetData(DropdownGridMode.Instance.GetGridMode(), 9);
                break;
            case "Very Hard 4x4":
                SetData(DropdownGridMode.Instance.GetGridMode(), 10);
                break;
            //6x6
            case "Easy 6x6":
                SetData(DropdownGridMode.Instance.GetGridMode(), 15);
                break;
            case "Medium 6x6":
                SetData(DropdownGridMode.Instance.GetGridMode(), 18);
                break;
            case "Hard 6x6":
                SetData(DropdownGridMode.Instance.GetGridMode(), 20);
                break;
            case "Very Hard 6x6":
                SetData(DropdownGridMode.Instance.GetGridMode(), 24);
                break;
            //9x9
            case "Easy 9x9":
                SetData(DropdownGridMode.Instance.GetGridMode(), 40);
                break;
            case "Medium 9x9":
                SetData(DropdownGridMode.Instance.GetGridMode(), 50);
                break;
            case "Hard 9x9":
                SetData(DropdownGridMode.Instance.GetGridMode(), 55);
                break;
            case "Very Hard 9x9":
                SetData(DropdownGridMode.Instance.GetGridMode(), 64);
                break;
        }
        CopyUnsolveData();
        return data;
    }
    public SudukuBoardData SetData(int n, int k)
    {
        int[] data_solve = new int[n * n];
        int[] data_unsolve = new int[n * n];
        data_solve = SudukuGenerator.GetDataSolve(n);
        data_unsolve = SudukuGenerator.GetDataUnsolve(data_solve, k);
        data.solved_data = data_solve;
        data.unsolved_data = data_unsolve;
        //foreach (var item in data_solve)
        //{
        //    Debug.Log("------------------------" + item);
        //}
        return data;
    }
   
    private void CopyUnsolveData()
    {
        data.unsolved_data_base = new int[data.unsolved_data.Length];
        for (int i = 0; i < data.unsolved_data.Length; i++)
        {
            data.unsolved_data_base[i] = data.unsolved_data[i];
        }
    }
}
