using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudukuData : MonoBehaviour
{

    public class SudukuEasyData : MonoBehaviour
    {
        public static List<SudukuData.SudukuBoardData> GetData()
        {
            List<SudukuData.SudukuBoardData> data = new List<SudukuData.SudukuBoardData>();
            data.Add(new SudukuData.SudukuBoardData(
                new int[81]
                {
                9,0,0,7,4,0,6,8,0,3,4,0,6,8,0,1,2,7,0,7,0,0,1,2,4,0,9,0,2,0,5,0,4,0,6,8,0,8,4,1,0,6,3,0,0,7,6,0,2,0,8,0,1,4,2,9,1,8,0,3,7,4,5,8,3,0,4,5,1,0,9,6,4,0,6,9,0,7,0,3,1
                },
                new int[81]
                {
                9,1,2,7,4,5,6,8,3,3,4,5,6,8,9,1,2,7,6,7,8,3,1,2,4,5,9,1,2,3,5,7,4,9,6,8,5,8,4,1,9,6,3,7,2,7,6,9,2,3,8,5,1,4,2,9,1,8,6,3,7,4,5,8,3,7,4,5,1,2,9,6,4,5,6,9,2,7,8,3,1
                }
                ));
            data.Add(new SudukuData.SudukuBoardData(
                new int[81]
                {
                    1,0,7,2,0,8,3,0,9,2,3,5,0,7,9,0,4,8,6,8,0,3,1,4,2,5,7,3,6,0,8,9,2,0,7,4,9,5,4,0,3,6,8,0,2,7,2,0,1,4,0,9,3,6,0,9,0,5,2,0,0,0,1,8,1,2,4,0,3,7,9,0,0,7,6,0,8,1,0,2,0
                },
                new int[81]
                {
                    1,4,7,2,5,8,3,6,9,2,3,5,6,7,9,1,4,8,6,8,9,3,1,4,2,5,7,3,6,1,8,9,2,5,7,4,9,5,4,7,3,6,8,1,2,7,2,8,1,4,5,9,3,6,4,9,3,5,2,7,6,8,1,8,1,2,4,6,3,7,9,5,5,7,6,9,8,1,4,2,3
                }
                ));
            return data;
        }

    }
    public class SudukuMediumData : MonoBehaviour
    {
        public static List<SudukuData.SudukuBoardData> GetData()
        {
            List<SudukuData.SudukuBoardData> data = new List<SudukuData.SudukuBoardData>();
            data.Add(new SudukuData.SudukuBoardData(
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
            data.Add(new SudukuData.SudukuBoardData(
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

    }
    public class SudukuHardData : MonoBehaviour
    {
        public static List<SudukuData.SudukuBoardData> GetData()
        {
            List<SudukuData.SudukuBoardData> data = new List<SudukuData.SudukuBoardData>();
            data.Add(new SudukuData.SudukuBoardData(
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
            data.Add(new SudukuData.SudukuBoardData(
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

    }
    public class SudukuVeryHardData : MonoBehaviour
    {
        public static List<SudukuData.SudukuBoardData> GetData()
        {
            List<SudukuData.SudukuBoardData> data = new List<SudukuData.SudukuBoardData>();
            data.Add(new SudukuData.SudukuBoardData(
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
            data.Add(new SudukuData.SudukuBoardData(
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

    }


    public static SudukuData Instance;
    public struct SudukuBoardData
    {
        public int[] unsolve_data;
        public int[] solve_data;

        public SudukuBoardData(int[] unsolve_data, int[] solve_data) : this()
        {
            this.unsolve_data = unsolve_data;
            this.solve_data = solve_data;
        }
    }

    public Dictionary<string, List<SudukuBoardData>> suduku_game = new Dictionary<string, List<SudukuBoardData>>();


    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } 
        else
            Destroy(this);

        suduku_game.Add("Easy", SudukuEasyData.GetData());
        suduku_game.Add("Medium", SudukuMediumData.GetData());
        suduku_game.Add("Hard", SudukuHardData.GetData());
        suduku_game.Add("Very Hard", SudukuVeryHardData.GetData());
    }

}
