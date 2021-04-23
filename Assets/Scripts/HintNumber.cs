
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

    int square_index_box;
    int square_value_box;
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
        CheckLine(square_index);
        CheckBoxSquare(square_index);
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
            Debug.Log("Last Digit");
        }
        else
        {
            SetIndexValue(square_index, ListCrossHatching);
            tutorial_mode = "Cross-Hatching(Box)";
            Debug.Log("CrossHatching");
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
            ObserveThisNumber();
        }
    }

    private void LastDigit_ObserveThisNumber()
    {
        SetAllSquareColor(Color.white);
        SetSquareColor(Color.gray);
        SudukuGrid.Instance.grid_squares_[squareIndex].GetComponentsInChildren<Image>(true)[0].color = Color.white;
        var box = Instantiate(BoxSuduku, SudukuGrid.Instance.gameObject.transform);
        box.GetComponent<Transform>().transform.position = SudukuGrid.Instance.grid_squares_[squareIndex].GetComponent<GridSquare>().transform.position;
        box.GetComponent<Image>().rectTransform.sizeDelta = SizeDelta(squareIndex, true);
    }
    private void LastDigit_HiglinghtArea()
    {

    }
     private void LastDigit_SetNumber()
    {

    }
     private void LastDigit_Apply()
    {

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
    private void CrossHatchingBox_HiglinghtArea()
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
    }

    private void CrossHatchingBox_SetNumber()
    {
        IEnumerator set_number(float time)
        {
            var pos = PositionCenter(arr_index_squares[0], arr_index_squares[arr_index_squares.Length - 1]);
            var box = Instantiate(BoxSuduku, SudukuGrid.Instance.gameObject.transform);
            //SetLineDefaul(arr_index_squares);
            box.GetComponent<Transform>().transform.position = pos;
            box.GetComponent<Image>().rectTransform.sizeDelta = SizeDelta(arr_index_squares[0]);
            StartCoroutine(SetLineLight(arr_index_squares, 0));
            //SetBoxColor(SudukuData.Instance.data.unsolved_data, Color.gray);
            //SetBoxColor(arr_index_squares,Color.white);
            SudukuGrid.Instance.grid_squares_[square_index_box].GetComponentsInChildren<Image>(true)[0].color = Color.white;
            yield return new WaitForSeconds(time + 1);
            // Debug.Log(box.GetComponent<RectTransform>().position.z);


            SudukuGrid.Instance.grid_squares_[square_index_box].GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(true);
            SudukuGrid.Instance.grid_squares_[square_index_box].GetComponentsInChildren<Image>()[1].GetComponentInChildren<Text>(true).text = "";

            var square = SudukuGrid.Instance.grid_squares_[square_index_box].GetComponent<GridSquare>();
            square.GetComponentsInChildren<Image>()[1].GetComponentInChildren<Text>(true).gameObject.SetActive(true);

            yield return new WaitForSeconds(time);
            square.GetComponentsInChildren<Image>()[1].GetComponentInChildren<Text>(true).text = square_value_box.ToString();
            square.GetComponentsInChildren<Image>()[1].GetComponentInChildren<Text>(true).fontSize = 200;
            yield return new WaitForSeconds(time);
            square.GetComponentsInChildren<Image>()[1].GetComponentInChildren<Text>(true).fontSize = 150;
            yield return new WaitForSeconds(time);
            square.GetComponentsInChildren<Image>()[1].GetComponentInChildren<Text>(true).fontSize = 200;
            SudukuGrid.Instance.grid_squares_[square_index_box].GetComponentsInChildren<Image>(true)[1].color = Color.green;
            fields[2].GetComponentInChildren<Button>(true).gameObject.SetActive(true);
        }


        fields[2].GetComponentInChildren<Button>().gameObject.SetActive(false);
        StartCoroutine(set_number(0.1f));
    } 
    private void CrossHatchingBox_Apply()
    {
        Destroy(GameObject.Find("Box(Clone)"));
        SetField();
        BackHiglinghtArea();
        SetSquareColor(Color.white);
        SudukuGrid.Instance.grid_squares_[square_index_box].GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(false);
        number_hint--;
        hint_text.text = number_hint.ToString();
        tutorial_popup.SetActive(false);
        SudukuGrid.Instance.grid_squares_[square_index_box].GetComponent<GridSquare>().SetNumber(square_value_box);
        SudukuGrid.Instance.grid_squares_[square_index_box].GetComponent<GridSquare>().Select();
        SudukuGrid.Instance.grid_squares_[square_index_box].GetComponent<GridSquare>().OnSetNumber(square_value_box);
        SudukuGrid.Instance.grid_squares_[square_index_box].GetComponent<GridSquare>().SetNumberData(square_value_box, square_index_box);
        square_index_box = 0;
        square_value_box = 0;
    }



    //step 1 //ObserveThisNumber
    public void ObserveThisNumber()
    {
        tutorial_popup.gameObject.SetActive(true);
        switch (tutorial_mode)
        {
            case "Last Digit":
                LastDigit_ObserveThisNumber();
                break;
            case "Cross-Hatching(Box)":
                CrossHatchingBox_ObserveThisNumber();
                break;
   
        }
        
    }
    //step 2 HiglinghtArea
    public void HiglinghtArea()
    {
        switch (tutorial_mode)
        {
            case "Last Digit":
                LastDigit_HiglinghtArea();
                break;
            case "Cross-Hatching(Box)":
                CrossHatchingBox_HiglinghtArea();
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
                break;
            case "Cross-Hatching(Box)":
                CrossHatchingBox_SetNumber();
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
    }


    //Back step 2 HiglinghtArea
    public void BackHiglinghtArea()
    {
        switch (tutorial_mode)
        {
            case "Last Digit":
                
                break;
            case "Cross-Hatching(Box)":
                Back_CrossHatchingBox_HiglinghtArea();
                break;
        }
    }

    // Back step 3 SetNumber
    public void BackSetNumber()
    {
        switch (tutorial_mode)
        {
            case "Last Digit":
                
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

    
  
    public void Back_CrossHatchingBox_SetNumber()
    {
        //SetBoxColor(SudukuData.Instance.data.unsolved_data, Color.white);
        //SetBoxColor(SudukuData.Instance.data.unsolved_data, Color.white);
        Destroy(GameObject.Find("Box(Clone)"));
        var square = SudukuGrid.Instance.grid_squares_[square_index_box].GetComponent<GridSquare>();
        square.GetComponentsInChildren<Image>()[1].GetComponentInChildren<Text>(true).gameObject.SetActive(false);
        square.GetComponentsInChildren<Image>(true)[1].color = Color.white;
        square.GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(false);
        Back_CrossHatchingBox_HiglinghtArea();
        CrossHatchingBox_HiglinghtArea();
    }
    public void Back_CrossHatchingBox_HiglinghtArea()
    {
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
    IEnumerator SetLineLight(int[] line, float time)
    {
        foreach (var item in line)
        {
            yield return new WaitForSeconds(time);
            SudukuGrid.Instance.grid_squares_[item].GetComponentInChildren<Image>().color = Color.white;
            if (SudukuData.Instance.data.unsolved_data[item] == square_value_box)
            {
                SudukuGrid.Instance.grid_squares_[item].GetComponentInChildren<Image>().color = Color.green;
            }

            if (SudukuData.Instance.data.unsolved_data[item] == 0)
            {

                //Debug.Log(SudukuGrid.Instance.grid_squares_[item].GetComponentsInChildren<Image>(true)[1].name);
                SudukuGrid.Instance.grid_squares_[item].GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(true);
                if (item != square_index_box)
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
   
    public void CheckBoxSquare(int square_index_)
    {
        //for (int i = 0; i < arr_index_squares.Length; i++)
        //{
        //    if()
        //}

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
    public void CheckLine(int square_index_)
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