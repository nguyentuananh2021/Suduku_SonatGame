using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class GridSquare : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{
    public GameObject number_text;
    public List<GameObject> number_notes;
    private bool note_active;
    
    private int number_ = 0;
    
    private int correct_number;
    private bool has_default_value;

    private bool selected_ = false;
    private int square_index_ = -1;

    private bool has_wrong_value = false;
    //private bool has_default_value = false;
    public bool IsSelected() { return selected_; }

     void Awake()
    {
        note_active = false;
        selected_ = false;
        SetNoteNumberValue(0);
    }
    
    public List<string> GetSquareNotes()
    {
        var notes = new List<string>();
        foreach (var number in number_notes)
        {
            notes.Add(number.GetComponent<Text>().text);
        }
        return notes;
    }

    private void SetClearEmptyNotes()
    {
        foreach (var number in number_notes)
        {
            if (number.GetComponent<Text>().text == "0")
                number.GetComponent<Text>().text = "";
        }
    }
    private void SetNoteNumberValue(int value)
    {
        foreach (var number in number_notes)
        {
            if (value <= 0)
            {
                number.GetComponent<Text>().text = "";
            }
            else number.GetComponent<Text>().text = value.ToString();
        }
    }

    private void SetNoteSingleNumberValue(int value, bool foce_update = false)
    {
        if (note_active == false && foce_update == false) return;
        if(value <= 0) { number_notes[value - 1].GetComponent<Text>().text = ""; }
        else
        {
            SetNumber(0);
            if (number_notes[value - 1].GetComponent<Text>().text == "" || foce_update)
                number_notes[value - 1].GetComponent<Text>().text = value.ToString();
            else
                number_notes[value - 1].GetComponent<Text>().text = "";
        }
    }

    public void SetGridNotes(List<int> notes)
    {
        foreach (var note in notes)
        {
            SetNoteSingleNumberValue(note, true);
        }
    }

    public void OnNotesActive(bool active)
    {
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
    public void DisplayText()
    {
        if (number_ <= 0)
        {
            number_text.GetComponent<Text>().text = default;
        }
        else
            number_text.GetComponent<Text>().text = number_.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        selected_ = true;
        GameEvents.SquareSelectedMethod(square_index_);
      
    }

    public void OnSubmit(BaseEventData eventData)
    {

    }

    private void OnEnable()
    {
        GameEvents.OnUpdateSquareNumber += OnSetNumber;
        GameEvents.OnSquareSelected += OnSquareSelected;
        GameEvents.OnNotesActive += OnNotesActive;
        GameEvents.OnDeleteNumber += OnDeleteNumber;
    }
    private void OnDisable()
    {
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

    public void OnSetNumber(int number)
    {
        if (selected_ && has_default_value == false)
        {
            if (note_active == true && has_wrong_value == false)
            {
                SetNoteSingleNumberValue(number);
            }
            else if(note_active == false)
            {
                SetNoteNumberValue(0);
                SetNumber(number);
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

    public void SetSquareColor(Color co)
    {
        var colors = this.colors;
        colors.normalColor = co;
        this.colors = colors;
    }

   
}
