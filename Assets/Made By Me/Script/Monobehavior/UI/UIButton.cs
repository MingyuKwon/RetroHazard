using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GameObject focus;
    [SerializeField] int index;

    private void Awake() {
        focus = transform.GetChild(1).gameObject;
        SetFocus(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(UI.instance.PauseUI.pauseMainUI.gameObject.activeInHierarchy)
        {
            UI.instance.PauseUI.pauseMainUI.currentIndex = index;
        }else if(UI.instance.PauseUI.saveSlotUI.gameObject.activeInHierarchy)
        {

        }else if(UI.instance.PauseUI.optionUI.gameObject.activeInHierarchy)
        {

        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(UI.instance.PauseUI.pauseMainUI.gameObject.activeInHierarchy)
        {
            UI.instance.PauseUI.pauseMainUI.currentIndex = -1;
        }else if(UI.instance.PauseUI.saveSlotUI.gameObject.activeInHierarchy)
        {

        }else if(UI.instance.PauseUI.optionUI.gameObject.activeInHierarchy)
        {

        }
        focus.SetActive(false);
    }

    public void SetFocus(bool flag)
    {
        focus.SetActive(flag);
    }
}
