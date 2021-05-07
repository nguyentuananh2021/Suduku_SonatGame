using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

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
    public Color line_color = Color.red; 
    public Color same_number_color = Color.red;
    public void SetColorIndicator( Color line_color, Color same_number_color)
    {
        this.line_color = line_color;
        this.same_number_color = same_number_color;
    }
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
        SetPanelGrid();
        start_position = new Vector2(0.0f, 0.0f);
        //Debug.Log(SudukuData.Instance.suduku_game.Count);
        if (grid_square.GetComponent<GridSquare>() == null)
        {
            Debug.Log("GridSquare is null");
        }
        if (DropdownGridMode.Instance.GetGridMode() >= 0)
        {
            SetGridMode(DropdownGridMode.Instance.GetGridMode());
            CreateGrid();
            SetGridNumber(GameSetting.Instance.GetGameMode());
            // SetSquaresColor(LineIndicator.Instance.GetCellDataSolve(SudukuData.Instance.data.unsolved_data_base), cells_data_color);
            StartCoroutine(FadeInSquareNumber());
        }
        else Debug.Log("Grid Mode Null");
    }
    public RectTransform rect_panelCentrerGrid;
    private void SetPanelGrid()
    {
        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;
        //Debug.Log(width + "   " + height);
        if(height >= width * 2)
        {
            rect_panelCentrerGrid.transform.localScale = new Vector2(0.95f,0.95f);
        }
        else
        {
            rect_panelCentrerGrid.transform.localScale = new Vector2(1.02f, 1.02f);
        }
    }
    private void CreateGrid()
    {
        SpawnGridSquares();
        SetSquaresPosition();
    }
    private GameObject[,] ConvertData()
    {
        GameObject[,] arr_data = new GameObject[columns,rows];
        int k = 0;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                arr_data[i, j] = grid_squares_[k];
                k++;
            }
        }
        return arr_data;
    }
    IEnumerator FadeInSquareNumber()
    {
        var grid_square = ConvertData();
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < rows; i++)
        {
            yield return new WaitForSeconds(.03f);

            for (int j = 0; j < columns; j++)
            {
                grid_square[i,j].GetComponentsInChildren<Image>()[1].gameObject.SetActive(false);
                
                grid_square[i,j].GetComponentInChildren<Image>().color = line_color;
                if (i > 0)
                {
                    grid_square[i - 1, j].GetComponentInChildren<Image>().color = Color.white;
                }
                   
                if (i == rows - 1)
                {
                    grid_square[i, j].GetComponentInChildren<Image>().color = Color.white;
                }
            }
            
        }
    }

    private void SpawnGridSquares()
    {
        var rt = this.GetComponent<RectTransform>();
        var w_square = grid_square.GetComponent<RectTransform>().rect.width;
        var w = rt.rect.width;
        float x = 0;
        if (grid_squares_ != null)
        {
            grid_squares_.Clear();
        }
        int square_index_ = 0;
        float scale_ = (w - (square_gap * 4) - (square_offset * (columns - 1)) - (square_gap * columns / 3)) / (w_square * columns);
        switch (columns)
        {
            case 9: square_scale = scale_;
                x = w - (w_square * square_scale*columns + (square_offset*(columns-1)) + (square_gap*columns/3));
                start_position.x -= (w / 2) - (w_square * square_scale / 2) - x/2;
                start_position.y += (w / 2) - (w_square * square_scale / 2) - x/2; break;

            case 6: square_scale = scale_;
                x = w - (w_square * square_scale * columns + (square_offset * (columns - 1)) + (square_gap * columns / 3));
                float y = w - (w_square * square_scale * columns + (square_offset * (columns - 1)) + (square_gap * columns / 2));
                start_position.x -= (w / 2) - (w_square * square_scale / 2) - x / 2;
                start_position.y += (w / 2) - (w_square * square_scale / 2) - y / 2; break;

            case 4: square_scale = scale_;
                x = w - (w_square * square_scale * columns + (square_offset * (columns - 1)) + (square_gap * columns / 2));
                start_position.x -= (w / 2) - (w_square * square_scale / 2) - x / 2; ;
                start_position.y += (w / 2) - (w_square * square_scale / 2) - x / 2; ; break;

        }
        //Debug.Log("scale: " + square_scale + "   x = " + x);

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
                case 9: pos = new Vector2(3,3); square_gap = 5;  break;
                case 6: pos = new Vector2(3,2); square_gap =5; break;
                case 4: pos = new Vector2(2, 2); square_gap = 5; break;
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
        var json_data_pref = PlayerPrefs.GetString("json_data");
        // Debug.Log(data.solved_data[0]);
        if (json_data_pref != "")
        {
            var json_data = JsonUtility.FromJson<Data>(json_data_pref);
            SudukuData.Instance.data = new SudukuData.SudukuBoardData(json_data.unsolved_data, json_data.solved_data, json_data.unsolved_data_base);
            SetGridSquareData(SudukuData.Instance.data);
        }
        else
        {
            SetGridSquareData( SudukuData.Instance.GetDataGameMode(level));
        }
        
        
    }
    private void SetGridSquareData(SudukuData.SudukuBoardData data)
    {
        var json_data_pref = PlayerPrefs.GetString("json_data");
        for (int index = 0; index < grid_squares_.Count; index++)
        {
            grid_squares_[index].GetComponent<GridSquare>().SetNumber(data.unsolved_data[index]);
            grid_squares_[index].GetComponent<GridSquare>().SetCorectNumber(data.solved_data[index]);
            if(json_data_pref != "")
                grid_squares_[index].GetComponent<GridSquare>().SetNoteValues(index, JsonUtility.FromJson<Data>(json_data_pref).arr_notes);
            grid_squares_[index].GetComponent<GridSquare>().SetHasDefaultValue(data.unsolved_data_base[index] !=0 && data.unsolved_data_base[index] == data.solved_data[index]);
            SetSquareTextColorDefaul(index, Color.black);
        }
    }
    public void SetSquareTextColorDefaul(int index, Color color)
    {
        if(grid_squares_[index].GetComponent<GridSquare>().has_default_value)
            {
            grid_squares_[index].transform.GetChild(2).gameObject.GetComponent<Text>().color = color;
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
            component.SetSquareColor(same_number_color);
        }
        StartCoroutine(ExecuteAfterTime(1));
           

    }
    public void OnSquareSelected(int square_index)
    {
        var list_number_can_make_note = LineIndicator.Instance.GetNumberCanMakeNote(square_index);
        //var cells_data_unsolve = LineIndicator.Instance.GetCellDataSolve(SudukuData.Instance.data.unsolved_data_base);
        var horizontal_line = LineIndicator.Instance.GetHorizontalLine(square_index);
        var vertical_line = LineIndicator.Instance.GetVerticalLine(square_index);
        var square = LineIndicator.Instance.GetSquare(square_index);
        var same_number = LineIndicator.Instance.GetAllSameNumber(square_index, SudukuData.Instance.data.unsolved_data);
        var cell_selected = LineIndicator.Instance.GetCellSelected(square_index);

        LineNumber.Instance.SetActiveButton(list_number_can_make_note);
        SetSquaresColor(LineIndicator.Instance.GetAllSquaresIndexs(), Color.white);
        //SetSquaresColor(cells_data_unsolve, cells_data_color);
        SetSquaresColor(horizontal_line, line_color);
        SetSquaresColor(vertical_line, line_color);
        SetSquaresColor(square, line_color);
        SetSquaresColor(cell_selected, Color.white);
        if (same_number != null)
            SetSquaresColor(same_number, same_number_color);
        
    }
}
