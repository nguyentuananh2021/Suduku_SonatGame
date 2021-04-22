using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HintNumberButton : Selectable, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameEvents.OnHintNumberMethod();
    }

}
