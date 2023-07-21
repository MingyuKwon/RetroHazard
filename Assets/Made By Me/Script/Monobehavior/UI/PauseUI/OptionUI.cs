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
            optionUILogic.ChangePanel(optionUILogic.panelNum);
        }
    }
    private void Awake() {
        optionUILogic = new OptionUILogic(this);
        panelNum = 0;
    }

}
