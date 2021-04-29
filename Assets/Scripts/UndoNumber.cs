using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UndoNumber : MonoBehaviour
{
    public static UndoNumber Instance;
    public List<Undo> List_Undo = new List<Undo>();
    public struct Undo
    {
        public int square_index;
        public int number_;
        public List<string> notes;

        public Undo(int square_index, int number_, List<string> notes)
        {
            this.square_index = square_index;
            this.number_ = number_;
            this.notes = notes;
        }
    }
    private void Awake()
    {
        if (Instance) Destroy(this);
        Instance = this;
    }


    public void SetListUndo(int square_index, int number_, List<string> notes)
    {
        List_Undo.Add(new Undo(square_index, number_, notes));
    }
    public void OnUndo()
    {
        Undo undo = new Undo();
        
        if (List_Undo.Count > 0)
        {
            undo = List_Undo[List_Undo.Count - 1];

            var square = SudukuGrid.Instance.grid_squares_[undo.square_index].GetComponent<GridSquare>();
            square.SetSquareColor(Color.white);
            square.has_wrong_value = false;
            SudukuGrid.Instance.OnSquareSelected(undo.square_index);
            square.GetComponentInChildren<Text>().text = "";
            if (undo.number_ == 0)
            {
                square.SetNumber(0);
               
                square.SetNumberData(0, undo.square_index);
                var text_notes = square.GetComponentInChildren<GridLayoutGroup>().GetComponentsInChildren<Text>();
                for (int i = 0; i < text_notes.Length; i++)
                {
                    text_notes[i].text = undo.notes[i];
                }
            }
            else
            {
                ////square.OnDeleteNumber();
                //square.SetNumber(undo.number_);
                //square.SetNumberData(undo.number_, undo.square_index);
            }
            List_Undo.Remove(List_Undo[List_Undo.Count - 1]);
        }
        else Debug.Log("list undo = "+List_Undo.Count);
    }
}
