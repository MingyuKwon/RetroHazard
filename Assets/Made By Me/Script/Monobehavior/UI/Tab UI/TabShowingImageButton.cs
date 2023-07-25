using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabShowingImageButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text text;
    public string english;
    public string korean;
    [SerializeField] int index;

    private void Awake() {
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
            text.text = english;
        }else if(language == "K")
        {
            text.text = korean;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UI.instance.tabUI.isMouseOnshowingTabIndex = index;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UI.instance.tabUI.isMouseOnshowingTabIndex = -1;
    }
}
