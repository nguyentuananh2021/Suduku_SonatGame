using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class NumberButton : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{
    public int value = 0;
    private Lives lives = new Lives();
    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log("Number Button Value: " +value);
        GameEvents.OnUpdateSquareNumberMethod(value);
        lives.checkSolveData(Lives.value_Square_Data, value);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        
    }
}
