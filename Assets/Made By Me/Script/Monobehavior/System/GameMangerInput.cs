using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class GameMangerInput : MonoBehaviour
{
    public class InputEvent
    {
        public static event Action UPPressed;
        public static void Invoke_UpPressed()
        {
            UPPressed.Invoke();
        }

        public static event Action DownPressed;
        public static void Invoke_DownPressed()
        {
            DownPressed.Invoke();
        }

        public static event Action RightPressed;
        public static void Invoke_RightPressed()
        {
            RightPressed.Invoke();
        }

        public static event Action LeftPressed;
        public static void Invoke_LeftPressed()
        {
            LeftPressed.Invoke();
        }

        public static event Action UPJustPressed;
        public static void Invoke_UPJustPressed()
        {
            UPJustPressed.Invoke();
        }

        public static event Action DownJustPressed;
        public static void Invoke_DownJustPressed()
        {
            DownJustPressed.Invoke();
        }

        public static event Action RightJustPressed;
        public static void Invoke_RightJustPressed()
        {
            RightJustPressed.Invoke();
        }

        public static event Action LeftJustPressed;
        public static void Invoke_LeftJustPressed()
        {
            LeftJustPressed.Invoke();
        }


        public static event Action UPJustReleased;
        public static void Invoke_UPJustReleased()
        {
            UPJustReleased.Invoke();
        }

        public static event Action DownJustReleased;
        public static void Invoke_DownJustReleased()
        {
            DownJustReleased.Invoke();
        }
        
        public static event Action RightJustReleased;
        public static void Invoke_RightJustReleased()
        {
            RightJustReleased.Invoke();
        }

        public static event Action LeftJustReleased;
        public static void Invoke_LeftJustReleased()
        {
            LeftJustReleased.Invoke();
        }

        public static event Action EnergyReload;
        public static void Invoke_EnergyReload()
        {
            EnergyReload.Invoke();
        }

        public static event Action SheildReload;
        public static void Invoke_SheildReload()
        {
            SheildReload.Invoke();
        }


        public static event Action InteractiveJustPressed;
        public static void Invoke_InteractiveJustPressed()
        {
            InteractiveJustPressed.Invoke();
        }

        public static event Action InteractiveJustReleased;
        public static void Invoke_InteractiveJustReleased()
        {
            InteractiveJustReleased.Invoke();
        }





        public static event Action TabUIEnterPressed;
        public static void Invoke_UIEnterPressed()
        {
            if(InputCheck.isNowTabUIInput)
            {
                TabUIEnterPressed.Invoke();
            }
            
        }

        public static event Action TabUIBackPressed;
        public static void Invoke_UIBackPressed()
        {
            if(InputCheck.isNowTabUIInput){
                TabUIBackPressed.Invoke();
            }
            
        }

        public static event Action TabUIUpPressed;
        public static void Invoke_UIUpPressed()
        {
            if(InputCheck.isNowTabUIInput){
                TabUIUpPressed.Invoke();
            }
        }

        public static event Action TabUIDownPressed;
        public static void Invoke_UIDownPressed()
        {
            if(InputCheck.isNowTabUIInput){
                TabUIDownPressed.Invoke();
            }
        }

        public static event Action TabUIRightPressed;
        public static void Invoke_UIRightPressed()
        {
            if(InputCheck.isNowTabUIInput){
                TabUIRightPressed.Invoke();
            }
        }

        public static event Action TabUILeftPressed;
        public static void Invoke_UILeftPressed()
        {
            if(InputCheck.isNowTabUIInput){
                TabUILeftPressed.Invoke();
            }
            
        }
    }

    public class InputCheck
    {
        public static bool isNowFieldInput = false;
        public static bool isNowInteractiveUIInput = false;
        public static bool isNowTabUIInput = true;
        public static bool isNowBoxUIInput = false;
        public static bool isNowPauseUIInput = false;
        public static bool isNowAlertUIInput = false;
        public static bool isNowDialogtUIInput = false;

        private Player player;
        public InputCheck(Player _player)
        {
            player = _player;
        }

        public bool isPressingUP()
        {
            return player.GetButton("Move Up");
        }

        public bool isPressingDown()
        {
            return player.GetButton("Move Down");
        }

        public bool isPressingRight()
        {
            return player.GetButton("Move Right");
        }

        public bool isPressingLeft()
        {
            return player.GetButton("Move Left");
        }

        public bool isPressingShield()
        {
            return player.GetButton("Shield");
        }


        public bool isShieldButtonUp()
        {
            return player.GetButtonUp("Shield");
        }
        
        public bool isInteractiveButtonDown()
        {
            return player.GetButtonDown("Interactive");
        }

        public bool isAttackButtonDown()
        {
            return player.GetButtonDown("Attack");
        }

    }


    public static GameMangerInput instance = null;
    public static InputCheck inputCheck;

    private Player player;
    private ControllerMapEnabler mapEnabler;
    private ControllerMapEnabler.RuleSet[] ruleSets;
    // 0 : normal State
    // 1 : talk with NPC State
    // 2 : UI
    // 3 : Alert

    public int currentInputRule = 0;

    void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
        }

        player = ReInput.players.GetPlayer(0);
        mapEnabler = player.controllers.maps.mapEnabler;

        inputCheck = new InputCheck(player);

        ruleSets = new ControllerMapEnabler.RuleSet[mapEnabler.ruleSets.Count];
        ruleSets[0] = mapEnabler.ruleSets.Find(x => x.tag == "NormalState");
        ruleSets[1] = mapEnabler.ruleSets.Find(x => x.tag == "TalkWithNPC");
        ruleSets[2] = mapEnabler.ruleSets.Find(x => x.tag == "UI");
        ruleSets[3] = mapEnabler.ruleSets.Find(x => x.tag == "Alert");
    }

    private void OnEnable() {
        delegateInputFunctions();
    }

    private void OnDisable() {
        removeInputFunctions();
    }

    [Button]
    public void changePlayerInputRule(int ruleNum)
    {
        StartCoroutine(DelayChangeRule(ruleNum));
    }

    IEnumerator DelayChangeRule(int ruleNum)
    {
        yield return new WaitForEndOfFrame();

        foreach(var rule in ruleSets)
        {
            rule.enabled = false;
        }
        ruleSets[ruleNum].enabled = true;

        currentInputRule = ruleNum;

        if(currentInputRule != 0)
        {
            MouseUI.instance.MouseActive(true);
        }else
        {
            MouseUI.instance.MouseActive(false);
        }

        mapEnabler.Apply();
    }



    public void delegateInputFunctions()
    {
        player.AddInputEventDelegate(UPPressed, UpdateLoopType.Update, InputActionEventType.ButtonPressed, "Move Up");
        player.AddInputEventDelegate(DownPressed, UpdateLoopType.Update, InputActionEventType.ButtonPressed, "Move Down");
        player.AddInputEventDelegate(RightPressed, UpdateLoopType.Update, InputActionEventType.ButtonPressed,"Move Right");
        player.AddInputEventDelegate(LeftPressed, UpdateLoopType.Update, InputActionEventType.ButtonPressed,"Move Left");

        player.AddInputEventDelegate(UPJustPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Move Up");
        player.AddInputEventDelegate(DownJustPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Move Down");
        player.AddInputEventDelegate(RightJustPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed,"Move Right");
        player.AddInputEventDelegate(LeftJustPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed,"Move Left");

        player.AddInputEventDelegate(UPJustReleased, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased, "Move Up");
        player.AddInputEventDelegate(DownJustReleased, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased, "Move Down");
        player.AddInputEventDelegate(RightJustReleased, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased,"Move Right");
        player.AddInputEventDelegate(LeftJustReleased, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased,"Move Left");
    
        player.AddInputEventDelegate(EnergyReloadStart, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Energy Refill");
        player.AddInputEventDelegate(SheildReloadStart, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Sheild Refill");
    
        player.AddInputEventDelegate(InteractiveJustPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Interactive");
        player.AddInputEventDelegate(InteractiveJustReleased, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased, "Interactive");






        player.AddInputEventDelegate(UIEnterPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Enter");
        player.AddInputEventDelegate(UIBackPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Back");
        player.AddInputEventDelegate(UIUpPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectUp");
        player.AddInputEventDelegate(UIDownPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectDown");
        player.AddInputEventDelegate(UIRightPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectRight");
        player.AddInputEventDelegate(UILeftPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectLeft");

    }

    public void removeInputFunctions()
    {
        player.RemoveInputEventDelegate(UPPressed);
        player.RemoveInputEventDelegate(DownPressed);
        player.RemoveInputEventDelegate(RightPressed);
        player.RemoveInputEventDelegate(LeftPressed);

        player.RemoveInputEventDelegate(UPJustPressed);
        player.RemoveInputEventDelegate(DownJustPressed);
        player.RemoveInputEventDelegate(RightJustPressed);
        player.RemoveInputEventDelegate(LeftJustPressed);

        player.RemoveInputEventDelegate(UPJustReleased);
        player.RemoveInputEventDelegate(DownJustReleased);
        player.RemoveInputEventDelegate(RightJustReleased);
        player.RemoveInputEventDelegate(LeftJustReleased);

        player.RemoveInputEventDelegate(EnergyReloadStart);
        player.RemoveInputEventDelegate(SheildReloadStart);

        player.RemoveInputEventDelegate(InteractiveJustPressed);
        player.RemoveInputEventDelegate(InteractiveJustReleased);





        player.RemoveInputEventDelegate(UIEnterPressed);
        player.RemoveInputEventDelegate(UIBackPressed);
        player.RemoveInputEventDelegate(UIUpPressed);
        player.RemoveInputEventDelegate(UIDownPressed);
        player.RemoveInputEventDelegate(UIRightPressed);
        player.RemoveInputEventDelegate(UILeftPressed);
    }

    //////////////////////////////// INGAME ///////////////////////////////////////////////
    //////////////////////////////// INGAME ///////////////////////////////////////////////
    //////////////////////////////// INGAME ///////////////////////////////////////////////
    //////////////////////////////// INGAME ///////////////////////////////////////////////
    //////////////////////////////// INGAME ///////////////////////////////////////////////
    //////////////////////////////// INGAME ///////////////////////////////////////////////
    //////////////////////////////// INGAME ///////////////////////////////////////////////

    private void InteractiveJustPressed(InputActionEventData data)
    {
        InputEvent.Invoke_InteractiveJustPressed();
    }

    private void InteractiveJustReleased(InputActionEventData data)
    {
        InputEvent.Invoke_InteractiveJustReleased();
    }

    void EnergyReloadStart(InputActionEventData data)
    {
        InputEvent.Invoke_EnergyReload();
    }

    void SheildReloadStart(InputActionEventData data)
    {
        InputEvent.Invoke_SheildReload();
    }

    public void UPPressed(InputActionEventData data)
    {
        InputEvent.Invoke_UpPressed();
    }

    public void DownPressed(InputActionEventData data)
    {
        InputEvent.Invoke_DownPressed();
    }

    public void RightPressed(InputActionEventData data)
    {
        InputEvent.Invoke_RightPressed();
    }

    public void LeftPressed(InputActionEventData data)
    {
        InputEvent.Invoke_LeftPressed();
    }
    // keep presseing

    public void UPJustPressed(InputActionEventData data)
    {
        InputEvent.Invoke_UPJustPressed();
    }

    public void DownJustPressed(InputActionEventData data)
    {
        InputEvent.Invoke_DownJustPressed();
    }

    public void RightJustPressed(InputActionEventData data)
    {
        InputEvent.Invoke_RightJustPressed();
    }

    public void LeftJustPressed(InputActionEventData data)
    {
        InputEvent.Invoke_LeftJustPressed();
    }


    // just the time release the button
    public void UPJustReleased(InputActionEventData data)
    {
        InputEvent.Invoke_UPJustReleased();
    }

    public void DownJustReleased(InputActionEventData data)
    {        
        InputEvent.Invoke_DownJustReleased();
    }

    public void RightJustReleased(InputActionEventData data)
    {        
        InputEvent.Invoke_RightJustReleased();
    }

    public void LeftJustReleased(InputActionEventData data)
    {        
        InputEvent.Invoke_LeftJustReleased();
    }

    //////////////////////////////// INGAME ///////////////////////////////////////////////
    //////////////////////////////// INGAME ///////////////////////////////////////////////
    //////////////////////////////// INGAME ///////////////////////////////////////////////
    //////////////////////////////// INGAME ///////////////////////////////////////////////
    //////////////////////////////// INGAME ///////////////////////////////////////////////
    //////////////////////////////// INGAME ///////////////////////////////////////////////
    //////////////////////////////// INGAME ///////////////////////////////////////////////



    //////////////////////////////// UI ///////////////////////////////////////////////
    //////////////////////////////// UI ///////////////////////////////////////////////
    //////////////////////////////// UI ///////////////////////////////////////////////
    //////////////////////////////// UI ///////////////////////////////////////////////
    //////////////////////////////// UI ///////////////////////////////////////////////
    //////////////////////////////// UI ///////////////////////////////////////////////
    //////////////////////////////// UI ///////////////////////////////////////////////

    public void UIEnterPressed(InputActionEventData data)
    {
        InputEvent.Invoke_UIEnterPressed();
    }

    public void UIBackPressed(InputActionEventData data)
    {
        InputEvent.Invoke_UIBackPressed();
    }

    public void UIUpPressed(InputActionEventData data)
    {
        InputEvent.Invoke_UIUpPressed();
    }

    public void UIDownPressed(InputActionEventData data)
    {
        InputEvent.Invoke_UIDownPressed();
    }

    public void UIRightPressed(InputActionEventData data)
    {
        InputEvent.Invoke_UIRightPressed();
    }

    public void UILeftPressed(InputActionEventData data)
    {
        InputEvent.Invoke_UILeftPressed();
    }

    



    //////////////////////////////// UI ///////////////////////////////////////////////
    //////////////////////////////// UI ///////////////////////////////////////////////
    //////////////////////////////// UI ///////////////////////////////////////////////
    //////////////////////////////// UI ///////////////////////////////////////////////
    //////////////////////////////// UI ///////////////////////////////////////////////
    //////////////////////////////// UI ///////////////////////////////////////////////
    //////////////////////////////// UI ///////////////////////////////////////////////
}
