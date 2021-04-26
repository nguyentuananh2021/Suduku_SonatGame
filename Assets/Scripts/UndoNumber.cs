using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UndoNumber : MonoBehaviour
{
    public static UndoNumber Instance;
    public List<Undo> List_Undo;
    public struct Undo
    {
        public int square_index;
        public int number_;
        public List<string> list_note;
        public bool isNote;

        public Undo(int square_index, int number_, List<string> list_note, bool isNote)
        {
            this.square_index = square_index;
            this.number_ = number_;
            this.list_note = list_note;
            this.isNote = isNote;
        }
    }
    private void Awake()
    {
        if (Instance) Destroy(this);
        Instance = this;
    }
    void Start()
    {
        List_Undo = new List<Undo>();
    }
    public void AddUndoNumber(int square_index, int number_, bool isNote = false)
    {
        List<string> list_undo = new List<string>();
        foreach (var item in SudukuGrid.Instance.grid_squares_[square_index].GetComponent<GridSquare>().GetComponentInChildren<GridLayoutGroup>().GetComponentsInChildren<Text>())
        {
            list_undo.Add(item.text);
        }
        List_Undo.Add(new Undo(square_index, number_, list_undo, isNote));
    }
    public void OnUndo()
    {
        Undo undo = new Undo();
        var square = SudukuGrid.Instance.grid_squares_[undo.square_index].GetComponent<GridSquare>();
        if (List_Undo.Count > 0)
        {
            undo = List_Undo[List_Undo.Count - 1];

            if (undo.isNote)
            {
                square.number_notes.Clear();
                //GameEvents.OnNotesActiveMethod(true);
                foreach (var item in undo.list_note)
                {
                    square.GetComponentInChildren<GridLayoutGroup>().GetComponentsInChildren<Text>()[int.Parse(item)].text = item;
                }
            }
            else
            {
                //GameEvents.OnNotesActiveMethod(false);
                
                square.SetNoteNumberValue(0);
                square.SetNumber(undo.number_);
                square.OnSetNumber(undo.number_);
            }

            square.SetNumberData(undo.number_, undo.square_index);
            List_Undo.Remove(List_Undo[List_Undo.Count - 1]);
        }
        else Debug.Log("list undo = "+List_Undo.Count);
        
    }

}
