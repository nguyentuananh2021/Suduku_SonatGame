﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouWinPopup : MonoBehaviour
{
    public Text textClock;
    void Start()
    {
        textClock.text = Clock.Instance.GetCurrentTimeText().text;
    }
}
