using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FocusUI : MonoBehaviour
{
    
    Image image;
    public SelectButton[] selectButtons;

    private void Awake() {
        selectButtons = transform.GetChild(0).GetComponentsInChildren<SelectButton>();
        image = GetComponent<Image>();
        image.enabled = false;
        if(transform.parent.GetComponent<ItemContainer>() == null)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        
    }


    public void SetFocus(bool flag)
    {
        image.enabled = flag;
    }

    public void SetSelect(int index)
    {
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
