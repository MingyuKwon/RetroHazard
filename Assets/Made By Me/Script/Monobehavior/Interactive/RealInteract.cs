using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RealInteract : Interact
{
    [Tooltip("Current goal that you will check every simple interact with real interact")]
    [SerializeField] int if_CurrentGoalIndex_Is_Interact = -1;

    [Tooltip("goal that you will change after the simple interact")]
    [SerializeField] int nextGoalIndex_Is_Interact = -1;

    [Tooltip("Current goal that you will check when real interact success")]
    [SerializeField] int if_CurrentGoalIndex_Is_Success = -1;

    [Tooltip("goal that you will change after the real interact success")]
    [SerializeField] int nextGoalIndex_Is_Success = -1;

    private void Start() {
        Debug.Log("Real Interact name : " + this.gameObject.name);
        if(SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_Interact_Destroy[transform.GetSiblingIndex()])
        {
            this.gameObject.SetActive(false);
        }
    }

    public override void OnEnable() {
        base.OnEnable();
        GameManager.EventManager.InteractEvent += InteractiveWithReal;
        TabUI.Interact_KeyItem_Success_Event += realInteractSuccess;
    }

    public override void OnDisable() {
        base.OnDisable();
        GameManager.EventManager.InteractEvent -= InteractiveWithReal;
        TabUI.Interact_KeyItem_Success_Event -= realInteractSuccess;
    }

    private void InteractiveWithReal()
    {
        if(if_CurrentGoalIndex_Is_Interact == -1) return;


        if(if_CurrentGoalIndex_Is_Interact == PlayerGoalCollection.currentGoalIndex)
        {
            PlayerGoalCollection.currentGoalIndex = nextGoalIndex_Is_Interact;
        }
    }

    private void realInteractSuccess(InteractiveDialog interactiveDialog, int n)
    {
        if(interactiveDialog.InteractCode == dialog.InteractCode)
        {
            if(check.activeInHierarchy)
            {
                SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_Interact_Destroy[transform.GetSiblingIndex()] = true;
                this.transform.parent.gameObject.SetActive(false);
            }
        }

        if(if_CurrentGoalIndex_Is_Success == -1) return;

        if(if_CurrentGoalIndex_Is_Success == PlayerGoalCollection.currentGoalIndex)
        {
            PlayerGoalCollection.currentGoalIndex = nextGoalIndex_Is_Success;
        }
    }

}
