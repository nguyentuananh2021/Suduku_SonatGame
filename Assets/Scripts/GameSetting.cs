using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    public enum EGameMode
    {
        NOT_SET,
        EASY,
        MEDIUM,
        HARD,
        VERY_HARD
    }
    public static GameSetting Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
            Destroy(this);
    }
    private EGameMode _GameMode;
    private void Start()
    {
        _GameMode = EGameMode.NOT_SET;
    }

    public void SetGameMode(EGameMode mode)
    {
        _GameMode = mode;
    }
    public void SetGameMode(string mode)
    {
        if (mode == "Easy") SetGameMode(EGameMode.EASY);
        else if (mode == "Medium") SetGameMode(EGameMode.EASY);
        else if (mode == "Hard") SetGameMode(EGameMode.EASY);
        else if (mode == "VeryHard") SetGameMode(EGameMode.EASY);
        else SetGameMode(EGameMode.NOT_SET);
    }

    public string GetGameMode()
    {
        switch (_GameMode)
        {
            case EGameMode.EASY: return "Easy";
            case EGameMode.MEDIUM: return "Medium";
            case EGameMode.HARD: return "Hard";
            case EGameMode.VERY_HARD: return "VeryHard";
        }
        Debug.Log("Error: game level is not set...!");
        return "";
    }
}
