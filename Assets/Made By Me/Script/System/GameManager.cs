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
    public static GameManager instance = null;
    public static event Action GameOver;

    PlayerMove playerMove;
    PlayerAnimation playerAnimation;
    private Player player;

    [Header("Difficulty")]
    public int DEFCON = 0;

    [Header("Slow Motion")]
    public bool isSlowMotion = false;
    public const float defalutSlowScale = 0.3f;
    public float slowMotionTimer = 0f;
    public const float defaultSlowMotionTime = 0.8f;
    public float slowMotionTime = 0.8f;

    [Header("Flag field")]
    public bool isPlayerNearNPC = false;
    public bool isPlayerParry = false;
    public bool isPlayerSheilding = false;
    public bool Sheild_Durability_Reducing = false;
    public bool isPlayerPaused = false; // Every player script refer to this value 
    public bool isGamePaused = false; // Every player script refer to this value 

    public bool ObtainKeyItem = false;

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
        player.AddInputEventDelegate(SetPauseGameInput, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Pause");
        player.AddInputEventDelegate(SetTab, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Tab");
        playerMove = FindObjectOfType<PlayerMove>();
        playerAnimation = FindObjectOfType<PlayerAnimation>();
    }

    private void OnEnable() {
        PlayerShield.Sheild_Durability_Reduce_Start_Event += Set_Sheild_Durability_Reducing;
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
                GameManager.instance.isPlayerParry = false;
            }
        }else
        {
            slowMotionTimer = 0f;
        }
         
    }

    // dont move position while doing another action
    public void SetPlayerMove(bool flag)
    {
        playerMove.canMove = flag;
    }

    public void SetPlayerAnimationIdle()
    {
        playerAnimation.SetPlayerAnimationIdle();

    }

    public void SetPlayerAnimationObtainKeyItem(bool flag)
    {
        playerAnimation.animator.SetBool("Obtain Key Item", flag);
        ObtainKeyItem = flag;

    }

    public void SetPlayerFree()
    {
        playerAnimation.SetPlayerFree();
    }

    public void SetPausePlayer(bool flag)
    {
        isPlayerPaused = flag;
        if(flag)
        {
            playerAnimation.BeforePauseXInput = playerAnimation.LastXInput;
            playerAnimation.BeforePauseYInput = playerAnimation.LastYInput;
            playerAnimation.XInput = 0;
            playerAnimation.animator.SetFloat("XInput", 0);
            playerAnimation.YInput = 0;
            playerAnimation.animator.SetFloat("YInput", 0);
        }else
        {
            StartCoroutine(TwoFrameSkip());
        }
    }

    IEnumerator TwoFrameSkip()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        if(playerAnimation.isWalkingPress)
        {
            playerAnimation.XInput = playerAnimation.LastXInput;
            playerAnimation.animator.SetFloat("XInput", playerAnimation.XInput);
            playerAnimation.YInput = playerAnimation.LastYInput;
            playerAnimation.animator.SetFloat("YInput", playerAnimation.YInput);
        }else
        {
            playerAnimation.LastXInput = playerAnimation.BeforePauseXInput;
            playerAnimation.LastYInput = playerAnimation.BeforePauseYInput;
            playerAnimation.animator.SetFloat("LastXInput", playerAnimation.BeforePauseXInput);
            playerAnimation.animator.SetFloat("LastYInput", playerAnimation.BeforePauseYInput);
        }
    }

    //SlowMotion
    public void SlowMotion()
    {
        Time.timeScale = defalutSlowScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        this.slowMotionTime = defaultSlowMotionTime;
        isSlowMotion = true;
    }

    public void SlowMotion(float slowMotionTime)
    {
        Time.timeScale = defalutSlowScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        this.slowMotionTime = slowMotionTime;
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
            SetPauseGame(false);
        }
        else if(Time.timeScale == 1f)
        {
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
