using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SudukuGrid : MonoBehaviour
{
    public static SudukuGrid Instance;
    public int columns = 0;
    public int rows = 0;
    public float square_offset = .0f;
    public GameObject grid_square;
    public Vector2 start_position;
    public float square_scale = 1.0f;
    public float square_gap = 1f;
    public List<GameObject> grid_squares_ = new List<GameObject>();

    public Color line_color = Color.red; public Color cell_color = Color.red; public Color cells_data_color = Color.red;

    private void Awake()
    {
        if (Instance) Destroy(this);
        Instance = this;

    }
    public void SetGridMode(int n)
    {
        columns = n;
        rows = n;

    }
    void Start()
    {
        start_position = new Vector2(0.0f, 0.0f);
        //Debug.Log(SudukuData.Instance.suduku_game.Count);
        if (grid_square.GetComponent<GridSquare>() == null)
        {
            Debug.Log("GridSquare is null");
        }
        if (Dropdown.Instance.grid_mode >= 0)
        {
            SetGridMode(Dropdown.Instance.grid_mode);
            CreateGrid();
            SetGridNumber(GameSetting.Instance.GetGameMode());
            SetSquaresColor(LineIndicator.Instance.GetCellDataSolve(SudukuData.Instance.unsolve_data_base), cells_data_color);
        }
        else Debug.Log("Grid Mode Null");
    }
    private void CreateGrid()
    {
        SpawnGridSquares();
        SetSquaresPosition();
    }
    private void SpawnGridSquares()
    {   
        if(grid_squares_ != null)
        {
            grid_squares_.Clear();
        }
        int square_index_ = 0;
        switch (columns)
        {
            case 9: square_scale = 1 ; break;
            case 6: square_scale = 1.525f; start_position.x += 37; start_position.y -=35 ; break;
            case 4: square_scale = 2.27f; start_position.x += 98; start_position.y -= 98; break;
        }
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                grid_squares_.Add(Instantiate(grid_square, this.transform));
                grid_squares_[grid_squares_.Count - 1].GetComponent<GridSquare>().SetSquareIndex(square_index_);
                grid_squares_[grid_squares_.Count - 1].transform.localScale = new Vector2(square_scale, square_scale);
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
            Vector2 pos = new Vector2(3,3);
            switch (columns)
            {
                case 9: pos = new Vector2(3,3); break;
                case 6: pos = new Vector2(3,2); square_gap =7; break;
                case 4: pos = new Vector2(2, 2); square_gap = 15; break;
            }
            if (column_number + 1 > columns)
            {
                row_number++;
                column_number = 0;

                square_gap_number.x = 0;
                row_moved = false;
            }
            var pos_x_offset = offset.x * column_number + (square_gap_number.x * square_gap);
            var pos_y_offset = offset.y * row_number + (square_gap_number.y * square_gap);

            if (column_number > 0 && column_number %pos.x == 0)
            {
                square_gap_number.x++;
                pos_x_offset += square_gap;
            }
            if(row_number > 0 && row_number %pos.y == 0 && row_moved == false)
            {
                row_moved = true;
                square_gap_number.y++;
                pos_y_offset += square_gap;
            }

            square.GetComponent<RectTransform>().anchoredPosition = new Vector3(start_position.x + pos_x_offset, start_position.y - pos_y_offset);
            column_number ++;
        }
    }

    private void SetGridNumber(string level)
    {
        if (level is null)
        {
            throw new System.ArgumentNullException(nameof(level));
        }
        SudukuData.SudukuBoardData data = SudukuData.Instance.GetDataGameMode(level);
        SetGridSquareData(data);
    }
    private void SetGridSquareData(SudukuData.SudukuBoardData data)
    {
        for (int index = 0; index < grid_squares_.Count; index++)
        {
            grid_squares_[index].GetComponent<GridSquare>().SetNumber(data.unsolved_data[index]);
            grid_squares_[index].GetComponent<GridSquare>().SetCorectNumber(data.solved_data[index]);
            grid_squares_[index].GetComponent<GridSquare>().SetHasDefaultValue(data.unsolved_data[index] !=0 && data.unsolved_data[index] == data.solved_data[index]);
            if (grid_squares_[index].GetComponent<GridSquare>().has_default_value)
            {
                grid_squares_[index].transform.GetChild(2).gameObject.GetComponent<TMP_Text>().color = Color.black;
            }
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
    public void SetSquaresColor(int[] data, Color color)
    {
        //Debug.Log(data.Length);
        foreach (var index in data)
        {
            var component = grid_squares_[index].GetComponent<GridSquare>();
            
            if(component.HasWrongValue() == false)
            {
                component.SetSquareColor(color);
            }
        }
    }
    
    public void SetCellNotesColor(List<int> list_cell)
    {
        IEnumerator ExecuteAfterTime(float time)
        {
            yield return new WaitForSeconds(time);
            foreach (var index in list_cell)
            {
                var component = grid_squares_[index].GetComponent<GridSquare>();
                component.SetSquareColor(line_color);
            }
            // Code to execute after the delay
        }
        foreach (var index in list_cell)
        {
            var component = grid_squares_[index].GetComponent<GridSquare>();
            component.SetSquareColor(cell_color);
        }
        StartCoroutine(ExecuteAfterTime(1));
           

    }
    public void OnSquareSelected(int square_index)
    {
        var cells_data_unsolve = LineIndicator.Instance.GetCellDataSolve(SudukuData.Instance.unsolve_data_base);
        var horizontal_line = LineIndicator.Instance.GetHorizontalLine(square_index);
        var vertical_line = LineIndicator.Instance.GetVerticalLine(square_index);
        var square = LineIndicator.Instance.GetSquare(square_index);
        var same_number = LineIndicator.Instance.GetAllSameNumber(square_index, SudukuData.Instance.data.unsolved_data);
        var cell_selected = LineIndicator.Instance.GetCellSelected(square_index);

        SetSquaresColor(LineIndicator.Instance.GetAllSquaresIndexs(), Color.white);
        SetSquaresColor(cells_data_unsolve, cells_data_color);
        SetSquaresColor(horizontal_line, line_color);
        SetSquaresColor(vertical_line, line_color);
        SetSquaresColor(square, line_color);
        SetSquaresColor(cell_selected, Color.white);
        if (same_number != null)
            SetSquaresColor(same_number, cell_color);
        
    }
}
