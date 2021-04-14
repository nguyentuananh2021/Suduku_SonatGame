using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    public Text textClock;
    public Text game_mode;
    public void DisplayTime()
    {
        textClock.text = Clock.Instance.GetCurrentTimeText();
        game_mode.text = GameSetting.Instance.GetGameMode().ToString();
    }
}
