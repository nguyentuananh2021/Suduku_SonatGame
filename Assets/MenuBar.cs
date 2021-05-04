using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBar : MonoBehaviour
{
    public Color color_on_click = Color.red;
    public Color color_out_click = Color.red;
    public List<Button> buttons;
    void Start()
    {
        SetColorButton(1);
    }
    public void SetColorButton(int index)
    {

        for (int i = 0; i < buttons.Count; i++)
        {
            if (i == index)
            {
                buttons[i].GetComponent<Image>().color = color_on_click;
            }
            else
                buttons[i].GetComponent<Image>().color = color_out_click;
        }

    }
    public void on_click_(int index)
    {
        switch (index){
            case 0:

                break;
            case 1:
                
                break;
            case 2:

                break;
            case 3:
                break;
        }
    }
}
