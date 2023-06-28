using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class SaveSlotDeleteButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(SceneManager.GetActiveScene().name == "Main Menu")
        {
            MainMenu.instance.saveSlotUI.isDelete = true;
        }else
        {
            UI.instance.PauseUI.saveSlotUI.isDelete = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(SceneManager.GetActiveScene().name == "Main Menu")
        {
            MainMenu.instance.saveSlotUI.isDelete = false;
        }else
        {
            UI.instance.PauseUI.saveSlotUI.isDelete = false;
        }
    }
}
