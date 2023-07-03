using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public enum InputType
{
    FieldInput = 0,
    InteractiveUIInput = 1,
    TabUIInput = 2,
    BoxUIInput = 3,
    DialogtUIInput = 4,
    MenuUIInput = 5,
    SaveSlotUIInput = 6,
    OptionUIInput = 7,
    AlertUIInput = 8,
    NoticeUIInput = 9,
}

public class GameMangerInput : MonoBehaviour
{
    public class InputEvent
    {
        static bool currentInput(InputType type)
        {
            bool flag = false;

            if(GameMangerInput.inputControlStack.Count == 0)
            {
                return flag;
            }

            if(GameMangerInput.inputControlStack.Peek() == type)
            {
                flag = true;
            }

            return flag;
        }

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

        public static event Action NoticeCloseEvent;
        public static void Invoke_NoticeCloseEvent()
        {
            NoticeCloseEvent.Invoke();
        }

        public static event Action TabUIEnterPressed;
        public static event Action InteractiveUIEnterPressed;
        public static event Action DialogUIEnterPressed;
        public static event Action BoxUIEnterPressed;
        public static event Action MenuUIEnterPressed;
        public static event Action AlertUIEnterPressed;
        public static void Invoke_UIEnterPressed()
        {
            if(currentInput(InputType.TabUIInput))
            {
                TabUIEnterPressed.Invoke();
            }else if(currentInput(InputType.InteractiveUIInput))
            {
                InteractiveUIEnterPressed.Invoke();
            }else if(currentInput(InputType.DialogtUIInput))
            {
                DialogUIEnterPressed.Invoke();
            }else if(currentInput(InputType.BoxUIInput))
            {
                BoxUIEnterPressed.Invoke();
            }else if(currentInput(InputType.MenuUIInput))
            {
                MenuUIEnterPressed.Invoke();
            }else if(currentInput(InputType.AlertUIInput))
            {
                AlertUIEnterPressed.Invoke();
            }
        }

        public static event Action TabUIBackPressed;
        public static event Action BoxUIBackPressed;
        public static event Action MenuUIBackPressed;
        public static event Action AlertUIBackPressed;
        public static void Invoke_UIBackPressed()
        {
            if(currentInput(InputType.TabUIInput))
            {
                TabUIBackPressed.Invoke();
            }else if(currentInput(InputType.BoxUIInput))
            {
                BoxUIBackPressed.Invoke();
            }else if(currentInput(InputType.MenuUIInput))
            {
                MenuUIBackPressed.Invoke();
            }else if(currentInput(InputType.AlertUIInput))
            {
                AlertUIBackPressed.Invoke();
            }
        }

        public static event Action TabUIUpPressed;
        public static event Action DialogUIUpPressed;
        public static event Action BoxUIUpPressed;
        public static event Action MenuUIUpPressed;
        public static void Invoke_UIUpPressed()
        {
            if(currentInput(InputType.TabUIInput))
            {
                TabUIUpPressed.Invoke();
            }else if(currentInput(InputType.DialogtUIInput))
            {
                DialogUIUpPressed.Invoke();
            }else if(currentInput(InputType.BoxUIInput))
            {
                BoxUIUpPressed.Invoke();
            }else if(currentInput(InputType.MenuUIInput))
            {
                MenuUIUpPressed.Invoke();
            }
        }

        public static event Action TabUIDownPressed;
        public static event Action DialogUIDownPressed;
        public static event Action BoxUIDownPressed;
        public static event Action MenuUIDownPressed;
        public static void Invoke_UIDownPressed()
        {
            if(currentInput(InputType.TabUIInput))
            {
                TabUIDownPressed.Invoke();
            }else if(currentInput(InputType.DialogtUIInput))
            {
                DialogUIDownPressed.Invoke();
            }else if(currentInput(InputType.BoxUIInput))
            {
                BoxUIDownPressed.Invoke();
            }else if(currentInput(InputType.MenuUIInput))
            {
                MenuUIDownPressed.Invoke();
            }
        }

        public static event Action TabUIRightPressed;
        public static event Action DialogUIRightPressed;
        public static event Action BoxUIRightPressed;
        public static event Action MenuUIRightPressed;
        public static event Action AlertUIRightPressed;
        public static void Invoke_UIRightPressed()
        {
            if(currentInput(InputType.TabUIInput))
            {
                TabUIRightPressed.Invoke();
            }else if(currentInput(InputType.DialogtUIInput))
            {
                DialogUIRightPressed.Invoke();
            }else if(currentInput(InputType.BoxUIInput))
            {
                BoxUIRightPressed.Invoke();
            }else if(currentInput(InputType.MenuUIInput))
            {
                MenuUIRightPressed.Invoke();
            }else if(currentInput(InputType.AlertUIInput))
            {
                AlertUIRightPressed.Invoke();
            }
        }

