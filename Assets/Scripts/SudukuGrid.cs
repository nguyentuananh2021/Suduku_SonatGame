﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudukuGrid : MonoBehaviour
{
    public int columns = 0;
    public int rows = 0;
    public float square_offset = .0f;
    public GameObject grid_square;
    public Vector2 start_position = new Vector2(0.0f, 0.0f);
    public float square_scale = 1.0f;

    private List<GameObject> grid_squares_ = new List<GameObject>();

    public int selectData;

    // Start is called before the first frame update
    void Start()
    {
        if(grid_square.GetComponent<GridSquare>() == null)
        {
            Debug.Log("add gameObject?");
        }
        else
        {
            CreateGrid();
            SetGridNumber();
        }
    }
    /*
   .
   .
   . CreateGrid
   .
   .
   .
    */
    private void CreateGrid()
    {
        SpawnGridSquares();
        SetSquaresPosition();
    }

    private void SpawnGridSquares()
    {   //0 1 2 3 4 5 6 7 8 9...81
        int square_index_ = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                grid_squares_.Add(Instantiate(grid_square, this.transform));
                grid_squares_[grid_squares_.Count - 1].GetComponent<GridSquare>().SetSquareIndex(square_index_);
                //grid_squares_[grid_squares_.Count - 1].transform.parent = this.transform;
                grid_squares_[grid_squares_.Count - 1].transform.localScale = new Vector3(square_scale, square_scale, square_scale);
                square_index_++;
            }
        }

    }
    private void SetSquaresPosition()
    {
        var square_rect = grid_squares_[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2();
        offset.x = square_rect.rect.width * square_rect.transform.localScale.x + square_offset;
        offset.y = square_rect.rect.height * square_rect.transform.localScale.y + square_offset;

        int column_number = 0;
        int row_number = 0;

        foreach (GameObject square in grid_squares_)
        {
            if(column_number +1 > columns)
            {
                row_number++;
                column_number = 0;
            }
            var pos_x_offset = offset.x * column_number;
            var pos_y_offset = offset.y * row_number;
            square.GetComponent<RectTransform>().anchoredPosition = new Vector3(start_position.x + pos_x_offset, start_position.y - pos_y_offset);
            column_number ++;
        }
    }
    /*
    .
    .
    . SetGridNumber
    .
    .
    .
     */
    private void SetGridNumber()
    {
        
        List<SudukuData.SudukuBoardData> data = new List<SudukuData.SudukuBoardData>();
        data = SudukuData.GetData();

        //foreach (var square in grid_squares_)
        //{
        //    square.GetComponent<GridSquare>().SetNumber(Random.Range(0, 9));
        //}
        selectData = Random.Range(0, data.Count);
        SetGridSquareData(selectData,data);
    }
    private void SetGridSquareData(int selectData,List<SudukuData.SudukuBoardData> data)
    {
        for (int i = 0; i < grid_squares_.Count; i++)
        {
            grid_squares_[i].GetComponent<GridSquare>().SetNumber(data[selectData].unsolve_data[i]);
            grid_squares_[i].GetComponent<GridSquare>().SetCorectNumber(data[selectData].solve_data[i]);
            grid_squares_[i].GetComponent<GridSquare>().SetHasDefaultValue(data[selectData].unsolve_data[i] !=0 && data[selectData].unsolve_data[i] == data[selectData].solve_data[i]);
        }
    }
}
