using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuButtons : MonoBehaviour
{
    public void LoadSence(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadEasyGame(string name)
    {
        switch (Dropdown.Instance.grid_mode)
        {
            case 4:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.EASY_4x4);
                SceneManager.LoadScene(name);
                break;
            case 6:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.EASY_6x6);
                SceneManager.LoadScene(name);
                break;
            case 9:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.EASY_9x9);
                SceneManager.LoadScene(name);
                break;
        }
        
    }
    public void LoadMediumGame(string name)
    {
        switch (Dropdown.Instance.grid_mode)
        {
            case 4:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.MEDIUM_4x4);
                SceneManager.LoadScene(name);
                break;
            case 6:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.MEDIUM_6x6);
                SceneManager.LoadScene(name);
                break;
            case 9:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.MEDIUM_9x9);
                SceneManager.LoadScene(name);
                break;
        }
    }
    public void LoadHardGame(string name)
    {
        switch (Dropdown.Instance.grid_mode)
        {
            case 4:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.HARD_4x4);
                SceneManager.LoadScene(name);
                break;
            case 6:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.HARD_6x6);
                SceneManager.LoadScene(name);
                break;
            case 9:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.HARD_9x9);
                SceneManager.LoadScene(name);
                break;
        }
    }
    public void LoadVeryHardGame(string name)
    {
        switch (Dropdown.Instance.grid_mode)
        {
            case 4:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.VERY_HARD_4x4);
                SceneManager.LoadScene(name);
                break;
            case 6:
                GameSetting.Instance.SetGameMode(GameSetting.EGameMode.VERY_HARD_6x6);
                SceneManager.LoadScene(name);
                break;
            case 9:
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
