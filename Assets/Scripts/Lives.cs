using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public Text wrong_text;
    public int wrong_number = 0;

    public static int value_Square_Data;

    private static List<SudukuData.SudukuBoardData> data;
    void Start()
    {
        Debug.Log("text = " + wrong_text.GetComponent<Text>().text);
        data = new List<SudukuData.SudukuBoardData>();
        data = SudukuData.GetData();
     

        Debug.Log(SudukuGrid.selectData + "square_solve = ");
    }
 
    public void checkSolveData(int value_data, int number_button)
    {
        if(value_data != number_button)
        {
            Debug.Log("erro number !!!");
            ErroNumber();
        }
    }
    public void GetValueSquareData(int square_index)
    {
        value_Square_Data = data[SudukuGrid.selectData].solve_data[square_index];
    }
    public void ErroNumber()
    {
        wrong_number ++;
        //Debug.Log("wrong_number"+ wrong_number);
        //Debug.Log("wrong_text" + wrong_text.text);
        wrong_text.GetComponent<Text>().text = "2";
        if(wrong_number > 0)
        {
            wrong_text.GetComponent<Text>().color = Color.red;
        }
    }
}
