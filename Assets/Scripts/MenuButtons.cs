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
        GameSetting.Instance.SetGameMode(GameSetting.EGameMode.EASY);
        SceneManager.LoadScene(name);
    }
    public void LoadMediumGame(string name)
    {
        GameSetting.Instance.SetGameMode(GameSetting.EGameMode.MEDIUM);
        SceneManager.LoadScene(name);
    }
    public void LoadHardGame(string name)
    {
        GameSetting.Instance.SetGameMode(GameSetting.EGameMode.HARD);
        SceneManager.LoadScene(name);
    }
    public void LoadVeryHardGame(string name)
    {
        GameSetting.Instance.SetGameMode(GameSetting.EGameMode.VERY_HARD);
        SceneManager.LoadScene(name);
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
