using System.Collections;
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

    public float square_gap = 1f;

    public List<GameObject> grid_squares_ = new List<GameObject>();

    private int seleced_grid_data = 0;

    public Color line_color = Color.red;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(SudukuData.Instance.suduku_game.Count);
        if(grid_square.GetComponent<GridSquare>() == null)
        {
            Debug.Log("GridSquare is null");
        }
        CreateGrid();
        SetGridNumber(GameSetting.Instance.GetGameMode());
    }
   /*
    CreateGrid
   */
    private void CreateGrid()
    {
        SpawnGridSquares();
        SetSquaresPosition();
    }

    private void SpawnGridSquares()
    {   //0 1 2 3 4 5 6 7 8 9...81
        if(grid_squares_ != null)
        {
            grid_squares_.Clear();
        }
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
        Vector2 square_gap_number = new Vector2(0.0f, 0.0f);
        bool row_moved = false;

        offset.x = square_rect.rect.width * square_rect.transform.localScale.x + square_offset;
        offset.y = square_rect.rect.height * square_rect.transform.localScale.y + square_offset;

        int column_number = 0;
        int row_number = 0;

        foreach (GameObject square in grid_squares_)
        {
            if (column_number + 1 > columns)
            {
                row_number++;
                column_number = 0;

                square_gap_number.x = 0;
                row_moved = false;
            }
            var pos_x_offset = offset.x * column_number + (square_gap_number.x * square_gap);
            var pos_y_offset = offset.y * row_number + (square_gap_number.y * square_gap);

            if (column_number > 0 && column_number % 3 == 0)
            {
                square_gap_number.x++;
                pos_x_offset += square_gap;
            }
            if(row_number > 0 && row_number % 3 == 0 && row_moved == false)
            {
                row_moved = true;
                square_gap_number.y++;
                pos_y_offset += square_gap;
            }

            square.GetComponent<RectTransform>().anchoredPosition = new Vector3(start_position.x + pos_x_offset, start_position.y - pos_y_offset);
            column_number ++;
        }
    }
    /*
     SetGridNumber
    */
    private void SetGridNumber(string level)
    {
        if (level is null)
        {
            throw new System.ArgumentNullException(nameof(level));
        }

        seleced_grid_data = Random.Range(0, SudukuData.Instance.suduku_game[level].Count);
        SudukuData.SudukuBoardData data = SudukuData.Instance.suduku_game[level][seleced_grid_data];
        SetGridSquareData(data);
    }
    private void SetGridSquareData(SudukuData.SudukuBoardData data)
    {
        for (int index = 0; index < grid_squares_.Count; index++)
        {
            grid_squares_[index].GetComponent<GridSquare>().SetNumber(data.unsolve_data[index]);
            grid_squares_[index].GetComponent<GridSquare>().SetCorectNumber(data.solve_data[index]);
            grid_squares_[index].GetComponent<GridSquare>().SetHasDefaultValue(data.unsolve_data[index] !=0 && data.unsolve_data[index] == data.solve_data[index]);
        }
        
    }
    public void OnEnable()
    {
        GameEvents.OnSquareSelected += OnSquareSelected;
    }
    public void OnDisable()
    {
        GameEvents.OnSquareSelected -= OnSquareSelected;
    }

    public void OnDestroy()
    {
        GameEvents.OnSquareSelected -= OnSquareSelected;
    }

    private void SetSquaresColor(int[] data, Color co)
    {
        //Debug.Log(data.Length);
        foreach (var index in data)
        {
            var component = grid_squares_[index].GetComponent<GridSquare>();
            
            if(component.HasWrongValue() == false && component.IsSelected() == false)
            {
                component.SetSquareColor(co);
            }
        }
    }
    public void OnSquareSelected(int square_index)
    {
        var horizontal_line = LineIndicator.Instance.GetHorizontalLine(square_index);
        var vertical_line = LineIndicator.Instance.GetVerticalLine(square_index);
        var square = LineIndicator.Instance.GetSquare(square_index);
        SetSquaresColor(LineIndicator.Instance.GetAllSquaresIndexs(), Color.white);

        SetSquaresColor(square, line_color);
        SetSquaresColor(horizontal_line, line_color);
        SetSquaresColor(vertical_line, line_color);
        
        
    }
}
