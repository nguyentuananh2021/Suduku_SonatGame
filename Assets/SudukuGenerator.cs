using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudukuGenerator:MonoBehaviour
{
	public static SudukuGenerator Instance;
    private void Awake()
    {
		if (Instance == null)
		{
			Instance = this;
		}
		else Destroy(this);
    }
    private static int[] RandomNumberRowOne(int n)
	{
		int[] arr = new int[n];
		for (int i = 0; i < arr.Length; i++)
		{
			arr[i] = i + 1;
		}
		return Shuffle(arr);
	}
	private static int[] Shuffle(int[] arr)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			//temp = value a[n]
			int ran = Random.Range(0, arr.Length);
			int temp = arr[ran];
			arr[ran] = arr[i];
			arr[i] = temp;
		}
		return arr;
	}
	public static int[] GetDataSolve(int n)
    {
		int[] row_1 = RandomNumberRowOne(n);
		int[,] puzzle = new int[n, n];
		int[] data_solve = new int[n * n];
        for (int i = 0; i < n; i++)
        {
			puzzle[0, i] = row_1[i];
        }
		SolveSudoku(puzzle, 1, 0, n);
		int j = 0;
		foreach (var item in puzzle)
        {
			data_solve[j] = item;
			j++;
        }
        return data_solve;
    }
	public static int[] GetDataUnsolve(int[] data_solve, int k)
    {
		int[] data_unsolve = new int[data_solve.Length];
        for (int i = 0; i < data_solve.Length; i++)
        {
			data_unsolve[i] = data_solve[i];
        }

		for (int i = 0; i < k; i++)
        {
			int random = Random.Range(0, data_unsolve.Length - 1);
			if (data_unsolve[random] == 0)
				i--;
			else
				data_unsolve[random] = 0;
		}
		return data_unsolve;
	}




	public static bool SolveSudoku(int[,] puzzle, int row, int col, int grid_mode)
	{

		if (row < grid_mode && col < grid_mode)
		{
			if (puzzle[row, col] != 0)
			{
				if ((col + 1) < grid_mode) return SolveSudoku(puzzle, row, col + 1, grid_mode);
				else if ((row + 1) < grid_mode) return SolveSudoku(puzzle, row + 1, 0, grid_mode);
				else return true;
			}
			else
			{
				for (int i = 0; i < grid_mode; ++i)
				{
					if (IsAvailable(puzzle, row, col, i + 1, grid_mode))
					{
						puzzle[row, col] = i + 1;

						if ((col + 1) < grid_mode)
						{
							if (SolveSudoku(puzzle, row, col + 1, grid_mode)) return true;
							else puzzle[row, col] = 0;
						}
						else if ((row + 1) < grid_mode)
						{
							if (SolveSudoku(puzzle, row + 1, 0, grid_mode)) return true;
							else puzzle[row, col] = 0;
						}
						else return true;
					}
				}
			}

			return false;
		}
		else return true;
	}

	private static bool IsAvailable(int[,] puzzle, int row, int col, int num, int grid_mode)
	{
		int rowStart;
		int colStart;
		switch (grid_mode)
		{
			case 4:
				rowStart = (row / 2) * 2;
				colStart = (col / 2) * 2;

				for (int i = 0; i < grid_mode; ++i)
				{
					if (puzzle[row, i] == num) return false;
					if (puzzle[i, col] == num) return false;
					if (puzzle[rowStart + (i % 2), colStart + (i / 2)] == num) return false;
				}
				break;
			case 6:
				rowStart = (row / 2) * 2;
				colStart = (col / 3) * 3;

				for (int i = 0; i < grid_mode; ++i)
				{
					if (puzzle[row, i] == num) return false;
					if (puzzle[i, col] == num) return false;
					if (puzzle[rowStart + (i % 2), colStart + (i % 3)] == num) return false;
				}
				break;
			case 9:
				rowStart = (row / 3) * 3;
				colStart = (col / 3) * 3;

				for (int i = 0; i < grid_mode; ++i)
				{
					if (puzzle[row, i] == num) return false;
					if (puzzle[i, col] == num) return false;
					if (puzzle[rowStart + (i % 3), colStart + (i / 3)] == num) return false;
				}
				break;
		}
		return true;
	}
}