        public static event Action TabUILeftPressed;
        public static event Action DialogUILeftPressed;
        public static event Action BoxUILeftPressed;
        public static event Action MenuUILeftPressed;
        public static event Action AlertUILeftPressed;
        public static void Invoke_UILeftPressed()
        {
            if(currentInput(InputType.TabUIInput))
            {
                TabUILeftPressed.Invoke();
            }else if(currentInput(InputType.DialogtUIInput))
            {
                DialogUILeftPressed.Invoke();
            }else if(currentInput(InputType.BoxUIInput))
            {
                BoxUILeftPressed.Invoke();
            }else if(currentInput(InputType.MenuUIInput))
            {
                MenuUILeftPressed.Invoke();
            }else if(currentInput(InputType.AlertUIInput))
            {
                AlertUILeftPressed.Invoke();
            }
        }

        public static event Action TabUILeftTabPressed;
        public static event Action BoxUILeftTabPressed;
        public static void Invoke_UILeftTabPressed()
        {
            if(currentInput(InputType.TabUIInput))
            {
                TabUILeftTabPressed.Invoke();
            }else if(currentInput(InputType.BoxUIInput))
            {
                BoxUILeftTabPressed.Invoke();
            }
        }

        public static event Action TabUIRightTabPressed;
        public static event Action BoxUIRightTabPressed;
        public static void Invoke_UIRightTabPressed()
        {
            if(currentInput(InputType.TabUIInput))
            {
                TabUIRightTabPressed.Invoke();
            }else if(currentInput(InputType.BoxUIInput))
            {
                BoxUIRightTabPressed.Invoke();
            }
        }

        public static event Action RemoveSaveSlot;
        public static void Invoke_RemoveSaveSlot()
        {
            if(currentInput(InputType.MenuUIInput))
            {
                RemoveSaveSlot.Invoke();
            }
        }


    }

    public class InputCheck
    {
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

    public static bool mouseCliking = false;

    public static Stack<InputType> inputControlStack = new Stack<InputType>();

    public static void getInput(InputType type)
    {
        if(inputControlStack.Count != 0 && inputControlStack.Peek() == type)
        {
            return;
        }

        inputControlStack.Push(type);
        changePlayerInputRule();
    }

    public static void releaseInput(InputType type)
    {
        if(inputControlStack.Count == 0)
        {
            return;
        }

        if(inputControlStack.Peek() != type) // 만약 요청 했는데 최 상단이 아니라면 밑에 깔려 있는것
                                                // 주로 초기 실행에서 마구잡이로 요청이 들어오고 나오는 과정에서 쓰임
        {
            Stack<InputType> stack = new Stack<InputType>();
            bool flag = false;
            while(inputControlStack.Count != 0 )
            {
                InputType temp = inputControlStack.Pop();
                if(temp == type)
                {
                    flag = true;
                    break;
                }
                stack.Push(temp);
            }

            if(!flag)
            {
                Debug.Log("Your Requset : " + type + " does not exist in Stack");
            }

            while(stack.Count != 0 )
            {
                inputControlStack.Push(stack.Pop());
            }

            Debug.Log("POP inside : " + type+ " Current Peek : " + inputControlStack.Peek());

            return;
        }

        inputControlStack.Pop();
        changePlayerInputRule();
    }

    private void Update() {
        CheckStack();
    }

    private void CheckStack() {
        String str = "Current Stack State : Count "+ inputControlStack.Count + " \n";

        Stack<InputType> stack = new Stack<InputType>();
            while(inputControlStack.Count != 0 )
            {
                InputType temp = inputControlStack.Pop();
                str += " " + temp + "\n";
                stack.Push(temp);
            }

            while(stack.Count != 0 )
            {
                inputControlStack.Push(stack.Pop());
            }

        Debug.Log(str);

    }

    public static void clearInputControlStack()
    {
        inputControlStack.Clear();
    }

    private Player player;
    private static ControllerMapEnabler mapEnabler;
    private static ControllerMapEnabler.RuleSet[] ruleSets;
    // 0 : normal State
    // 1 : talk with NPC State
    // 2 : UI
    // 3 : Alert

    public static int currentInputRule = 0;

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
        ruleSets[1] = mapEnabler.ruleSets.Find(x => x.tag == "TalkWithNPC"); // 이 부분이 그냥 엔터만 받아서 넘기는 부분 총괄
        ruleSets[2] = mapEnabler.ruleSets.Find(x => x.tag == "UI");
        ruleSets[3] = mapEnabler.ruleSets.Find(x => x.tag == "Alert");
        ruleSets[4] = mapEnabler.ruleSets.Find(x => x.tag == "Notice");
    }

