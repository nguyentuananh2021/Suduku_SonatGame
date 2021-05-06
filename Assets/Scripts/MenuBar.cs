using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBar : MonoBehaviour
{
    public Color color_on_click = Color.red;
    public Color color_out_click = Color.red;
    private Button[] buttons;
    void Start()
    {
        buttons = GetComponentsInChildren<Button>();
        SetColorButton(0);
    }
    public void SetColorButton(int index)
    {

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == index)
            {
                buttons[i].GetComponent<Image>().color = color_on_click;
            }
            else
                buttons[i].GetComponent<Image>().color = color_out_click;
        }

    }

}
