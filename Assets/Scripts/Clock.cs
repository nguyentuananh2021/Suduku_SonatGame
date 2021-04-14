using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    int hour_;
    int minute_;
    int seconds_;

    private Text textClock;
    private float delta_time;
    private bool stop_clock_ = false;
    private bool pause_clock_ = false;
    public static Clock Instance;

    string time_data;
    private bool is_continue = false;
    private void Awake()
    {
        if (Instance)
        {
            Destroy(Instance);
        }
        Instance = this;
        textClock = GetComponent<Text>();
        delta_time = 0;
    }

    void Start()
    {
        stop_clock_ = false;
        is_continue = false;
        var json_data_pref = PlayerPrefs.GetString("json_data");
        if (json_data_pref != "")
        {
            is_continue = true;
            time_data = JsonUtility.FromJson<Data>(json_data_pref).times_;
            hour_ = int.Parse(time_data.Split(':')[0]);
            minute_ = int.Parse(time_data.Split(':')[1]);
            seconds_ = int.Parse(time_data.Split(':')[2]);
        }
        //Debug.Log(hour_ + ":" + minute_ + ":" + seconds_);
        delta_time = (hour_ * 3600 + minute_ * 60 + seconds_);
    }

    void Update()
    {
        if(stop_clock_ == true)
        {
            Destroy(this);
            //Debug.Log("Destroy Timer");
        }
        if (pause_clock_ == false)
        {
            delta_time += Time.deltaTime;
           
            TimeSpan span = TimeSpan.FromSeconds(delta_time);


            string hour = LeadingZero(span.Hours);
            string minute = LeadingZero(span.Minutes);
            string seconds = LeadingZero(span.Seconds);

            textClock.text = hour + ":" + minute + ":" + seconds;
        }
     
    }

    public string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }
    public void OnGameOver()
    {
        stop_clock_ = true;
    }
    public void OnYouWin()
    {
        stop_clock_ = true;
    }
    private void OnEnable()
    {
        GameEvents.OnYouWin += OnYouWin;
        GameEvents.OnGameOver += OnGameOver;
    }
    private void OnDisable()
    {
        GameEvents.OnYouWin -= OnYouWin;
        GameEvents.OnGameOver -= OnGameOver;
    }

    public string GetCurrentTimeText()
    {
        return textClock.text;
    }
    public void OnPauseGame(bool Bool)
    {
        pause_clock_ = Bool;
    }

}
