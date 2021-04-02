using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueGame : MonoBehaviour
{
    public TMPro.TMP_Text text_;
    private void Start()
    {
        SetDisplay();
    }
    public string GetLevelAndTime()
    {
        if(PlayerPrefs.GetInt("continue")!= 0)
            return "Easy 4x4 00:00:23";
        return "";
    }
    public void SetDisplay()
    {
        var str = GetLevelAndTime();
        Debug.Log(str);
        if (str != "")
        {
            text_.text = str;
            gameObject.SetActive(true);
        }
        else gameObject.SetActive(false);
    }
}
