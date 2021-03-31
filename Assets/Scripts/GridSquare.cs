using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class GridSquare : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{
    public GameObject number_text;
    public List<GameObject> number_notes;
    public GameObject grid_number_note;
    public bool note_active;
    
    private int number_ = 0;
    
    private int correct_number;
    public bool has_default_value;

    private bool selected_ = false;
    private int square_index_ = -1;

    private bool has_wrong_value = false;
    public bool IsSelected() { return selected_; }

    private void Awake()
    {
        note_active = false;
        selected_ = false;
        SetNoteNumberValue(0);
    }
    private void Start()
    {
        DeActivateNumberNode();
        ActivateNumberNote(Dropdown.Instance.grid_mode);
        Set_grid_note();
    }
    private void ActivateNumberNote(int grid_mode)
    {
        for (int i = 0; i < grid_mode; i++)
        {
            number_notes[i].SetActive(true);
        }
    }
    private void DeActivateNumberNode()
    {
        foreach (var item in number_notes)
        {
            item.SetActive(false);
        }
    }
    public void Set_grid_note()
    {
        if(Dropdown.Instance.grid_mode == 4)
        {
            grid_number_note.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedRowCount;
            grid_number_note.GetComponent<GridLayoutGroup>().cellSize = new Vector2(70, 70);
        }
    }
    public List<string> GetSquareNotes()
    {
        var notes = new List<string>();
        foreach (var number in number_notes)
        {
            
            notes.Add(number.GetComponent<TMP_Text>().text);
        }
        return notes;
    }
    //private void SetClearEmptyNotes()
    //{
    //    foreach (var number in number_notes)
    //    {
    //        if (number.GetComponent<TMP_Text>().text == "0")
    //            number.GetComponent<TMP_Text>().text = "";
    //    }
    //}
    private void SetNoteNumberValue(int value)
    {
        foreach (var number in number_notes)
        {
            if (value <= 0)
            {
                number.GetComponent<TMP_Text>().text = "";
            }
            else 
            {
                number.GetComponent<TMP_Text>().text = value.ToString();
            }
        }
    }

    private void SetNoteSingleNumberValue(int value, bool foce_update = false)
    {
        if (note_active == false && foce_update == false) return;
        if(value <= 0) { number_notes[value - 1].GetComponent<TMP_Text>().text = ""; }
        else
        {
            SetNumber(0);
            SetNumberData(0, square_index_);
            if (number_notes[value - 1].GetComponent<TMP_Text>().text == "" || foce_update)
                number_notes[value - 1].GetComponent<TMP_Text>().text = value.ToString();
            else
                number_notes[value - 1].GetComponent<TMP_Text>().text = "";
        }
    }
    //public void SetGridNotes(List<int> notes)
    //{
    //    foreach (var note in notes)
    //    {
    //        SetNoteSingleNumberValue(note, true);
    //    }
    //}
    public void OnNotesActive(bool active)
    {
        //SetSquareColor(Color.white);
        note_active = active;
    }
    public void SetCorectNumber(int number)
    {
        correct_number = number;
        has_wrong_value = false;
    }
    public bool HasWrongValue() { return has_wrong_value; }
    public void SetHasDefaultValue(bool has_default) 
    {
        has_default_value = has_default; 
    }
    public bool GethasDefaultValue() { return has_default_value; }

    public void SetSquareIndex(int index)
    {
        square_index_ = index;
    }
    public void SetNumber(int number)
    {
        number_ = number;
        
        DisplayText();
    }
    public void SetNumberData(int num, int square_index)
    {
        SudukuData.Instance.data.unsolve_data[square_index] = num;
        SudukuGrid.Instance.OnSquareSelected(square_index);
        if(SudukuData.Instance.GetSquareEmpty() == 0)
        {
            //Debug.Log("YOU WIN");
            SudukuData.Instance.CheckForYouWin();
        }
    }
    public void DisplayText()
    {
        if (number_ <= 0)
        {
            number_text.GetComponent<TMP_Text>().text = default;
        }
        else
            number_text.GetComponent<TMP_Text>().text = number_.ToString();
        if(has_default_value)
            number_text.GetComponent<TMP_Text>().color = Color.black;

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        selected_ = true;

        GameEvents.SquareSelectedMethod(square_index_);
    }
    public void OnSubmit(BaseEventData eventData){}
    private void OnEnable()
    {
        GameEvents.OnUpdateSquareNumber += OnSetNumber;
        GameEvents.OnSquareSelected += OnSquareSelected;
        GameEvents.OnNotesActive += OnNotesActive;
        GameEvents.OnDeleteNumber += OnDeleteNumber;
    }
    private void OnDisable()
    {
        //selected_ = false;
        GameEvents.OnUpdateSquareNumber -= OnSetNumber;
        GameEvents.OnSquareSelected -= OnSquareSelected;
        GameEvents.OnNotesActive -= OnNotesActive;
        GameEvents.OnDeleteNumber -= OnDeleteNumber;
    }
    public void OnDeleteNumber()
    {
        if(selected_ && !has_default_value)
        {
            number_ = 0;
            has_wrong_value = false;
            SetSquareColor(Color.white);
            SetNoteNumberValue(0);
            DisplayText();
        }
    }

    public void DeleteNumberNotes(int square_index)
    {
        List<int> list_index = LineIndicator.Instance.GetAllCellNoteIsActivate(square_index);
        //Debug.Log(number_);
        foreach (var index in list_index)
        {
            var square_ = SudukuGrid.Instance.grid_squares_[index].GetComponent<GridSquare>();
            //Debug.Log("index"+index);
            TMP_Text[] notes = square_.GetComponentInChildren<GridLayoutGroup>().GetComponentsInChildren<TMP_Text>();
            foreach (var item in notes)
            {
                if (item.text == number_.ToString() && number_ == correct_number)
                {
                    item.text = "";
                }
            }

        }
    }

    public void OnSetNumber(int number)
    {
        if (selected_ && has_default_value == false)
        {
            if (note_active == true && has_wrong_value == false )
            {
                List<int> list_cell = LineIndicator.Instance.GetCellNotes(number, square_index_, SudukuData.Instance.data.unsolve_data);

                if (list_cell.Count > 0)
                {
                    SudukuGrid.Instance.SetCellNotesColor(list_cell);
                }
                else 
                {
                    SetNoteSingleNumberValue(number);
                    
                }  
            }
            else if(note_active == false)
            {
                SetNoteNumberValue(0);
                SetNumber(number);
                DeleteNumberNotes(square_index_);

                if (correct_number != number_)
                {
                    has_wrong_value = true;
                    var colors = this.colors;
                    colors.normalColor = Color.red;
                    this.colors = colors;
                    GameEvents.OnWrongNumberMethod();
                }
                else
                {
                    has_wrong_value = false;
                    var colors = this.colors;
                    colors.normalColor = Color.white;
                    this.colors = colors;
                    SetNumberData(number, square_index_);
                }
            }
        }
    }
    public void OnSquareSelected( int square_index)
    {
        if(square_index_ != square_index)
        {
            selected_ = false;
        }
    }
    public void SetSquareColor(Color color)
    {
        var colors = this.colors;
        colors.normalColor = color;
        this.colors = colors;
    }
}
