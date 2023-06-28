using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int index;

    private void Awake() {
       transform.GetChild(1).gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(MainMenu.instance.mainMenuUI.gameObject.activeInHierarchy)
        {
            MainMenu.instance.mainMenuUI.selectIndex = index;
                
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(MainMenu.instance.mainMenuUI.gameObject.activeInHierarchy)
        {
            MainMenu.instance.mainMenuUI.selectIndex = -1;
                
        }
    }
}
