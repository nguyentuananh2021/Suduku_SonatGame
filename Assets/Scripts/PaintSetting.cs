using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintSetting : MonoBehaviour
{
    public Image bg_suduku;
    public Image bg_grid;
    public Image bg_paint_popup;
    public List<Text> all_text;
    public Color bg_Dark = Color.red;
    public Color bg_white = Color.red;
    public Color bg_golden = Color.red;
    public Color bg_square = Color.red;
    public Color square_text = Color.red;
    public Color note_text = Color.red;
    public Color btn_select = Color.red;
    public int TEXT_SQUARE_SIZE_MEDIUM = 200;
    public int TEXT_NOTE_SIZE_MEDIUM = 80;
    
    public int mode = 0;
    public int size = 1;
    //public List<Button> btn_font_size;
    public List<Image> btn_light_mode;
    public List<Image> btn_font_size;
    void Start()
    {
        OnWhiteLight();
        OnFontMedium();
        //SetSelect();
       // btn_font_size[0].Select();
    }
    public void SetSelect()
    {
        for (int i = 0; i < btn_font_size.Count; i++)
        {
            btn_light_mode[i].color = Color.white;
           
            if (mode == i)
            {
                btn_light_mode[i].color = btn_select;
            }
            btn_font_size[i].color = Color.white;
           
            if (size == i)
            {
                btn_font_size[i].color = btn_select;
                if (size == 0)
                {
                    btn_font_size[2].GetComponentInChildren<Text>().color = btn_select;
                    btn_font_size[1].GetComponentInChildren<Text>().color = btn_select;
                    btn_font_size[size].GetComponentInChildren<Text>().color = bg_white;

                }
                if (size == 1)
                {
                    btn_font_size[0].GetComponentInChildren<Text>().color = btn_select;
                    btn_font_size[2].GetComponentInChildren<Text>().color = btn_select;
                    btn_font_size[size].GetComponentInChildren<Text>().color = bg_white;
                }
                if (size == 2)
                {
                    btn_font_size[0].GetComponentInChildren<Text>().color = btn_select;
                    btn_font_size[1].GetComponentInChildren<Text>().color = btn_select;
                    btn_font_size[size].GetComponentInChildren<Text>().color = bg_white;
                }
            }

        }
    }
    private void SetTextColor(Color color_)
    {
        foreach (var text in all_text)
        {
            text.color = color_;
        }
    }
    private void SetBackgroundColor(Color cl_bg_suduku, Color cl_bg_grid, Color cl_bg_paint, Color color_sqaure)
    {
        bg_suduku.color = cl_bg_suduku;
        bg_grid.color = cl_bg_grid;
        bg_paint_popup.color = cl_bg_paint;
        SetSquareColor(color_sqaure);
    }

    public void OnWhiteLight()
    {
        mode = 0; 
        SetSelect();
        SetTextColor(Color.black);
        SetTextColorSquare(square_text, note_text);
        SetBackgroundColor(bg_white, bg_Dark, bg_white, bg_white);
    }
    public void OnGoldenLight()
    {
        mode = 1; 
        SetSelect();
        SetTextColor(Color.black); 
        
        SetTextColorSquare(square_text, note_text);
        SetBackgroundColor(bg_golden, bg_Dark, bg_golden, bg_golden);
    }
    public void OnDarkLight()
    {
        mode = 2; 
        SetSelect();
        SetTextColor(Color.white);
        SetTextColorSquare(bg_white, Color.gray);
        SetBackgroundColor(bg_Dark, Color.gray, bg_Dark, bg_square);
    }


    public void OnFontSmall()
    {
        size =0; SetSelect();
        SetFontSize(TEXT_SQUARE_SIZE_MEDIUM - 50, TEXT_NOTE_SIZE_MEDIUM - 5);
    }
    public void OnFontMedium()
    {
        size =1; SetSelect();
        SetFontSize(TEXT_SQUARE_SIZE_MEDIUM, TEXT_NOTE_SIZE_MEDIUM);
        
    }
    public void OnFontLarge()
    {
        size =2; SetSelect();
        SetFontSize(TEXT_SQUARE_SIZE_MEDIUM + 50, TEXT_NOTE_SIZE_MEDIUM + 5);
    }


    private void SetFontSize(int size_text_square, int size_text_note)
    {

        var grid_square_ = SudukuGrid.Instance.grid_squares_;
        foreach (var square in grid_square_)
        {
            var notes = square.GetComponent<GridSquare>().GetComponentsInChildren<Text>();
            for (int i = 0; i < notes.Length - 1; i++)
            {
                notes[i].fontSize = size_text_note;
            }
            notes[notes.Length - 1].fontSize = size_text_square;
        }
    }





    private void SetSquareColor(Color color_sqaure)
    {
        var grid_square_ = SudukuGrid.Instance.grid_squares_;
        foreach (var square in grid_square_)
        {
            square.GetComponent<GridSquare>().GetComponentInChildren<Image>().color = color_sqaure;
        }
    }
    private void SetTextColorSquare(Color square_text, Color note_text)
    {
        var grid_square_ = SudukuGrid.Instance.grid_squares_;
        foreach (var square in grid_square_)
        {
            var notes = square.GetComponent<GridSquare>().GetComponentsInChildren<Text>();
            for (int i = 0; i < notes.Length-1; i++)
            {
                notes[i].color = note_text;
            }
            notes[notes.Length-1].color = square_text;
        }
    }
}
