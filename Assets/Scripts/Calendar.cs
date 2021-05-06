using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class Day
{

    public int dayNum;
    public Color dayColor;
    public Color textColor;
    public GameObject obj;
    public bool isToDay = false;
    public int status = 0;
    public bool isSelected = false;
    /// <summary>
    /// Constructor of Day
    /// </summary>
    public Day(int dayNum, Color dayColor, Color textColor, GameObject obj)
    {
        this.dayNum = dayNum;
        this.obj = obj;
        UpdateColor(dayColor, textColor);
        UpdateDay(dayNum);
    }

    /// <summary>
    /// Call this when updating the color so that both the dayColor is updated, as well as the visual color on the screen
    /// </summary>
    public void UpdateColor(Color newColor, Color colortext)
    {

        obj.GetComponent<Image>().color = newColor;
        obj.GetComponentInChildren<Text>().color = colortext;
        dayColor = newColor;
    }

    /// <summary>
    /// When updating the day we decide whether we should show the dayNum based on the color of the day
    /// This means the color should always be updated before the day is updated
    /// </summary>
    public void UpdateDay(int newDayNum)
    {
        this.dayNum = newDayNum;
        if (dayColor == Color.white || dayColor == Color.green)
        {
            obj.GetComponentInChildren<Text>().text = (dayNum + 1).ToString();
            //if(dayNum+1 > newDayNum)
            //{
            //    obj.GetComponent<Button>().interactable = false;
            //}
        }
        else
        {
            obj.GetComponentInChildren<Text>().text = "";
        }
    }
}
public class Calendar : MonoBehaviour
{
    /// <summary>
    /// Cell or slot in the calendar. All the information each day should now about itself
    /// </summary>
    public List<Button> Directions;
   
    public Color color_blue = Color.red;
    public Color color_grey = Color.red;
    
    /// <summary>
    /// All the days in the month. After we make our first calendar we store these days in this list so we do not have to recreate them every time.
    /// </summary>
    private List<Day> days = new List<Day>();

    /// <summary>
    /// Setup in editor since there will always be six weeks. 
    /// Try to figure out why it must be six weeks even though at most there are only 31 days in a month
    /// </summary>
    public Transform[] weeks;

    /// <summary>
    /// This is the text object that displays the current month and year
    /// </summary>
    public Text MonthAndYear;

    /// <summary>
    /// this currDate is the date our Calendar is currently on. The year and month are based on the calendar, 
    /// while the day itself is almost always just 1
    /// If you have some option to select a day in the calendar, you would want the change this objects day value to the last selected day
    /// </summary>
    public DateTime currDate = DateTime.Now;

    /// <summary>
    /// In start we set the Calendar to the current date
    /// </summary>
    private void Start()
    {
        UpdateCalendar(DateTime.Now.Year, DateTime.Now.Month);
        //foreach (var day in days)
        //{
        //    Debug.Log("is today: "+ day.isToDay+"----"+" square color:"+day.dayColor + "---" +"day num:"+ day.dayNum +"---"+ "day obj name:"+day.obj.name + "---"+ "day text color:"+day.textColor.ToString());
        //}
    }

    /// <summary>
    /// Anytime the Calendar is changed we call this to make sure we have the right days for the right month/year
    /// </summary>
    void UpdateCalendar(int year, int month)
    {
        DateTime temp = new DateTime(year, month, 1);
        currDate = temp;
        MonthAndYear.text = temp.Year + "." + temp.Month;
        int startDay = GetMonthStartDay(year, month);
        int endDay = GetTotalNumberOfDays(year, month);


        ///Create the days
        ///This only happens for our first Update Calendar when we have no Day objects therefore we must create them

        if (days.Count == 0)
        {
            for (int w = 0; w < 6; w++)
            {
                for (int i = 0; i < 7; i++)
                {
                    Day newDay;
                    int currDay = (w * 7) + i;
                    if (currDay < startDay || currDay - startDay >= endDay)
                    {
                        newDay = new Day(currDay - startDay, color_grey,color_grey, weeks[w].GetChild(i).gameObject);
                        newDay.obj.GetComponent<Button>().interactable = false;
                    }
                    else
                    {
                        newDay = new Day(currDay - startDay, Color.white,color_blue, weeks[w].GetChild(i).gameObject);
                        if(newDay.dayNum >= DateTime.Now.Day)
                        {
                            newDay.obj.GetComponent<Button>().interactable = false;
                        }
                    }
                    
                    days.Add(newDay);
                }
            }
        }
        ///loop through days
        ///Since we already have the days objects, we can just update them rather than creating new ones
        else
        {
            for (int i = 0; i < 42; i++)
            {
                if (i < startDay || i - startDay >= endDay)
                {
                    days[i].UpdateColor(color_grey, color_grey);
                    days[i].obj.GetComponent<Button>().interactable = false;
                }
                else
                {
                    days[i].UpdateColor(Color.white, color_blue);
                    
                    days[i].obj.GetComponent<Button>().interactable = true;
                    if (days[i].dayNum > DateTime.Now.Day && currDate.Year == DateTime.Now.Year && currDate.Month == DateTime.Now.Month)
                    {
                        days[i].obj.GetComponent<Button>().interactable = false;
                    }
                }
                days[i].isToDay = false;
                days[i].UpdateDay(i - startDay);
            }
           
        }

        ///This just checks if today is on our calendar. If so, we highlight it in green
        if (DateTime.Now.Year == year && DateTime.Now.Month == month)
        {
            days[(DateTime.Now.Day - 1) + startDay].UpdateColor(color_blue, Color.white);
            days[(DateTime.Now.Day - 1) + startDay].isToDay = true; days[(DateTime.Now.Day - 1) + startDay].obj.GetComponent<Button>().interactable = true;
        }

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
}