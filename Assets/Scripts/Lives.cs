using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    public static Lives Instance;
    public List<GameObject> error_images;

    int lives_ = 0;
    public int error_number_ = 0;
    public GameObject game_over_popup;
    public Text level_text;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }
    public int GetWrong() 
    {
        return error_number_;
    }
    private void Start()
    {
        level_text.GetComponent<Text>().text = GameSetting.Instance.GetGameMode();
        lives_ = error_images.Count;
        error_number_ = 0;
    }
    private void WrongNumber()
    {
        if(error_number_ < error_images.Count)
        {
            error_images[error_number_].SetActive(true);
            error_number_++;
            lives_--;
        }
        CheckForGameOver();
    }
    private void CheckForGameOver()
    {
        if(lives_<= 0)
        {
            GameEvents.OnGameOverMethod();
            game_over_popup.SetActive(true);
        }
    }
    private void OnEnable()
    {
        GameEvents.OnWrongNumber += WrongNumber;
    }
    private void OnDisable()
    {
        GameEvents.OnWrongNumber -= WrongNumber;
    }
}