    private void OnEnable() {
        delegateInputFunctions();
    }

    private void OnDisable() {
        removeInputFunctions();
    }

    private static void changePlayerInputRule()
    {
        if(inputControlStack.Count == 0)
        {
            return;
        }

        switch(inputControlStack.Peek())
        {
            case InputType.FieldInput :
                changePlayerInputRule(0);
                break;
            case InputType.InteractiveUIInput :
                changePlayerInputRule(1);
                break;
            case InputType.BoxUIInput :
                changePlayerInputRule(1);
                break;
            case InputType.DialogtUIInput :
                changePlayerInputRule(1);
                break;
            case InputType.TabUIInput :
                changePlayerInputRule(1);
                break;
            case InputType.MenuUIInput :
                changePlayerInputRule(2);
                break;
            case InputType.AlertUIInput :
                changePlayerInputRule(3);
                break;
            case InputType.NoticeUIInput :
                changePlayerInputRule(4);
                break;
        }
    }

    [Button]
    private static void changePlayerInputRule(int ruleNum)
    {
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


        
        player.AddInputEventDelegate(NoticeClose, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "NoticeClose");


        player.AddInputEventDelegate(UIEnterPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Enter");
        player.AddInputEventDelegate(UIBackPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Back");
        player.AddInputEventDelegate(UIUpPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectUp");
        player.AddInputEventDelegate(UIDownPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectDown");
        player.AddInputEventDelegate(UIRightPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectRight");
        player.AddInputEventDelegate(UILeftPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "SelectLeft");

        player.AddInputEventDelegate(RemoveSaveSlot, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Remove");


        player.AddInputEventDelegate(LeftTab, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Move to Left Tab");
        player.AddInputEventDelegate(RightTab, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Move to Right Tab");

        player.AddInputEventDelegate(UIEnterPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "MouseLeftButton");
        player.AddInputEventDelegate(UIMouseBackPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased, "MouseRightButton");
    
        player.AddInputEventDelegate(UIEnterPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "AlertLeftClick");
        player.AddInputEventDelegate(UIMouseBackPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "AlertRightClick");

        player.AddInputEventDelegate(UILeftPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "AlertLeftPressed");
        player.AddInputEventDelegate(UIRightPressed, UpdateLoopType.Update, InputActionEventType.ButtonJustReleased, "AlertRightPressed");
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

        player.RemoveInputEventDelegate(NoticeClose);


        player.RemoveInputEventDelegate(UIEnterPressed);
        player.RemoveInputEventDelegate(UIBackPressed);
        player.RemoveInputEventDelegate(UIMouseBackPressed);
        player.RemoveInputEventDelegate(UIUpPressed);
        player.RemoveInputEventDelegate(UIDownPressed);
        player.RemoveInputEventDelegate(UIRightPressed);
        player.RemoveInputEventDelegate(UILeftPressed);
    }

    private void NoticeClose(InputActionEventData data)
    {
        InputEvent.Invoke_NoticeCloseEvent();
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

    public void UIMouseBackPressed(InputActionEventData data)
    {
        Debug.Log("UIMouseBackPressed : mouseCliking " + mouseCliking);

        if(AlertUI.instance.gameObject.activeInHierarchy)
        {
            mouseCliking = true; // true가 입력 막는 flag인데, true를 설정 하는 놈이 alert 밖에 없다
            InputEvent.Invoke_UIBackPressed();
        }else
        {
            if(!mouseCliking)
            {   
                InputEvent.Invoke_UIBackPressed();
            }else
            {
                mouseCliking = false;
            }
        }
        
         
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

    public void LeftTab(InputActionEventData data)
    {        
        InputEvent.Invoke_UILeftTabPressed();
    }

    public void RightTab(InputActionEventData data)
    {        
        InputEvent.Invoke_UIRightTabPressed();
    }

    public void RemoveSaveSlot(InputActionEventData data)
    {        
        InputEvent.Invoke_RemoveSaveSlot();
    }


    //////////////////////////////// UI ///////////////////////////////////////////////
    //////////////////////////////// UI ///////////////////////////////////////////////
    //////////////////////////////// UI ///////////////////////////////////////////////
    //////////////////////////////// UI ///////////////////////////////////////////////
    //////////////////////////////// UI ///////////////////////////////////////////////
    //////////////////////////////// UI ///////////////////////////////////////////////
    //////////////////////////////// UI ///////////////////////////////////////////////
}
