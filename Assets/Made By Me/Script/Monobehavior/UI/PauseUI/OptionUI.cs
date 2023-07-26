using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    static OptionUILogic optionUILogic;
    public static int panelNum{
        get{
            return optionUILogic.panelNum;
        }

        set{
            optionUILogic.panelNum = value;
            panelsearchNum = value;
            optionUILogic.ChangePanel(optionUILogic.panelNum);
        }
    }

    public static int panelsearchNum{
        get{
            return optionUILogic.panelsearchNum;
        }

        set{
            optionUILogic.panelsearchNum = value;
            optionUILogic.ChangeOptionButtonFocus(optionUILogic.panelsearchNum);
        }
    }

    private void Awake() {
        optionUILogic = new OptionUILogic(this);
        panelNum = 1;
        panelNum = 0;

        panelsearchNum = 0;
    }

    private void OnEnable() {
        optionUILogic.OnEnable();
    }

    private void OnDisable() {
        optionUILogic.OnDisable();
    }

    private void OnDestroy() {
        optionUILogic.OnDestroy();
    }

}
