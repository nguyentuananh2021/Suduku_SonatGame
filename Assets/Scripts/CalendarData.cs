using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarData : MonoBehaviour
{
    public class Data_Calendar
    {
        public Data suduku_data;
        public int status = 0;
        //public 
    }
    public List<Data_Calendar> days_ = new List<Data_Calendar>();
    void Start()
    {
        
    }
    public void SetDataCalendar()
    {
        if(PlayerPrefs.GetString("CalendarData") == "")
        {
            Data_Calendar data = new Data_Calendar();
            //data.suduku_data.
        }
        else
        {

        }
    }
    public void GetDataCalendar()
    {

    }
}
