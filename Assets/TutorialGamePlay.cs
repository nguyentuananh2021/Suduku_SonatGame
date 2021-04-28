using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialGamePlay : MonoBehaviour
{
    public List<GameObject> squares_;
    public Color color_on_click = Color.yellow;
    public Color color_light_area = Color.blue;
    public List<Button> button_numbers;
    public List<Image> square_box1;
    public List<Image> square_box2;
    public List<Image> square_box3;
    public List<Image> square_box4;
    public List<Animator> anims;

    public List<GameObject> Panels;
    private void Start()
    {
        Panels[0].gameObject.SetActive(true);
        square_box1[0].gameObject.GetComponentInChildren<Text>().text = "1";
        square_box1[1].gameObject.GetComponentInChildren<Text>().text = "2";
        square_box1[2].gameObject.GetComponentInChildren<Text>().text = "3";
        square_box1[3].color = color_light_area;
        PlayerPrefs.SetString("asked", "true");
    }
    public void ClickTheCell_HigLightArea()
    {
        IEnumerator SetColorHiglightArea(float time)
        {

            for (int i = 0; i < square_box1.Count-1; i++)
            {
                yield return new WaitForSeconds(time);
                square_box1[i].color = color_light_area;
            }
        }
        square_box1[3].color = color_on_click;
        StartCoroutine(SetColorHiglightArea(0.1f));
        anims[3].GetComponent<Animator>().SetBool("isClick", true);
    }

    public void ClickNumber_4()
    {
        
        IEnumerator SetColorHiglightArea(float time)
        {
            yield return new WaitForSeconds(time);
            square_box1[3].gameObject.GetComponentInChildren<Text>().text = "4";
            square_box1[3].gameObject.GetComponentInChildren<Text>().color = Color.blue;
            anims[3].GetComponent<Animator>().SetBool("isClick", false);
            anims[1].GetComponent<Animator>().SetBool("isClick", true);
            for (int i = 0; i < square_box1.Count; i++)
            {
               
                yield return new WaitForSeconds(time);
                square_box1[i].color = color_light_area;
                
            }
            Step2();
        }
        StartCoroutine(SetColorHiglightArea(0.1f));

    }
    private void Step2()
    {
        square_box1[0].color = Color.white;
        square_box1[1].color = Color.white;

        square_box2[3].gameObject.GetComponentInChildren<Text>().text = "1";
        square_box2[3].color = color_light_area;
        square_box2[2].color = color_on_click;
        Panels[2].gameObject.SetActive(true);
        
    }
    public List<Image> square_row = new List<Image>();
    
    public void ClickNumber_2()
    {
        square_row.Add(square_box1[2]);
        square_row.Add(square_box1[3]);
        square_row.Add(square_box2[2]);
        square_row.Add(square_box2[3]);

        IEnumerator SetColorHiglightArea(float time)
        {
            yield return new WaitForSeconds(time);
            square_box2[2].gameObject.GetComponentInChildren<Text>().text = "2";
            square_box2[2].gameObject.GetComponentInChildren<Text>().color = Color.blue;
            anims[1].GetComponent<Animator>().SetBool("isClick", false);
            anims[0].GetComponent<Animator>().SetBool("isClick", true);
            for (int i = 0; i < square_row.Count; i++)
            {

                yield return new WaitForSeconds(time);
                square_row[i].color = color_light_area;

            }
            Panels[2].SetActive(false);
            Step3();
        }
        StartCoroutine(SetColorHiglightArea(0.1f));

    }
    private void Step3()
    {
        foreach (var square in square_row)
        {
            square.color = Color.white;
        }
        
        square_box2[0].gameObject.GetComponentInChildren<Text>().text = "4";
        square_box4[0].gameObject.GetComponentInChildren<Text>().text = "3";
        square_box2[0].color = color_light_area;
        square_box4[0].color = color_light_area;
        square_box2[2].color = color_light_area;
        square_box4[2].color = color_on_click;
        
        Panels[3].gameObject.SetActive(true);


    }
    public List<Image> square_collumn = new List<Image>();
    public void ClickNumber_1()
    {
        square_collumn.Add(square_box2[0]);
        square_collumn.Add(square_box2[2]);
        square_collumn.Add(square_box4[0]);
        square_collumn.Add(square_box4[2]);

        IEnumerator SetColorHiglightArea(float time)
        {
            yield return new WaitForSeconds(time);
            square_box4[2].gameObject.GetComponentInChildren<Text>().text = "1";
            square_box4[2].gameObject.GetComponentInChildren<Text>().color = Color.blue;
            anims[0].GetComponent<Animator>().SetBool("isClick", false);
            
            for (int i = 0; i < square_collumn.Count; i++)
            {
                yield return new WaitForSeconds(time);
                square_collumn[i].color = color_light_area;

            }
            Panels[3].SetActive(false);
            Step4();
        }
        StartCoroutine(SetColorHiglightArea(0.1f));

    }
    private void Step4()
    {
        for (int i = 0; i < square_row.Count; i++)
        {
            square_box1[i].color = color_light_area;
            square_row[i].color = color_light_area;
            square_collumn[i].color = color_light_area;
        }
        Panels[4].gameObject.SetActive(true);
        Panels[5].gameObject.SetActive(true);
    }


    public void OnclickStart()
    {
        DropdownGridMode.Instance.SetGridMode(0);

    }
}
