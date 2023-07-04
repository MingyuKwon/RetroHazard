using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayDoor : RealInteract
{
    [Header("[1,0] [-1,0] [0,1] [0,-1] Select One")]
    public int openX;
    public int openY;

    string[] texts = new string[1];

    public override void OnEnable() {
        base.OnEnable();
    }

    public override void OnDisable() {
        base.OnDisable();
    }

    // 우선 InteractUI 띄우는 것만 구현해 보자
    public void OneWayDoorInteract(Transform playerTransform)
    {
        if(openX == 1 && openY == 0)
        {
            if(playerTransform.position.x < transform.position.x)
            {
                GameManagerUI.instance.SetInteractiveDialogText(dialog.SucessDialog);
            }else
            {
                texts[0] = dialog.Interactive_Situation;
                GameManagerUI.instance.SetInteractiveDialogText(texts);
            }

        }else if(openX == -1 && openY == 0)
        {
            if(playerTransform.position.x > transform.position.x)
            {
                GameManagerUI.instance.SetInteractiveDialogText(dialog.SucessDialog);
            }else
            {
                texts[0] = dialog.Interactive_Situation;
                GameManagerUI.instance.SetInteractiveDialogText(texts);
            }
        }else if(openX == 0 && openY == 1)
        {
            if(playerTransform.position.y < transform.position.y)
            {
                GameManagerUI.instance.SetInteractiveDialogText(dialog.SucessDialog);
            }else
            {
                texts[0] = dialog.Interactive_Situation;
                GameManagerUI.instance.SetInteractiveDialogText(texts);
            }
        }else if(openX == 0 && openY == -1)
        {
            if(playerTransform.position.y > transform.position.y)
            {
                GameManagerUI.instance.SetInteractiveDialogText(dialog.SucessDialog);
            }else
            {
                texts[0] = dialog.Interactive_Situation;
                GameManagerUI.instance.SetInteractiveDialogText(texts);
            }
        }

        GameManagerUI.instance.VisualizeInteractiveUI(true);
        
    }
}
