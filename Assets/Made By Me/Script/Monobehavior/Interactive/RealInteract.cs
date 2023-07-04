using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RealInteract : Interact
{
    [Tooltip("Current goal that you will check every simple interact with real interact")]  // interact 할 때 정해져 있어야 할 목표
    [SerializeField] int if_CurrentGoalIndex_Is_Interact = -1;

    [Tooltip("goal that you will change after the simple interact")] // interact만 하면 바뀔 목표
    [SerializeField] int nextGoalIndex_Is_Interact = -1;

    [Tooltip("Current goal that you will check when real interact success")] // interact에 성공했을 때 현재 설정 되어 있어야 할 목표
    [SerializeField] int if_CurrentGoalIndex_Is_Success = -1;

    [Tooltip("goal that you will change after the real interact success")] // interact에 성공했을 때 다음으로 설정 되어야 할 목표
    [SerializeField] int nextGoalIndex_Is_Success = -1;

    private void Start() {
        Debug.Log("Real Interact name : " + this.gameObject.name);
        if(SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_Interact_Destroy[transform.GetSiblingIndex()])
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);

            BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();

            if (boxCollider2D != null)
            {
                Destroy(boxCollider2D);
            }

        }else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    public override void OnEnable() {
        base.OnEnable();
        GameManager.EventManager.InteractEvent += InteractiveWithReal;
        GameManager.EventManager.Interact_KeyItem_Success_Event += realInteractSuccess;
    }

    public override void OnDisable() {
        base.OnDisable();
        GameManager.EventManager.InteractEvent -= InteractiveWithReal;
        GameManager.EventManager.Interact_KeyItem_Success_Event -= realInteractSuccess;
    }

    private void InteractiveWithReal() // 이건 실제 아이템으로 성공하지 않고 그냥 interact만 했을 때 뜨는 창을 띄우는거
    {
        if(if_CurrentGoalIndex_Is_Interact == -1) return;


        if(if_CurrentGoalIndex_Is_Interact == PlayerGoalCollection.currentGoalIndex)
        {
            PlayerGoalCollection.currentGoalIndex = nextGoalIndex_Is_Interact;
        }
    }

    private void realInteractSuccess(InteractiveDialog interactiveDialog, int n) // 이게 성공
    {
        if(interactiveDialog.InteractCode == dialog.InteractCode)
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
                    GameManager.EventManager.InvokeInteractNoticeEvent(noticeIndex_ifExist);
                }
            
            }
        }

        if(if_CurrentGoalIndex_Is_Success == -1) return;

        if(if_CurrentGoalIndex_Is_Success == PlayerGoalCollection.currentGoalIndex)
        {
            PlayerGoalCollection.currentGoalIndex = nextGoalIndex_Is_Success;
        }
    }

}
