using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseRootUI : MonoBehaviour
{
    UI ui;

    private void Awake() {
        ui = GetComponentInParent<UI>();
    }
    private void OnEnable() {
        GameMangerInput.instance.changePlayerInputRule(2);
        ui.MouseCursor(true);
    }

    private void OnDisable() { // have an error when game is closed ny force while pause Ui is opened
        GameMangerInput.instance.changePlayerInputRule(0);
        ui.MouseCursor(false);
    }
}
