using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class GridSquare : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{
    public GameObject number_text;
    private int number_ = 0;
    
    private int correct_number;
    private bool has_default_value;

    private bool selected_ = false;
    private int square_index_ = -1;

    private Color[] colors_ = new Color[3];

    private void Start()
    {
        colors_[1] = Color.cyan;
        colors_[0] = Color.white;
        selected_ = false;
    }
    public void SetCorectNumber(int number)
    {
        correct_number = number;
    }
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
    }
    private void OnDisable()
    {
        GameEvents.OnUpdateSquareNumber -= OnSetNumber;
        GameEvents.OnSquareSelected -= OnSquareSelected;
    }


    public void OnSetNumber(int number)
    {
        if (selected_ && has_default_value == false)
        {
            SetNumber(number);
            if (correct_number != number_)
            {
                var colors = this.colors;
                colors.normalColor = Color.red;
                this.colors = colors;

                GameEvents.OnWrongNumberMethod();
            }
            else
            {
                //has_default_value = true;
                var colors = this.colors;
                colors.normalColor = Color.white;
                this.colors = colors;
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
}
