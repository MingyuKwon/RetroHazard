using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class GameManager : MonoBehaviour, IFGameManager
{
    public class EventManager
    {
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


    }

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
    PlayerAnimation playerAnimation;
    private Player player;

    void Awake() {
        if(instance == null)
        {
            instance = this;
            eventInstance = new EventManager();
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
        }

        player = ReInput.players.GetPlayer(0);
        player.AddInputEventDelegate(SetPauseGameInput, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Pause");
        player.AddInputEventDelegate(SetTab, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Tab");
        playerMove = FindObjectOfType<PlayerMove>();
        playerAnimation = FindObjectOfType<PlayerAnimation>();
    }

    private void OnDestroy() {
        player.RemoveInputEventDelegate(SetPauseGameInput);
        player.RemoveInputEventDelegate(SetTab);
    }

    private void OnEnable() {
        PlayerShield.Sheild_Durability_Reduce_Start_Event += Set_Sheild_Durability_Reducing;
        PauseMainUI.GotoMainMenuEvent += DestroyMyself;
    }

    private void OnDisable() {
        PauseMainUI.GotoMainMenuEvent -= DestroyMyself;
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
        playerAnimation.animator.SetBool("Obtain Key Item", flag);
        ObtainKeyItem = flag;

    }

    public void ResetPlayerAnimationState()
    {
        playerAnimation.ResetPlayerAnimationState_CalledByGameManager();
    }

    public void SetPausePlayer(bool flag)
    {
        isPlayerPaused = flag;
        playerAnimation.SetPausePlayer(flag);
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
            SetPauseGame(false);
        }
        else if(Time.timeScale == 1f)
        {
            GameManagerUI.instance.isShowingESC = true;
            GameManagerUI.instance.Visualize_PauseMainUI(true);
            SetPauseGame(true);
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
