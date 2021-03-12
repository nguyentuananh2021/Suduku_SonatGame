using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SudukuData : MonoBehaviour
{
    public static SudukuData Instance;
    public struct SudukuBoardData
    {
        public int[] unsolve_data;
        public int[] solve_data;

        public SudukuBoardData(int[] unsolve_data, int[] solve_data)
        {
            this.unsolve_data = unsolve_data;
            this.solve_data = solve_data;
        }
    }
    public static List<SudukuBoardData> GetData()
    {
        List<SudukuBoardData> data = new List<SudukuBoardData>();
        data.Add(new SudukuBoardData(
            new int[81]
            {
                2, 1, 0, 9, 8, 3, 4, 6, 7, 0, 2, 1, 5, 9, 8, 3, 4, 6, 0, 4, 2, 1, 0, 9, 8, 3, 0, 6, 7, 4, 0, 1, 5, 9, 0, 3, 0, 6, 0, 4,
                0, 1, 0, 9, 8, 3, 0, 0, 7, 0, 0, 0, 5, 0, 0, 3, 4, 0, 7, 0, 0, 1, 5, 9, 8, 3, 0, 0, 7, 4, 0, 1, 5, 9, 8, 0, 4, 0, 7, 4, 1
            },
            new int[81]
            {
                2, 1, 5, 9, 8, 3, 4, 6, 7, 4, 2, 1, 5, 9, 8, 3, 4, 6, 7, 4, 2, 1, 5, 9, 8, 3, 4, 6, 7, 4, 2, 1, 5, 9, 8, 3, 4, 6, 7, 4,
                2, 1, 5, 9, 8, 3, 4, 6, 7, 4, 2, 1, 5, 9, 8, 3, 4, 6, 7, 4, 2, 1, 5, 9, 8, 3, 4, 6, 7, 4, 2, 1, 5, 9, 8, 3, 4, 6, 7, 4, 1
            }
            ));
        data.Add(new SudukuBoardData(
            new int[81]
            {
                5, 0, 0, 9, 8, 3, 0, 0, 7, 0, 2, 1, 5, 0, 9, 3, 4, 0, 0, 4, 2, 1, 0, 9, 8, 2, 0, 6, 0, 4, 0, 1, 4, 9, 0, 6, 0, 6, 0, 3,
                0, 1, 0, 9, 8, 3, 0, 0, 7, 0, 0, 0, 5, 0, 0, 0, 0, 6, 7, 4, 0, 3, 5, 0, 8, 3, 0, 0, 7, 4, 0, 1, 0, 8, 0, 0, 4, 0, 7, 4, 1
            },
            new int[81]
            {
                5, 2, 5, 9, 8, 3, 7, 7, 7, 4, 2, 1, 5, 9, 9, 3, 4, 6, 7, 4, 2, 1, 5, 9, 8, 2, 4, 6, 7, 4, 2, 1, 4, 9, 8, 6, 4, 6, 7, 3,
                2, 1, 6, 9, 8, 3, 4, 6, 7, 4, 2, 1, 5, 9, 8, 3, 4, 6, 7, 4, 2, 3, 5, 9, 8, 3, 5, 6, 7, 4, 2, 1, 1, 8, 8, 8, 4, 6, 7, 4, 1
            }
            ));
        return data;
    }
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    // Update is called once per frame
 
}
