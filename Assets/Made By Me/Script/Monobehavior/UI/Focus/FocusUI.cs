using UnityEngine;
using UnityEngine.UI;

public class FocusUI : MonoBehaviour
{
    Image image;
    public SelectButton[] selectButtons;
    public Text[] selectTexts;

    private void Awake() {
        selectButtons = transform.GetChild(0).GetComponentsInChildren<SelectButton>();
        selectTexts = transform.GetChild(0).GetComponentsInChildren<Text>();
        image = GetComponent<Image>();
        image.enabled = false;
        if(transform.parent.GetComponent<ItemContainer>() == null && transform.parent.GetComponent<BoxItemContainer>() == null && transform.parent.GetComponent<PlayerItemContainer>() == null )
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    public void SetFocus(bool flag)
    {
        image.enabled = flag;
    }

    public void SetselectText(int index, string str)
    {
        switch(str)
        {
            case "DisArm":
                if(GameAudioManager.LanguageManager.currentLanguage == "E")
                {
                    selectTexts[index].text = "DisArm";
                }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
                {
                    selectTexts[index].text = "해제하기";
                }
                break;

            case "Equip":
                if(GameAudioManager.LanguageManager.currentLanguage == "E")
                {
                    selectTexts[index].text = "Equip";
                }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
                {
                    selectTexts[index].text = "장착하기";
                }
                break;
            case "Use":
                if(GameAudioManager.LanguageManager.currentLanguage == "E")
                {
                    selectTexts[index].text = "Use";
                }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
                {
                    selectTexts[index].text = "사용하기";
                }
                break;
            
            case "Combine":
                if(GameAudioManager.LanguageManager.currentLanguage == "E")
                {
                    selectTexts[index].text = "Combine";
                }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
                {
                    selectTexts[index].text = "조합하기";
                }
                break;
            
            case "Discard":
                if(GameAudioManager.LanguageManager.currentLanguage == "E")
                {
                    selectTexts[index].text = "Discard";
                }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
                {
                    selectTexts[index].text = "버리기";
                }
                break;
            

            case "Take item":
                if(GameAudioManager.LanguageManager.currentLanguage == "E")
                {
                    selectTexts[index].text = "Take item";
                }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
                {
                    selectTexts[index].text = "가져가기";
                }
                break;

            case "Put item":
                if(GameAudioManager.LanguageManager.currentLanguage == "E")
                {
                    selectTexts[index].text = "Put item";
                }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
                {
                    selectTexts[index].text = "넣기";
                }
                break;


            default :
                selectTexts[index].text = "";
                break;
        }
    }

    public void SetSelect(int index)
    {
        if(index < 0)
        {
            for(int i = 0; i<3; i++)
            {
                selectButtons[i].SetSelect(false);
            }
            return;
        }

        for(int i = 0; i<3; i++)
        {
            if(index == i)
            {
                selectButtons[i].SetSelect(true);
            }else
            {
                selectButtons[i].SetSelect(false);
            }
            
        }
    }
}
