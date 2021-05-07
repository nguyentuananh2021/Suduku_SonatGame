using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Calendar : MonoBehaviour
{
    
    int startDay;
    int endDay;
    /// <summary>
    /// Cell or slot in the calendar. All the information each day should now about itself
    /// </summary>
    public Text text_current_date;
    public List<Button> Directions;
    public int previous_selected = -1;
    public Color color_selected = Color.red;
    public Color color_day = Color.red;
    public Color color_text = Color.red;
    public Color color_Disable = Color.red;
    public GameObject grid;

    public List<GameObject> Days = new List<GameObject>();

    public Text MonthAndYear;

    public DateTime currDate = DateTime.Now;
    public static Calendar Instance;
    private void Awake()
    {
        if (Instance) Destroy(this);
        Instance = this;
    }

    /// <summary>
    /// In start we set the Calendar to the current date
    /// </summary>
    private void Start()
    {
        UpdateListDay();
        UpdateCalendar(DateTime.Now.Year, DateTime.Now.Month);

    }
    public void OnEnable()
    {
        GameEvents.OnDaySelected += OnDaySelected;

    }
    public void OnDisable()
    {
        GameEvents.OnDaySelected -= OnDaySelected;
    }
    public void OnDaySelected(int day_num)
    {
        if(day_num >= 0 && day_num < endDay)
        {
            if (previous_selected < 0)
            {
                Days[day_num + startDay].GetComponent<DayCalendar>().UpdateColor(color_selected, Color.white);
            }
            else
            {
                Days[previous_selected + startDay].GetComponent<DayCalendar>().UpdateColor(Color.white, color_text);
                Days[day_num + startDay].GetComponent<DayCalendar>().UpdateColor(color_selected, Color.white);
            }
            previous_selected = day_num;
        }
        if (currDate.Year == DateTime.Now.Year && currDate.Month == DateTime.Now.Month)
        {
            Days[(DateTime.Now.Day - 1) + startDay].GetComponent<DayCalendar>().UpdateColor(color_day, Color.white);
        }
        //Days[day_num].GetComponent<Image>().color = Color.green;
    }
    private void UpdateListDay()
    {
        foreach (var obj in grid.GetComponentsInChildren<DayCalendar>())
        {
            Days.Add(obj.gameObject);
        }
    }
    /// <summary>
    /// Anytime the Calendar is changed we call this to make sure we have the right days for the right month/year
    /// </summary>
    void UpdateCalendar(int year, int month)
    {

        DateTime temp = new DateTime(year, month, 1);
        currDate = temp;
        MonthAndYear.text = temp.Year + "." + temp.Month;
        startDay = GetMonthStartDay(year, month);
        
        endDay = GetTotalNumberOfDays(year, month);

        Debug.Log(startDay + "----"+ endDay);
   
        for (int i = 0; i < 42; i++)
        {
            Days[i].GetComponent<DayCalendar>().interactable = true;
            if (i < startDay || i - startDay >= endDay)
            {
                Days[i].GetComponent<DayCalendar>().UpdateColor(color_Disable, color_Disable);
                Days[i].GetComponent<DayCalendar>().interactable = false;
            }
            else
            {
                Days[i].GetComponent<DayCalendar>().UpdateColor(Color.white, color_text);
            }
            //Days[i].GetComponent<DayCalendar>().isToDay = false;
            Days[i].GetComponent<DayCalendar>().UpdateDay(i - startDay);
        }
        ///This just checks if today is on our calendar. If so, we highlight it in green
        if (DateTime.Now.Year == year && DateTime.Now.Month == month)
        {
            Days[(DateTime.Now.Day - 1) + startDay].GetComponent<DayCalendar>().UpdateColor(color_day, Color.white);
        }
        Days[(DateTime.Now.Day - 1) + startDay].GetComponent<DayCalendar>().selected_ = true;
        text_current_date.text = DateTime.Now.Year + "." + DateTime.Now.Month + "." + DateTime.Now.Day;
    }

    /// <summary>
    /// This returns which day of the week the month is starting on
    /// </summary>
    int GetMonthStartDay(int year, int month)
    {
        DateTime temp = new DateTime(year, month, 1);

        //DayOfWeek Sunday == 0, Saturday == 6 etc.
        return (int)temp.DayOfWeek;
    }

    /// <summary>
    /// Gets the number of days in the given month.
    /// </summary>
    int GetTotalNumberOfDays(int year, int month)
    {
        return DateTime.DaysInMonth(year, month);
    }

    /// <summary>
    /// This either adds or subtracts one month from our currDate.
    /// The arrows will use this function to switch to past or future months
    /// </summary>
    public void SwitchMonth(int direction)
    {
        DateTime startYear = new DateTime(2021,1,1);
        DateTime andYear = DateTime.Now;
        andYear.CompareTo(startYear);
        
        if (direction < 0)
        {
            currDate = currDate.AddMonths(-1);
            Directions[1].interactable = true;
            if (currDate == startYear)
            {
                Directions[0].interactable = false;
                //currDate = currDate.AddMonths(1);
            }
    
                
        }
        else
        {
            currDate = currDate.AddMonths(1);
            Directions[0].interactable = true;
            if (currDate.Year == andYear.Year && currDate.Month == andYear.Month)
            {
                Directions[1].interactable = false;
                
            }
        }

        UpdateCalendar(currDate.Year, currDate.Month);
        //Debug.Log(currDate.Month);
    }


    public void PlayDaily(string game_mode)
    {

    }
}