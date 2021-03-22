using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuButtons : MonoBehaviour
{
    private int grid_mode;
    private void Start()
    {
        grid_mode = GameSetting.Instance.dropdown_value;
    }
    public void LoadSence(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadEasyGame(string name)
    {
        switch (grid_mode)
        {
            case 0:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.EASY_4x4);
                SceneManager.LoadScene(name);
                break;
            case 1:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.EASY_6x6);
                SceneManager.LoadScene(name);
                break;
            case 2:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.EASY_9x9);
                SceneManager.LoadScene(name);
                break;
        }
        
    }
    public void LoadMediumGame(string name)
    {
        switch (grid_mode)
        {
            case 0:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.MEDIUM_4x4);
                SceneManager.LoadScene(name);
                break;
            case 1:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.MEDIUM_6x6);
                SceneManager.LoadScene(name);
                break;
            case 2:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.MEDIUM_9x9);
                SceneManager.LoadScene(name);
                break;
        }
    }
    public void LoadHardGame(string name)
    {
        switch (grid_mode)
        {
            case 0:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.HARD_4x4);
                SceneManager.LoadScene(name);
                break;
            case 1:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.HARD_6x6);
                SceneManager.LoadScene(name);
                break;
            case 2:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.HARD_9x9);
                SceneManager.LoadScene(name);
                break;
        }
    }
    public void LoadVeryHardGame(string name)
    {
        switch (grid_mode)
        {
            case 0:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.VERY_HARD_4x4);
                SceneManager.LoadScene(name);
                break;
            case 1:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.VERY_HARD_6x6);
                SceneManager.LoadScene(name);
                break;
            case 2:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.VERY_HARD_9x9);
                SceneManager.LoadScene(name);
                break;
        }
    }
    public void ActivateObject(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void DeActivateObject(GameObject obj)
    {
        obj.SetActive(false);
    }

}
