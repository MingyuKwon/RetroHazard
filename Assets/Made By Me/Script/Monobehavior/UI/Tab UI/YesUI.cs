using UnityEngine;
using UnityEngine.EventSystems;

public class YesUI : MonoBehaviour, IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!UI.instance.tabUI.isOpenedItem) return;
        UI.instance.tabUI.yesNoChoice = true;
    }

}
