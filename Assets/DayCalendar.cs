using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DayCalendar : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{
    private int day_num_previous = -1;
    public int day_num;
    public Color dayColor;
    public Color dayTextColor;
    public bool selected_ = false;
    /// <summary>
    /// Constructor of Day
    /// </summary>
    public DayCalendar(int dayNum, Color dayColor, Color dayTextColor)
    {
        this.day_num = dayNum;
        UpdateColor(dayColor, dayTextColor);
        UpdateDay(dayNum);
    }

    protected DayCalendar()
    {

    }

    /// <summary>
    /// Call this when updating the color so that both the dayColor is updated, as well as the visual color on the screen
    /// </summary>
    public void UpdateColor(Color newColor, Color newTextColor)
    {
        this.GetComponent<Image>().color = newColor;
        this.GetComponentInChildren<Text>().color = newTextColor;
        dayColor = newColor;
        dayTextColor = newTextColor;
    }

    /// <summary>
    /// When updating the day we decide whether we should show the dayNum based on the color of the day
    /// This means the color should always be updated before the day is updated
    /// </summary>
    public void UpdateDay(int newDayNum)
    {
        this.day_num = newDayNum;
        if (dayColor == Color.white || dayColor == Color.green)
        {
            this.GetComponentInChildren<Text>().text = (day_num + 1).ToString();
        }
        else
        {
            this.GetComponentInChildren<Text>().text = "";
        }
    }

    private void Awake()
    {
        selected_ = false;
    }
    private void Start()
    {
    }

    public void DisplayText()
    {
    
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        selected_ = true;
        GameEvents.DaySelectedMethod(day_num);
     
    }

    private void SetColorSelected(Color color_selected)
    {
        
    }
    public void OnSubmit(BaseEventData eventData) { }
    private void OnEnable()
    {

        GameEvents.OnDaySelected += OnDaySelected;

    }

    private void OnDisable()
    {
        
        GameEvents.OnDaySelected -= OnDaySelected;

    }

    public void OnDaySelected(int square_index)
    {
        if (day_num != square_index)
        {
            selected_ = false;
        }
    }

}
