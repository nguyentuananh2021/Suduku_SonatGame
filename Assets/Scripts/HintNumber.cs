
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintNumber : MonoBehaviour
{
    public GameObject tutorial_popup;
    public Text hint_text;
    int[] arr_index_squares;
    public List<GameObject> fields;
    public GameObject BoxSuduku;
    public int squareIndex;
    public Color higlight_area;
    public List<Text> place_holders;
    public Image suduku_bg;
    public struct Note
    {
        public int value;
        public List<int> list_index;

        public Note(int value, List<int> list_index)
        {
            this.value = value;
            this.list_index = list_index;
        }
    }
    public List<Note> ListCrossHatching = new List<Note>();
    public List<Note> ListCellOnly = new List<Note>();
    public int number_hint;
    public static HintNumber Instance;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(this);
    }
    public void Start()
    {
        SetField();
        SetListNoteValue(DropdownGridMode.Instance.GetGridMode(), ListCrossHatching);
        SetListNoteValue(DropdownGridMode.Instance.GetGridMode(), ListCellOnly);
        if (PlayerPrefs.GetString("json_data") != "")
        {
            number_hint = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("json_data")).hint_;
        }
        else
            number_hint = 9;
        hint_text.text = number_hint.ToString();
    }
    public int GetNumberHint()
    {
        return number_hint;
    }
    private void SetField()
    {

        fields[0].SetActive(true);
        fields[1].SetActive(false);
        fields[2].SetActive(false);
        tutorial_popup.SetActive(false);
    }
    public int GetSquareIndexInListNote()
    {
        int i = 0;
        
        foreach (var item in ListCrossHatching)
        {
            if (item.list_index.Count == 1)
            {
                i = 1;
                return item.list_index[0];
            }
        }
        if (i == 0) return 0;
        return 0;
    }

    public int square_index_box;
    public int square_value_box;
    private int GetSquareValueInListNote()
    {
        int i = 0;
        foreach (var item in ListCrossHatching)
        {
            if (item.list_index.Count == 1)
            {
                i = 1;
                return item.value;
            }
        }
        if (i == 0) return 0;
        return 0;

    }

    private void SetIndexValue(int square_index, List<Note> ListNote)
    {
        int value = 0;
        
        foreach (var item in ListNote)
        {
            if(item.list_index.Count == 1 && item.list_index[0] == square_index)
            {
                value = item.value;

            }
        }
        if(value == 0)
        {
            square_index_box = GetSquareIndexInListNote();
            square_value_box = GetSquareValueInListNote();
            
        }
        else
        {
            square_index_box = square_index;
            square_value_box = value;
        }
    }
    public string tutorial_mode = "";
    private void InitializedIndexValue(int square_index)
    {
        SetListNoteValue(DropdownGridMode.Instance.GetGridMode(), ListCrossHatching);
        SetListNoteValue(DropdownGridMode.Instance.GetGridMode(), ListCellOnly);
        Check_LastDigit(square_index);
        Check_CrossHatchingBox(square_index);
        int check = 0;
        foreach (var item in ListCellOnly)
        {
            //Debug.Log("value =" + item.value + "----" + "index =" + item.list_index.Count);
            if (item.list_index.Count > 0) 
                check++;
        }

        if (check == 1)
        {
            SetIndexValue(square_index, ListCellOnly);
            tutorial_mode = "Last Digit";
        }
        else
        {
            SetIndexValue(square_index, ListCrossHatching);
            tutorial_mode = "Cross-Hatching(Box)";
        }
    }
    public void OnClickHint(int square_index)
    {

        squareIndex = square_index;
        InitializedIndexValue(square_index);
        if (square_index_box == 0)
        {
            if (number_hint > 0)
            {
                number_hint--;
                hint_text.text = number_hint.ToString();

            }
            else
                Debug.Log("number hint = 0");
        }
        else
        {
            Clock.Instance.OnPauseGame(true);
            ObserveThisNumber();
        }
    }

    private void LastDigit_ObserveThisNumber(bool isBack = false)
    {
        SetAllSquareColor(Color.white);
        SetSquareColor(Color.gray);
        SudukuGrid.Instance.grid_squares_[squareIndex].GetComponentsInChildren<Image>(true)[0].color = Color.white;
        SudukuGrid.Instance.grid_squares_[squareIndex].GetComponentsInChildren<Image>(true)[1].color = Color.white;
        if (isBack == false)
        {
            var box = Instantiate(BoxSuduku, SudukuGrid.Instance.gameObject.transform);
            box.GetComponent<Transform>().transform.position = SudukuGrid.Instance.grid_squares_[squareIndex].GetComponent<GridSquare>().transform.position;
            box.GetComponent<Image>().rectTransform.sizeDelta = SizeDelta(squareIndex, true);
        }
    }
    private void LastDigit_HiglightArea()
    {
        var line_hor = LineIndicator.Instance.GetHorizontalLine(squareIndex);
        var line_ver = LineIndicator.Instance.GetVerticalLine(squareIndex);
        var line_squa = LineIndicator.Instance.GetSquare(squareIndex);
        StartCoroutine(SetLineLight(line_squa, 0.05f, true));
        StartCoroutine(SetLineLight(line_hor, 0.05f, true));
        StartCoroutine(SetLineLight(line_ver, 0.05f, true));
    }
    private void LastDigit_SetNumber()
    {
        var square = SudukuGrid.Instance.grid_squares_[square_index_box].GetComponent<GridSquare>();
        square.GetComponentsInChildren<Image>(true)[0].color = Color.white;

        square.GetComponentsInChildren<Image>()[1].GetComponentInChildren<Text>(true).text = "";
        square.GetComponentsInChildren<Image>()[1].GetComponentInChildren<Text>(true).gameObject.SetActive(true);

        square.GetComponentsInChildren<Image>()[1].GetComponentInChildren<Text>(true).text = square_value_box.ToString();
        square.GetComponentsInChildren<Image>()[1].GetComponentInChildren<Text>(true).fontSize = 200;

        square.GetComponentsInChildren<Image>(true)[1].color = Color.green;
    }
    private void LastDigit_Apply()
    {
        Destroy(GameObject.Find("Box(Clone)"));
        SetField();
        BackHiglightArea();
        SetSquareColor(Color.white);
        foreach (var item in SudukuGrid.Instance.grid_squares_)
        {
            item.GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(false);
        }
        
        number_hint--;
        hint_text.text = number_hint.ToString();
        tutorial_popup.SetActive(false);
        SudukuGrid.Instance.grid_squares_[square_index_box].GetComponent<GridSquare>().OnSetNumber(square_value_box);
        SudukuGrid.Instance.grid_squares_[square_index_box].GetComponent<GridSquare>().SetNumberData(square_value_box, square_index_box); 
        square_index_box = 0;
        square_value_box = 0;
    }

    private void CrossHatchingBox_ObserveThisNumber()
    {

        SetAllSquareColor(Color.white);
        SetSquareColor(Color.gray);

        foreach (var index in GetCellSameNumber_CrossHatchingBox())
        {
            SudukuGrid.Instance.grid_squares_[index].GetComponentsInChildren<Image>(true)[0].color = Color.white;
        }
        SudukuGrid.Instance.SetSquaresColor(GetCellSameNumber_CrossHatchingBox().ToArray(), Color.green);
        
    }
    private void CrossHatchingBox_HiglightArea()
    {
        
        foreach (var sq_index in arr_index_squares)
        {
            if (SudukuData.Instance.data.unsolved_data[sq_index] == 0)
            {
                var line_hor = LineIndicator.Instance.GetHorizontalLine(sq_index);
                var line_ver = LineIndicator.Instance.GetVerticalLine(sq_index);
                for (int i = 0; i < arr_index_squares.Length; i++)
                {
                    if (SudukuData.Instance.data.unsolved_data[line_hor[i]] == square_value_box)
                    {
                        StartCoroutine(SetLineLight(line_hor, 0.05f));
                    }
                    if (SudukuData.Instance.data.unsolved_data[line_ver[i]] == square_value_box)
                    {
                        StartCoroutine(SetLineLight(line_ver, 0.05f));
                    }
                }
            }

        }
        var pos = PositionCenter(arr_index_squares[0], arr_index_squares[arr_index_squares.Length - 1]);
        var box = Instantiate(BoxSuduku, SudukuGrid.Instance.gameObject.transform);
        //SetLineDefaul(arr_index_squares);
        box.GetComponent<Transform>().transform.position = pos;
        box.GetComponent<Image>().rectTransform.sizeDelta = SizeDelta(arr_index_squares[0]);
        StartCoroutine(SetLineLight(arr_index_squares, .0f));
    }

    private void CrossHatchingBox_SetNumber()
    {
        IEnumerator set_number(float time)
        {
            var square = SudukuGrid.Instance.grid_squares_[square_index_box].GetComponent<GridSquare>();
            
            //SetBoxColor(SudukuData.Instance.data.unsolved_data, Color.gray);
            //SetBoxColor(arr_index_squares,Color.white);
            
            yield return new WaitForSeconds(time);
            square.GetComponentsInChildren<Image>(true)[0].color = Color.white;

            square.GetComponentsInChildren<Image>()[1].GetComponentInChildren<Text>(true).text = "";
            square.GetComponentsInChildren<Image>()[1].GetComponentInChildren<Text>(true).gameObject.SetActive(true);

            yield return new WaitForSeconds(time);
            square.GetComponentsInChildren<Image>()[1].GetComponentInChildren<Text>(true).text = square_value_box.ToString();
            square.GetComponentsInChildren<Image>()[1].GetComponentInChildren<Text>(true).fontSize = 200;

            square.GetComponentsInChildren<Image>(true)[1].color = Color.green;
            fields[2].GetComponentInChildren<Button>(true).gameObject.SetActive(true);
        }


        fields[2].GetComponentInChildren<Button>().gameObject.SetActive(false);
        StartCoroutine(set_number(0.1f));
    } 
    private void CrossHatchingBox_Apply()
    {
        Destroy(GameObject.Find("Box(Clone)"));
        SetField();
        BackHiglightArea();
        SetSquareColor(Color.white);
        SudukuGrid.Instance.grid_squares_[square_index_box].GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(false);
        number_hint--;
        hint_text.text = number_hint.ToString();
        tutorial_popup.SetActive(false);
        SudukuGrid.Instance.grid_squares_[square_index_box].GetComponent<GridSquare>().SetNumber(square_value_box);
        SudukuGrid.Instance.grid_squares_[square_index_box].GetComponent<GridSquare>().OnSetNumber(square_value_box);
        SudukuGrid.Instance.grid_squares_[square_index_box].GetComponent<GridSquare>().SetNumberData(square_value_box, square_index_box);
        square_index_box = 0;
        square_value_box = 0;
    }



    //step 1 //ObserveThisNumber
    public void ObserveThisNumber()
    {
        tutorial_popup.gameObject.SetActive(true);
        suduku_bg.color = Color.gray;
        switch (tutorial_mode)
        {
            case "Last Digit":
                LastDigit_ObserveThisNumber();
                place_holders[0].text = "Last Digit";
                place_holders[1].text = "Observe <color=#0099FF>this cell</color>";

                break;
            case "Cross-Hatching(Box)":
                CrossHatchingBox_ObserveThisNumber();
                place_holders[0].text = "Cross-Hatching(Box)";
                place_holders[1].text = "Observe <color=#0099FF>number " + "\"" + square_value_box + "\"</color>";
                break;
        }
        
    }
    //step 2 HiglightArea
    public void HiglightArea()
    {
        switch (tutorial_mode)
        {
            case "Last Digit":
                LastDigit_HiglightArea();
                place_holders[2].text = "In Suduku, the numbers 1-9 cannot appear twice in the same row, column, or box.\nAs shown in the figuer, the numbers that appear in the <color=#0099FF>highlight area</color> cannot appear in this cell";
                break;
            case "Cross-Hatching(Box)":
                CrossHatchingBox_HiglightArea();
                place_holders[2].text = "In Suduku, the numbers 1-9 cannot appear twice in the same row, column, or box.\nAs shown, observe <color=#0099FF>number \"" + square_value_box+ "\" </color>and its area;\n<color=#0099FF>number " + "\""+square_value_box+ "\"</color> cannot appear in the <color=#0099FF>highlight area</color>";
                break;
        }
    }
    // step 3 SetNumber
    public void SetNumber()
    {
        switch (tutorial_mode)
        {
            case "Last Digit":
                LastDigit_SetNumber();
                place_holders[3].text = "The row, column, and box of this cell contain all numbers except <color=#0099FF>number \"" + square_value_box+ "\"</color>,\nSo this cell can only be filled with <color=#0099FF>number \"" + square_value_box+"\"</color>";
                break;
            case "Cross-Hatching(Box)":
                CrossHatchingBox_SetNumber();
                place_holders[3].text = "As shown, observe this cell and its area; This cell is the only one in <color=#0099FF>this box</color> that can contain <color=#0099FF>number \"" + square_value_box+ "\"</color>;\nSo this cell should be filled with <color=#0099FF>number \"" + square_value_box+"\"</color>.";
                break;
        }
    }
    //step 4 ApplySetNumber
    public void ApplySetNumber()
    {
        switch (tutorial_mode)
        {
            case "Last Digit":
                LastDigit_Apply();
                break;
            case "Cross-Hatching(Box)":
                CrossHatchingBox_Apply();
                break;
        }
        suduku_bg.color = Color.white;
        Clock.Instance.OnPauseGame(false);
    }

    //Back step 2 HiglightArea
    public void BackHiglightArea()
    {
        switch (tutorial_mode)
        {
            case "Last Digit":
                Back_LastDigit_HiglightArea();
                break;
            case "Cross-Hatching(Box)":
                Back_CrossHatchingBox_HiglightArea();
                break;
        }
    }

    // Back step 3 SetNumber
    public void BackSetNumber()
    {
        switch (tutorial_mode)
        {
            case "Last Digit":
                Back_LastDigit_SetNumber();
                break;
            case "Cross-Hatching(Box)":
                Back_CrossHatchingBox_SetNumber();
                break;
        }
    }
 


    private List<int> GetCellSameNumber_CrossHatchingBox()
    {
        List<int> cells = new List<int>();
        foreach (var index in arr_index_squares)
        {
            if (SudukuData.Instance.data.unsolved_data[index] == 0)
            {
                var arr_hor = LineIndicator.Instance.GetHorizontalLine(index);
                var arr_ver = LineIndicator.Instance.GetVerticalLine(index);
                for (int i = 0; i < arr_hor.Length; i++)
                {
                    if (SudukuData.Instance.data.unsolved_data[arr_hor[i]] == square_value_box)
                    {
                        cells.Add(arr_hor[i]);
                    }
                    if (SudukuData.Instance.data.unsolved_data[arr_ver[i]] == square_value_box)
                    {
                        cells.Add(arr_ver[i]);
                    }
                }
            }
        }
        return cells;
    }
    private void SetAllSquareColor(Color color)
    {
        foreach (var square_ in SudukuGrid.Instance.grid_squares_)
        {
            var component = square_.GetComponent<GridSquare>();
            if (component.HasWrongValue() == false)
            {
                component.SetSquareColor(color);
            }
        }

    }
   


    
    private Vector3 PositionCenter(int square_index_start, int square_index_end)
    {
        var square_start = SudukuGrid.Instance.grid_squares_[square_index_start].GetComponent<GridSquare>().transform.position;
        var square_end = SudukuGrid.Instance.grid_squares_[square_index_end].GetComponent<GridSquare>().transform.position;
        Vector3 pos_center = new Vector3();


        pos_center.x = (square_start.x + square_end.x) / 2;
        pos_center.y = (square_start.y + square_end.y) / 2;
        pos_center.z = (square_start.z + square_end.z) / 2;
       
        return pos_center;
    }
    private Vector2 SizeDelta(int square_index_start, bool cell = false)
    {

        //var square_start = SudukuGrid.Instance.grid_squares_[square_index_start].transform.position;
        //var square_end = SudukuGrid.Instance.grid_squares_[square_index_end].transform.position;
        var rec = SudukuGrid.Instance.grid_squares_[square_index_start].GetComponent<RectTransform>().rect;
        var square_scale = SudukuGrid.Instance.grid_squares_[square_index_start].transform.localScale.x;
        var off_set = rec.width * square_scale;
       
        Vector2 size_box = new Vector2();
        var grid = DropdownGridMode.Instance.GetGridMode();

        if (cell)
        {
            size_box.x = off_set + 10;
            size_box.y = off_set + 10;
        }
        else
        {
            switch (grid)
            {
                case 4:
                    size_box.x = 2 * off_set + 30;
                    size_box.y = 2 * off_set + 30;
                    break;
                case 6:
                    size_box.x = 3 * off_set + 30;
                    size_box.y = 2 * off_set + 30;
                    break;
                case 9:
                    size_box.x = 3 * off_set + 30;
                    size_box.y = 3 * off_set + 30;
                    break;

            }
        }
        
        return size_box;
    }

    private void Back_LastDigit_SetNumber()
    {
        LastDigit_ObserveThisNumber(true);
        LastDigit_HiglightArea();
        SudukuGrid.Instance.grid_squares_[squareIndex].GetComponent<GridSquare>().GetComponentsInChildren<Image>()[1].GetComponentInChildren<Text>(true).gameObject.SetActive(false);
    }

    private void Back_LastDigit_HiglightArea()
    {
        LastDigit_ObserveThisNumber(true);
    }
    private void Back_CrossHatchingBox_SetNumber()
    {
        
        var square = SudukuGrid.Instance.grid_squares_[square_index_box].GetComponent<GridSquare>();
        square.GetComponentsInChildren<Image>()[1].GetComponentInChildren<Text>(true).gameObject.SetActive(false);
        square.GetComponentsInChildren<Image>(true)[1].color = Color.white;
        square.GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(false);
        Back_CrossHatchingBox_HiglightArea();
        CrossHatchingBox_HiglightArea();
    }
    private void Back_CrossHatchingBox_HiglightArea()
    {
        Destroy(GameObject.Find("Box(Clone)"));
        foreach (var sq_index in arr_index_squares)
        {
            var line_hor = LineIndicator.Instance.GetHorizontalLine(sq_index);
            var line_ver = LineIndicator.Instance.GetVerticalLine(sq_index);
            for (int i = 0; i < line_hor.Length; i++)
            {
                if (SudukuData.Instance.data.unsolved_data[line_hor[i]] == square_value_box)
                {
                    SetLineDefaul(line_hor);

                }
                if (SudukuData.Instance.data.unsolved_data[line_ver[i]] == square_value_box)
                {
                    SetLineDefaul(line_ver);
                }

            }
        }
        SetSquareColor(Color.white);
        ObserveThisNumber();
    }
    private void SetSquareColor(Color color)
    {
        foreach (var square in SudukuGrid.Instance.grid_squares_)
        {
            square.GetComponentInChildren<Image>().color = color;
            square.GetComponentsInChildren<Image>(true)[1].color = color;
        }
    }


    private void SetLineDefaul(int[] line)
    {
        foreach (var item in line)
        {
            if (SudukuData.Instance.data.unsolved_data[item] == 0)
            {
                //Debug.Log(SudukuGrid.Instance.grid_squares_[item].GetComponentsInChildren<Image>(true)[1].name);
                SudukuGrid.Instance.grid_squares_[item].GetComponentsInChildren<Image>(true)[1].GetComponentInChildren<Text>(true).gameObject.SetActive(false);
                SudukuGrid.Instance.grid_squares_[item].GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(false);

            }
        }
    }
    IEnumerator SetLineLight(int[] line, float time, bool is_lastDigit = false)
    {
        foreach (var item in line)
        {
            yield return new WaitForSeconds(time);
            if (item != square_index_box)
            {
                SudukuGrid.Instance.grid_squares_[item].GetComponentInChildren<Image>().color = higlight_area;
                SudukuGrid.Instance.grid_squares_[item].GetComponentsInChildren<Image>(true)[1].color = higlight_area;
            }
            else
            {
                SudukuGrid.Instance.grid_squares_[item].GetComponentInChildren<Image>().color = Color.white;
                SudukuGrid.Instance.grid_squares_[item].GetComponentsInChildren<Image>(true)[1].color = Color.white;
            }
            if (SudukuData.Instance.data.unsolved_data[item] == square_value_box)
            {
                SudukuGrid.Instance.grid_squares_[item].GetComponentInChildren<Image>().color = Color.green;
            }

            if (SudukuData.Instance.data.unsolved_data[item] == 0)
            {

                //Debug.Log(SudukuGrid.Instance.grid_squares_[item].GetComponentsInChildren<Image>(true)[1].name);
                SudukuGrid.Instance.grid_squares_[item].GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(true);
                if (item != square_index_box && is_lastDigit == false)
                {
                    SudukuGrid.Instance.grid_squares_[item].GetComponentsInChildren<Image>(true)[1].GetComponentInChildren<Text>(true).gameObject.SetActive(true);
                }
            }

        }


    }

    public void SetListNoteValue(int n, List<Note> list)
    {
        list.Clear();
        for (int i = 1; i <= n; i++)
        {
            list.Add(new Note(i, new List<int> { }));
        }
    }
   
    public void Check_CrossHatchingBox(int square_index_)
    {
        var data = SudukuData.Instance.data;
        arr_index_squares = LineIndicator.Instance.GetSquare(square_index_);
        for (int i = 0; i < arr_index_squares.Length; i++)
        {
            if (data.unsolved_data[arr_index_squares[i]] == 0)
            {
                var arr_index_ver = LineIndicator.Instance.GetVerticalLine(arr_index_squares[i]);
                var arr_index_hor = LineIndicator.Instance.GetHorizontalLine(arr_index_squares[i]);
                for (int value = 1; value <= arr_index_squares.Length; value++)
                {
                    if (IsOnlyBoxAndLine(value, arr_index_squares, arr_index_hor, arr_index_ver, data.unsolved_data))
                    {
                        ListCrossHatching.Find(x => x.value == value).list_index.Add(arr_index_squares[i]);
                    }

                }
            }
        }
    }
    
    private bool IsOnlyBoxAndLine(int value_, int[] arr_squ, int[] arr_hor, int[] arr_ver, int[] data_unsovlved)
    {
        for (int i = 0; i < arr_squ.Length; i++)
        {
            if (data_unsovlved[arr_squ[i]] == value_ || data_unsovlved[arr_hor[i]] == value_ || data_unsovlved[arr_ver[i]] == value_)
            {
                return false;
            }
        }
        return true;
    }
//---------------------------------------------------
//---------------------------------------------------
    public void Check_LastDigit(int square_index_)
    {
        var arr_index_hor = LineIndicator.Instance.GetHorizontalLine(square_index_);
        var arr_index_ver = LineIndicator.Instance.GetVerticalLine(square_index_);
        var arr_index_squ = LineIndicator.Instance.GetSquare(square_index_);
        var data = SudukuData.Instance.data;
        for (int value = 1; value <= arr_index_squ.Length; value++)
        {
            if (IsOnlyBoxAndLine(value, arr_index_squ, arr_index_hor, arr_index_ver, data.unsolved_data))
            {
                ListCellOnly.Find(x => x.value == value).list_index.Add(square_index_);
                //Debug.Log(value);
            }
        }
    }
}