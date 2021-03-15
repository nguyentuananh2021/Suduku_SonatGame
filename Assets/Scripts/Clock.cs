using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    int hour_ = 0;
    int minute_ = 0;
    int seconds_ = 0;

    private Text textClock;
    private float delta_time;
    private bool stop_clock_ = false;

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
    }

    // Update is called once per frame
    void Update()
    {
        if(stop_clock_ == false)
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
    private void OnEnable()
    {
        GameEvents.OnGameOver += OnGameOver;
    }
    private void OnDisable()
    {
        GameEvents.OnGameOver -= OnGameOver;
    }

    public Text GetCurrentTimeText()
    {
        return textClock;
    }
}
