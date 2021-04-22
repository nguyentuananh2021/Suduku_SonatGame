using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuStatisticalPopup : MonoBehaviour
{
    public Text Title_;
    public List<Text> wrongs_;
    public List<Text> times_;
    public List<Button> btns_;
    int dropdown_value = -1;
    private void Start()
    {
        dropdown_value = PlayerPrefs.GetInt("grid_mode_data");
    }
    public void Display()
    {
        switch (dropdown_value)
        {
            case 0:
                Displaytext_4x4();
                btns_[0].Select();
                break;
            case 1:
                Displaytext_6x6();
                btns_[1].Select();
                break;
            case 2:
                Displaytext_9x9();
                btns_[2].Select();
                break;

        }
    }
    public void Displaytext_4x4()
    {
        Title_.text = "Best 4x4";
        for (int i = 0; i < 4; i++)
        { 
            wrongs_[i].text = PlayerPrefs.GetString("4x4_wrongs_" + i);
            times_[i].text = PlayerPrefs.GetString("4x4_times_" + i);
            if(wrongs_[i].text.Length == 0)
            {
                wrongs_[i].text = "---";
                times_[i].text = "---";
            }
            //Debug.Log(PlayerPrefs.GetString("4x4_times_3"));
        }
    }
    public void Displaytext_6x6()
    {
        Title_.text = "Best 6x6";
        for (int i = 0; i < 4; i++)
        {
            wrongs_[i].text = PlayerPrefs.GetString("6x6_wrongs_"+i);
            times_[i].text = PlayerPrefs.GetString("6x6_times_"+i);
            if (wrongs_[i].text.Length == 0)
            {
                wrongs_[i].text = "---";
                times_[i].text = "---";
            }
        }
    }
    public void Displaytext_9x9()
    {
        Title_.text = "Best 9x9";
        for (int i = 0; i < 4; i++)
        {
            wrongs_[i].text = PlayerPrefs.GetString("9x9_wrongs_" + i);
            times_[i].text = PlayerPrefs.GetString("9x9_times_" + i);
            if (wrongs_[i].text.Length == 0)
            {
                wrongs_[i].text = "---";
                times_[i].text = "---";
            }
        }
    }
}
