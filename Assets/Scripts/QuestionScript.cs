using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetString("asked") == "")
        {
            this.gameObject.SetActive(true);
        }
        else this.gameObject.SetActive(false);
    }

}
