using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    public Text time_text;
    public Text game_mode;
    public void DisplayTime()
    {
        time_text.text = Clock.Instance.GetCurrentTimeText();
        game_mode.text = GameSetting.Instance.GetGameMode().ToString();
    }
}
