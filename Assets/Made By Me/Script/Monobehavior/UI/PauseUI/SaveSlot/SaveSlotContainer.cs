using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SaveSlotContainer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int index;

    Image focus;

    public Text whereText;
    public Text GoalText;

    private void Awake() {
       transform.GetChild(6).gameObject.SetActive(false);
       ChangeLanguage(GameAudioManager.LanguageManager.currentLanguage);

       GameAudioManager.LanguageManager.languageChangeEvent += ChangeLanguage;
    }

    private void OnDestroy() {
        GameAudioManager.LanguageManager.languageChangeEvent -= ChangeLanguage;
    }

    private void ChangeLanguage(string language)
    {
        if(language == "E")
        {
            whereText.text = "Where : ";
            GoalText.text = "Current Goal : ";
        }else if(language == "K")
        {
            whereText.text = "장소 : ";
            GoalText.text = "현재 목표 : ";
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(SceneManager.GetActiveScene().name == "Main Menu")
        {
            MainMenu.instance.saveSlotUI.saveSlotIndex = index;
        }else
        {
            UI.instance.PauseUI.saveSlotUI.saveSlotIndex = index;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(SceneManager.GetActiveScene().name == "Main Menu")
        {
            MainMenu.instance.saveSlotUI.saveSlotIndex = -1;
        }else
        {
            UI.instance.PauseUI.saveSlotUI.saveSlotIndex = -1;
        }
    }



}
