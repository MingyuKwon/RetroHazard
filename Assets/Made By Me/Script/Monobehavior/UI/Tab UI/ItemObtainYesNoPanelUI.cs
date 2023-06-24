using UnityEngine;

public class ItemObtainYesNoPanelUI : MonoBehaviour
{
    TabUI tabUI;
    FocusUI[] focus;

    private void Awake() {
        tabUI = transform.parent.gameObject.GetComponent<TabUI>();
        focus = GetComponentsInChildren<FocusUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(tabUI.yesNoChoice)
        {
            focus[0].SetFocus(true);
            focus[1].SetFocus(false);
        }else
        {
            focus[0].SetFocus(false);
            focus[1].SetFocus(true);
        }
    }
}
