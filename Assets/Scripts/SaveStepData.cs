using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStepData : MonoBehaviour
{
    public static SaveStepData Instance;
    public List<int> list_note_ = new List<int>();
    private void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }
    public void SaveJsonStep(int square_index_, int value, bool note_active,bool is_delete_note)
    {

    }
}
