﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    public Text time_text;
    public void DisplayTime()
    {
        time_text.text = Clock.Instance.GetCurrentTimeText().text;
    }
}