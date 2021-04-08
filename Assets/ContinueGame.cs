using System;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ContinueGame : MonoBehaviour
{
    public TMP_Text text_continue;
    public static ContinueGame Instance;
    private void Awake()
    {
        if (Instance) Destroy(this);
        //DontDestroyOnLoad(this);
        Instance = this;
    }
    void Start()
    {
        Display();
    }
    public void SetGamePlay()
    {

        Dropdown.Instance.SetGridMode(int.Parse(JsonUtility.FromJson<Data>(PlayerPrefs.GetString("json_data")).grid_mode.Split('x')[1]));
        GameSetting.Instance.SetGameMode(SudukuJSON.Instance.GetGameMode());
        
        SceneManager.LoadScene("GameScene");
    }


    private void Display()
    {
        Debug.Log(PlayerPrefs.GetString("json_data"));
        var data = JsonUtility.FromJson<Data>(PlayerPrefs.GetString("json_data"));
        if (PlayerPrefs.GetString("json_data") == "")
        {
            SetActive(false);
        }
        else
        {
            text_continue.text = data.grid_mode + " " + data.times_;
            SetActive(true);
        }
    }
    public void SetActive(bool bool_)
    {
        gameObject.SetActive(bool_);
    }
}
