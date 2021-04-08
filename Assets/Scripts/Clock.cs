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
        if (PlayerPrefs.GetString("json_data") != "")
        {
            var time = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("json_data")).times_;
            hour_ = int.Parse(time.Split(':')[0]);
            minute_ = int.Parse(time.Split(':')[1]);
            seconds_ = int.Parse(time.Split(':')[2]);
        }
       
    }

    // Update is called once per frame
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
                string hour = LeadingZero(span.Hours + hour_);
                string minute = LeadingZero(span.Minutes + minute_);
                string seconds = LeadingZero(span.Seconds + seconds_);
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
    public static string GetCurrentTime()
    {
        return Instance.delta_time.ToString();
    }
}
