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
            videoIndex = -1;
            audioIndex = -1;
            generalIndex = -1;
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

    public static int videoIndex{
        get{
            return optionUILogic.videoIndex;
        }

        set{
            optionUILogic.videoIndex = value;
            optionUILogic.SetVideoIndexFocus(optionUILogic.videoIndex);
        }
    }
    public GameObject[] displayFocus;

    public static int audioIndex{
        get{
            return optionUILogic.audioIndex;
        }

        set{
            optionUILogic.audioIndex = value;
            optionUILogic.SetAudioIndexFocus(optionUILogic.audioIndex);
        }
    }
    public GameObject[] audioFocus;

    public static int generalIndex{
        get{
            return optionUILogic.generalIndex;
        }

        set{
            optionUILogic.generalIndex = value;
            optionUILogic.SetGeneralIndexFocus(optionUILogic.generalIndex);
        }
    }
    public GameObject[] generalFocus;

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

    public void OkAudioOption()
    {
        optionUILogic.OkAudioOption();
    }

    public void OkVideoOption()
    {
        optionUILogic.OkVideoOption();
    }

    public void OkGeneralOption()
    {
        optionUILogic.OkGeneralOption();
    }

}
