using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class OneWayDoor : RealInteract
{
    [Header("[1,0] [-1,0] [0,1] [0,-1] Select One")]
    public int openX;
    public int openY;

    string[] texts = new string[1];

    public override void OnEnable() {
    }

    public override void OnDisable() {
    }

    // 우선 InteractUI 띄우는 것만 구현해 보자
    public void OneWayDoorInteract(Transform playerTransform)
    {
        bool flag = false;

        if(openX == 1 && openY == 0)
        {
            if(playerTransform.position.x < transform.position.x)
            {
                flag = true;
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
                flag = true;
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
                flag = true;
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
                flag = true;
                GameManagerUI.instance.SetInteractiveDialogText(dialog.SucessDialog);
            }else
            {
                texts[0] = dialog.Interactive_Situation;
                GameManagerUI.instance.SetInteractiveDialogText(texts);
            }
        }

        GameManagerUI.instance.VisualizeInteractiveUI(true);

        if(flag)
        {
            if(check.activeInHierarchy)
            {
                SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_Interact_Destroy[transform.GetSiblingIndex()] = true;
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);

                BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();

                if(boxCollider2D != null)
                {
                    Destroy(boxCollider2D);
                }

                if(noticeIndex_ifExist != 0)
                {
                    GameManager.EventManager.InvokeInteractNoticeEvent(noticeIndex_ifExist, true);
                }
            
            }
        }
        
    }
}
