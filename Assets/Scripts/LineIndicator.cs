using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineIndicator : MonoBehaviour
{
    public static LineIndicator Instance;
    private int n; 
    //4x4
    private int[,] square_data_4x4 = new int[4, 4]
    {
        { 0,  1,  4,  5},
        { 2,  3,  6,  7},
        { 8,  9, 12, 13},
        {10, 11, 14, 15}
    };
    //6x6
    private int[,] square_data_6x6 = new int[6, 6]
    {
        {0,  1,  2,  6,  7,  8 },
        {3,  4,  5,  9,  10, 11},
        {12, 13, 14, 18, 19, 20},
        {15, 16, 17, 21, 22, 23},
        {24, 25, 26, 30, 31, 32},
        {27, 28, 29, 33, 34, 35}
    };
    //9x9
    private int[,] square_data_9x9 = new int[9, 9]
    {
        {0, 1, 2,  9, 10,11, 18,19,20},
        {3, 4, 5,  12,13,14, 21,22,23},
        {6, 7, 8,  15,16,17, 24,25,26},
        {27,28,29, 36,37,38, 45,46,47},
        {30,31,32, 39,40,41, 48,49,50},
        {33,34,35, 42,43,44, 51,52,53},
        {54,55,56, 63,64,65, 72,73,74},
        {57,58,59, 66,67,68, 75,76,77},
        {60,61,62, 69,70,71, 78,79,80}
    };

    int[,] line_data;
    int[] line_data_falt;
    int[,] square_data;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
       
    }
    public void Start()
    {
        n = Dropdown.Instance.grid_mode;
        line_data = LineData(n);
        line_data_falt = LineDataFalt(n);
        switch (n)
        {
            case 4:
                square_data = square_data_4x4;
                break;
            case 6:
                square_data = square_data_6x6;
                break;
            case 9:
                square_data = square_data_9x9;
                break;

        }
    }
    // line data
    public int[,] LineData(int n)
    {
        var data = new int[n, n];
        int index = 0;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                data[i, j] = index;
                index++;
            }
        }
        return data;
    }
    // line data falt
    public int[] LineDataFalt(int n)
    {
        int[] data = new int[n*n];
        for (int i = 0; i < n*n; i++)
        {
            data[i] = i;
        }
        return data;
    }

    private (int, int) GetSquarePosition(int square_index)
    {
        int pos_row = -1;
        int pos_col = -1;
        for (int row = 0; row < n; row++)
        {
            for (int col = 0; col < n; col++)
            {
                if(line_data[row, col] == square_index)
                {
                    pos_row = row;
                    pos_col = col;
                }
            }
        }
        return (pos_row, pos_col);
    }

    public int[] GetHorizontalLine(int square_index)
    {
        int[] line = new int[n];
        var square_postion_row = GetSquarePosition(square_index).Item1;
        for (int i = 0; i < n; i++)
        {
            line[i] = line_data[square_postion_row, i];
        }
        return line;
    }
    public int[] GetVerticalLine(int square_index)
    {
        int[] line = new int[n];
        var square_position_col = GetSquarePosition(square_index).Item2;
        for (int i = 0; i < n; i++)
        {
            line[i] = line_data[i, square_position_col];
        }

        return line;
    }
    public int[] GetSquare(int square_index)
    {
        int[] line = new int[n];
        int pos_row = -1;
        for (int row = 0; row < n; row++)
        {
            for (int col = 0; col < n; col++)
            {
                if(square_data[row, col] == square_index)
                {
                    pos_row = row;
                }
            }
        }
        for (int i = 0; i < n; i++)
        {
            line[i] = square_data[pos_row, i];
        }
        return line;
    }
    public int[] GetAllSameNumber(int sqaure_index, int[] data)
    {
        int n = 0;
        int[] cell;
        if (data[sqaure_index] != default)
        {
            foreach (var item in data)
            {
                if (item == data[sqaure_index])
                {
                    n++;
                }
            }
            cell = new int[n];
            int value = data[sqaure_index];

            int j = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] > 0 && data[i] == value)
                {
                    cell[j] = i;
                   // Debug.Log("value = " + value + ".... i=" + i + "..... cell[" + j + "] = " + cell[j]);
                    j++;
                }
            }
            return cell;
        }
        return null;
    }
    public int[] GetCellSelected(int square_index)
    {
        int[] cell = new int[1];
        cell[0] = square_index;
        return cell;
    }

    public int[] GetCellDataSolve(int[] data)
    {
        int n = 0;
        foreach (var item in data)
        {
            if (item > 0) n++;
        }
        int[] cells = new int[n];
        int j = 0;
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i] > 0)
            {
                cells[j] = i;
                j++;
            }
        }
        return cells;
    }
    public int[] GetAllSquaresIndexs()
    {
        return line_data_falt;
    }
    //public int[] GetCellNotes(int value, int square_index, int[]unsolved_data)
    //{
    //    int[] row_arr = GetHorizontalLine(square_index);
    //    int[] col_arr = GetVerticalLine(square_index);
    //    int[] squ_arr = GetSquare(square_index);

    //    int[] Arr = new int[3];
    //    foreach (var item in row_arr)
    //    {

    //    }
    //}
}
