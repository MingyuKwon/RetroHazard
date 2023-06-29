using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class GameManager : MonoBehaviour
{

    ////// EventManager //////////////////////////////////////////////////////////////////////////////////
    ////// EventManager //////////////////////////////////////////////////////////////////////////////////
    ////// EventManager //////////////////////////////////////////////////////////////////////////////////

    public class EventManager
    {
        public static event Action<string, int, int> showNotice;
        public static void InvokeShowNotice(string text = null, int panelWidth = 400, int panelHeight = 100)
        {
            // 아무 인수 없이 호출시에, notice를 닫는 것
            showNotice?.Invoke(text, panelWidth, panelHeight);
        }

        public static event Action<float,float, int, float, float , int, float, float, int, int> Update_IngameUI_Event; 
         //Max HP, Current Hp, Energy, EnergyMaganize[Energy], EnergtStore[Energy] , Sheild, SheildMaganize[Sheild] , SheildStore , energyUpgrade
        public static void Invoke_Update_IngameUI_Event(float MaxHP, float CurrentHP, int Energy, float EnergyMaganize, float EnergyStore , int Sheild, float SheildMaganize , float SheildStore, int EnergyUpgrade, int SheildUpgrade)
        {
            Update_IngameUI_Event?.Invoke(MaxHP, CurrentHP, Energy, EnergyMaganize, EnergyStore ,  Sheild, SheildMaganize , SheildStore, EnergyUpgrade, SheildUpgrade);
        }

        public static event Action InteractEvent;
        public static void InvokeInteractEvent()
        {
            InteractEvent?.Invoke();
        }

        public static event Action ItemUseEvent;
        public static void InvokeItemUseEvent()
        {
            ItemUseEvent?.Invoke();
        }

        public static event Action Sheild_Durability_Reduce_Start_Event;
        public static void Invoke_Sheild_Durability_Reduce_Start_Event()
        {
            Sheild_Durability_Reduce_Start_Event?.Invoke();
        }

        public static event Action<float, int> Sheild_Durability_Change_Event;
        public static void Invoke_Sheild_Durability_Change_Event(float SheildMaganize, int Sheild)
        {
            Sheild_Durability_Change_Event?.Invoke(SheildMaganize, Sheild );
        }

        public static event Action<bool> SheildCrashEvent;
        public static void Invoke_SheildCrashEvent(bool flag)
        {
            SheildCrashEvent?.Invoke(flag);
        }

        public static event Action<bool> SheildRecoveryEvent;
        public static void Invoke_SheildRecoveryEvent(bool flag)
        {
            SheildRecoveryEvent?.Invoke(flag);
        }

        public static event Action Recovery_VFX_Start_Event;
        public static void Invoke_Recovery_VFX_Start_Event()
        {
            Recovery_VFX_Start_Event?.Invoke();
        }

        public static event Action<ItemInformation, int> Obtain_bullet_Item_Event;
        public static void Invoke_Obtain_bullet_Item_Event(ItemInformation information, int amount)
        {
            Obtain_bullet_Item_Event?.Invoke(information, amount);
        }

        public static event Action<ItemInformation> Obtain_Expansion_Item_Event;
        public static void Invoke_Obtain_Expansion_Item_Event(ItemInformation information)
        {
            Obtain_Expansion_Item_Event?.Invoke(information);
        }

        public static event Action<ItemInformation> Obtain_RealKey_Item_Event;
        public static void Invoke_Obtain_RealKey_Item_Event(ItemInformation information)
        {
            Obtain_RealKey_Item_Event?.Invoke(information);
        }

        public static event Action<ItemInformation, int> Obtain_Equip_Item_Event;
        public static void Invoke_Obtain_Equip_Item_Event(ItemInformation information, int KeyItemCode)
        {
            Obtain_Equip_Item_Event?.Invoke(information, KeyItemCode);
        }

        public static event Action<ItemInformation> Obtain_potion_Item_Event;
        public static void Invoke_Obtain_potion_Item_Event(ItemInformation information)
        {
            Obtain_potion_Item_Event?.Invoke(information);
        }

        static public event Action<int> discardItemEvent;
        public static void Invoke_discardItemEvent(int discardTargetItemIndex)
        {
            discardItemEvent?.Invoke(discardTargetItemIndex);
        }

        // Combine start Item, combine start index, select Item, selected index
        static public event Action<ItemInformation , int , ItemInformation, int> CombineEvent; 
        public static void Invoke_CombineEvent(ItemInformation startItem, int startIndex, ItemInformation selectItem, int selectedIndex)
        {
            CombineEvent?.Invoke(startItem, startIndex, selectItem , selectedIndex);
        }

        // success dialog and delete item from inventory
        static public event Action<InteractiveDialog, int> Interact_KeyItem_Success_Event;
        public static void Invoke_Interact_KeyItem_Success_Event(InteractiveDialog dialog, int currentIndex)
        {
            Interact_KeyItem_Success_Event?.Invoke(dialog, currentIndex);
        }

        static public event Action<int, float> UsePotionEvent;
        public static void Invoke_UsePotionEvent(int currentIndex, float healAmount)
        {
            UsePotionEvent?.Invoke(currentIndex, healAmount);
        }

        public static event Action inventoryExpandEvent;
        public static void Invoke_inventoryExpandEvent()
        {
            inventoryExpandEvent?.Invoke();
        }

        // true : inventory -> box, false : box -> inventory
        static public event Action<bool ,ItemInformation , int, int> BoxEvent; 
        public static void Invoke_BoxEvent(bool flag, ItemInformation from, int fromAmount, int fromIndex)
        {
            BoxEvent?.Invoke(flag, from, fromAmount, fromIndex);
        }

        public static event Action PlayerDeathEvent;
        public static void Invoke_PlayerDeathEvent()
        {
            PlayerDeathEvent?.Invoke();
        }

        



        public static event Action CloseGame_GotoMainMenuEvent;

        public static void Invoke_CloseGame_GotoMainMenuEvent()
        {
            CloseGame_GotoMainMenuEvent?.Invoke();
        }


        public static event Action PauseWindowLayer_Change_Event;
        public static void Invoke_PauseWindowLayer_Change_Event()
        {
            PauseWindowLayer_Change_Event?.Invoke();
        }

        public static event Action MainMenuWindowLayer_Change_Event;
        public static void Invoke_MainMenuWindowLayer_Change_Event()
        {
            MainMenuWindowLayer_Change_Event?.Invoke();
        }
    }
    ////// EventManager //////////////////////////////////////////////////////////////////////////////////
    ////// EventManager //////////////////////////////////////////////////////////////////////////////////
    ////// EventManager //////////////////////////////////////////////////////////////////////////////////





    [Header("Flag field")]
    static public bool isPlayerNearNPC = false;
    static public bool isPlayerParry = false;
    static public bool isPlayerSheilding = false;
    static public bool Sheild_Durability_Reducing = false;
    static public bool isPlayerPaused = false; // Every player script refer to this value 
    static public bool isGamePaused = false; // Every player script refer to this value 
    static public bool ObtainKeyItem = false;

    public static GameManager instance = null;
    public static EventManager eventInstance = null;

    [Header("Difficulty")]
    static public int DEFCON = 0;

    [Header("Slow Motion")]
    static public bool isSlowMotion = false;

    static public float defalutSlowScale = 0.3f;
    static public float defaultSlowMotionTime = 0.8f;

    static public float slowMotionTimer = 0f;
    static public float slowMotionTime = 0.8f;

    PlayerMove playerMove;
    private Player player;

    void Awake() {
        if(instance == null)
        {
            instance = this;
            //eventInstance = new EventManager();
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
        }

        player = ReInput.players.GetPlayer(0);
        player.AddInputEventDelegate(SetPauseGameInput, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Pause");
        player.AddInputEventDelegate(SetTab, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Tab");
        playerMove = FindObjectOfType<PlayerMove>();
    }

    private void OnDestroy() {
        player.RemoveInputEventDelegate(SetPauseGameInput);
        player.RemoveInputEventDelegate(SetTab);
    }

    private void OnEnable() {
        GameMangerInput.getInput(InputType.FieldInput);
        EventManager.Sheild_Durability_Reduce_Start_Event += Set_Sheild_Durability_Reducing;
        GameManager.EventManager.CloseGame_GotoMainMenuEvent += DestroyMyself;
    }

    private void OnDisable() {
        GameMangerInput.releaseInput(InputType.FieldInput);
        GameManager.EventManager.CloseGame_GotoMainMenuEvent -= DestroyMyself;
    }

    private void DestroyMyself()
    {
        Destroy(this.gameObject);
    }

    private void Set_Sheild_Durability_Reducing()
    {
        Sheild_Durability_Reducing = true;
    }

    private void Update() {

        if(isSlowMotion)
        {
            if(slowMotionTimer < slowMotionTime)
            {
                slowMotionTimer += (1f / slowMotionTime) * Time.unscaledDeltaTime;
            }
            else
            {
                Time.timeScale = 1f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
                isSlowMotion = false;
                isPlayerParry = false;
            }
        }else
        {
            slowMotionTimer = 0f;
        }
         
    }

    // dont move position while doing another action
    public void SetPlayerMove(bool flag)
    {
        playerMove.SetPlayerMove(flag);
    }


    public void SetPlayerAnimationObtainKeyItem(bool flag)
    {
        Player1.instance.playerAnimation.SetAnimationFlag("Bool", "Obtain Key Item", flag ? 1 : 0);  
        ObtainKeyItem = flag;

    }

    public void ResetPlayerAnimationState()
    {
        Player1.instance.playerAnimation.ResetPlayerAnimationState_CalledByGameManager();
    }

    public void SetPausePlayer(bool flag)
    {
        isPlayerPaused = flag;
        if(flag)
        {
            Player1.instance.playerAnimation.ResetPlayerAnimationState_CalledByGameManager();
        }
        
    }

    //SlowMotion
    public void SlowMotion()
    {
        Time.timeScale = defalutSlowScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        slowMotionTime = defaultSlowMotionTime;
        isSlowMotion = true;
    }

    public void SlowMotion(float _slowMotionTime)
    {
        Time.timeScale = defalutSlowScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        slowMotionTime = _slowMotionTime;
        isSlowMotion = true;
    }
    //SlowMotion

    //Pause
    public void SetPauseGame(bool flag)
    {
        isGamePaused = flag;
        SetPausePlayer(flag);
        if(flag)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Player1.instance.playerAnimation.WhenPauseReleased();
            playerMove.WhenPauseReleased();
            Time.timeScale = 1f;
        }
        
    }

    private void SetPauseGameInput(InputActionEventData data)
    {
        if(ObtainKeyItem) return;
        if(isPlayerParry) return;

        if(Time.timeScale == 0f)
        {
            GameManagerUI.instance.isShowingESC = false;
            GameManagerUI.instance.Visualize_PauseMainUI(false);
        }
        else if(Time.timeScale == 1f)
        {
            GameManagerUI.instance.isShowingESC = true;
            GameManagerUI.instance.Visualize_PauseMainUI(true);
        }
        
    }

    public void SetPauseGameInput()
    {
        if(ObtainKeyItem) return;
        if(isPlayerParry) return;

        if(Time.timeScale == 0f)
        {
            GameManagerUI.instance.isShowingESC = false;
            GameManagerUI.instance.Visualize_PauseMainUI(false);
            SetPauseGame(false);
        }
        else if(Time.timeScale == 1f)
        {
            GameManagerUI.instance.isShowingESC = true;
            GameManagerUI.instance.Visualize_PauseMainUI(true);
            SetPauseGame(true);
        }
        
    }
    //Pause

    //Tab
    private void SetTab(InputActionEventData data)
    {
        if(ObtainKeyItem) return;
        if(isPlayerParry) return;

        if(GameManagerUI.instance.isInteractiveUIActive) return;
        if(GameManagerUI.instance.isShowingBox) return;

        if(GameManagerUI.instance.isShowingTab && GameManagerUI.instance.isShowingMenu)
        {
            GameManagerUI.instance.isShowingMenu = false;
            GameManagerUI.instance.Visualize_Tab_Menu(false);  
        }else if(!GameManagerUI.instance.isShowingTab)
        {
            GameManagerUI.instance.isShowingMenu = true;
            GameManagerUI.instance.Visualize_Tab_Menu(true);  
        }
         
    }
    //Tab
}
