using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaiLyChallenges : MonoBehaviour
{
    public Text txt_month;
    public Text txt_day;
    // Start is called before the first frame update
    void Start()
    {
        txt_month.text = "Thg "+DateTime.Now.Month.ToString();
        txt_day.text = DateTime.Now.Day.ToString();
    }

}
