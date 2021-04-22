using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHint : MonoBehaviour
{
    public int[] arr_index_square;
    public struct Note
    {
        public int value;
        public List<int> list_index;

        public Note(int value, List<int> list_index)
        {
            this.value = value;
            this.list_index = list_index;
        }
    }
    public List<Note> note_list = new List<Note>();

    private void Start()
    {
        InitializationNoteList(DropdownGridMode.Instance.GetGridMode(), note_list);
    }
    private void InitializationNoteList(int n, List<Note> note_list_)
    {
        note_list_.Clear();
        for (int i = 1; i <= n; i++)
        {
            note_list_.Add(new Note(i, new List<int> { }));
        }
    }
    public void SetNoteList(int square_index_)
    {
        var data = SudukuData.Instance.data;
        arr_index_square = LineIndicator.Instance.GetSquare(square_index_);
        for (int i = 0; i < arr_index_square.Length; i++)
        {
            if (data.unsolved_data[arr_index_square[i]] == 0)
            {
                var arr_index_ver = LineIndicator.Instance.GetVerticalLine(arr_index_square[i]);
                var arr_index_hor = LineIndicator.Instance.GetHorizontalLine(arr_index_square[i]);
                for (int value = 1; value <= arr_index_square.Length; value++)
                {
                    if (Is_Only(value, arr_index_square, arr_index_hor, arr_index_ver, data.unsolved_data))
                    {
                        note_list.Find(x => x.value == value).list_index.Add(arr_index_square[i]);
                    }

                }
            }

        }
    }
    private bool Is_Only(int value_, int[] arr_squ, int[] arr_hor, int[] arr_ver, int[] data_unsovlved)
    {
        for (int i = 0; i < arr_squ.Length; i++)
        {
            if (data_unsovlved[arr_squ[i]] == value_ || data_unsovlved[arr_hor[i]] == value_ || data_unsovlved[arr_ver[i]] == value_)
            {
                return false;
            }
        }
        return true;
    }


    // check last digit
    private void CheckLastDigit(int square_index)
    {
        List<Note> list_check = new List<Note>();
        InitializationNoteList(note_list.Count, list_check);
        var arr_box = LineIndicator.Instance.GetSquare(square_index);
        var arr_Hor = LineIndicator.Instance.GetHorizontalLine(square_index);
        var arr_Ver = LineIndicator.Instance.GetVerticalLine(square_index);
        for (int i = 0; i < arr_box.Length; i++)
        {
            if (SudukuData.Instance.data.solved_data[arr_box[i]] == 0)
            {
                for (int number = 0; number < arr_box.Length; number++)
                {
                    for (int j = 0; j < arr_box.Length; j++)
                    {
                        if( SudukuData.Instance.data.solved_data[arr_box[j]] == number ||
                            SudukuData.Instance.data.solved_data[arr_Hor[j]] == number ||
                            SudukuData.Instance.data.solved_data[arr_Ver[j]] == number)
                        {

                        }
                    }
                }
            }

        }
    }
    //
}
