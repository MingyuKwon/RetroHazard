using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerAnimationLogic
{
    public class VFXAnimation
    {
        public Animator vfxAnimator;

        public VFXAnimation(Animator _vfxAnimator)
        {
            vfxAnimator = _vfxAnimator;
        }

        public void SetAnimationFlag(string type ,string flag, float value = 0)
        {
            if(type == "Trigger")
            {
                vfxAnimator.SetTrigger(flag);
            }else if(type == "Float")
            {
                vfxAnimator.SetFloat(flag, value);
            }else if(type == "Int")
            {
                vfxAnimator.SetInteger(flag, (int)value);
            }else if(type == "Bool")
            {
                if(value == 0)
                {
                    vfxAnimator.SetBool(flag, false);
                }else
                {
                    vfxAnimator.SetBool(flag, true);
                }
            }
        }

        public void StunAnimationStart()
        {
            vfxAnimator.SetTrigger("Stun");
        }
    }

    PlayerAnimation monoBehaviour;

    BoxCollider2D sheildCollider;
    BoxCollider2D attckCollider;

    //flags and values///////////////////////////////////
    public bool sheildCrash; // is Sheild is broken and cannot use

    public bool isAttacking; // is Player is now doing Attack Animation
    public bool isSheilding; // is Player is now doing Sheild Animation
    public bool isParrying; // is Player is now doing Parrying Animation

    public bool isWalkingPress;

    public float LastXInput = 0f;
    public float LastYInput = -1f;

    public float XInput = 0f;
    public float YInput = 0f;

     public float BeforePauseXInput = 0f;
     public float BeforePauseYInput = 0f;
    //flags and values///////////////////////////////////


    /////////references and constructor///////////////////////////////////
    Animator animator;
    public VFXAnimation vfxAnimation;

    private PlayerStatus status;

    public PlayerAnimationLogic(PlayerAnimation monoBehaviour)
    {
        this.monoBehaviour = monoBehaviour;
        animator = monoBehaviour.GetComponent<Animator>();
        vfxAnimation = new VFXAnimation(monoBehaviour.GetComponentInChildren<VFX>().gameObject.GetComponent<Animator>());


        sheildCollider = monoBehaviour.transform.GetChild(0).GetChild(2).gameObject.GetComponent<BoxCollider2D>();
        attckCollider = monoBehaviour.transform.GetChild(0).GetChild(1).gameObject.GetComponent<BoxCollider2D>();
    }
    
    /////////references and constructor///////////////////////////////////

    //monobehavior function//////////////////////////////////////////////////////////////////
    public void Start() {
        status = Player1.instance.playerStatus;
        
    }

    public void Update()
    {
        GameManager.isPlayerSheilding = isSheilding;
        isWalkingPress = GameMangerInput.inputCheck.isPressingUP()|| GameMangerInput.inputCheck.isPressingDown() 
        || GameMangerInput.inputCheck.isPressingRight() || GameMangerInput.inputCheck.isPressingLeft();

        if(GameManager.isPlayerPaused) return;
        
        SetWalkAnimation();
        SetAttackAnimation();
        SetShieldAnimation();
        SetParryAnimation();
    }

     //monobehavior function//////////////////////////////////////////////////////////////////



    ////////////////logical functions///////////////////////////////////
    public bool isCurrentAnimationEnd()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f;
    }

    public void SetAnimationFlag(string type ,string flag, float value = 0)
    {
        if(type == "Float")
        {
            animator.SetFloat(flag, value);
        }else if(type == "Int")
        {
            animator.SetInteger(flag, (int)value);
        }
    }

    public void SetPlayerAnimationObtainKeyItem(bool flag)
    {
        if(flag)
        {
            animator.Play("chara_obtain_key_Item");
        }else
        {
            animator.Play("Idle");
        }
    }

    
    public void SetWalkAnimation()
    {
        animator.SetFloat("XInput", XInput);
        animator.SetFloat("YInput", YInput);
        animator.SetFloat("LastXInput", LastXInput);
        animator.SetFloat("LastYInput", LastYInput);

        if((!animator.GetCurrentAnimatorStateInfo(0).IsName("Walk") &&
        !animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))) return;

        

        if(InStageWarp.isInStageWarpingNow)
        {
            animator.Play("Walk");
        }else if(
            GameMangerInput.inputCheck.isPressingUP() ||
            GameMangerInput.inputCheck.isPressingDown() ||
            GameMangerInput.inputCheck.isPressingRight() ||
            GameMangerInput.inputCheck.isPressingLeft() )
        {
            animator.Play("Walk");
        }else
        {
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
            {
                animator.Play("Idle");
            }   
        }
    }

    private void SetAttackAnimation()
    {
        if(status.EnergyMaganize[status.Energy] == 0) return;
        if(GameMangerInput.inputCheck.isAttackButtonDown())
        {
            if(isAttacking) return;
            if(isSheilding) return;
            if(isParrying) return;

            animator.SetTrigger("Attack");
            switch(status.Energy)
            {
                case 0 :
                    GameAudioManager.instance.PlaySFXMusic(SFXAudioType.noramlAttack);
                    break;
                case 1 :
                    GameAudioManager.instance.PlaySFXMusic(SFXAudioType.Energy1Attack);
                    break;
                case 2 :
                    GameAudioManager.instance.PlaySFXMusic(SFXAudioType.Energy2Attack);
                    break;
                case 3 :
                    GameAudioManager.instance.PlaySFXMusic(SFXAudioType.Energy3Attack);
                    break;
            }
            isAttacking = true;
            GameManager.instance.SetPlayerMove(false);
            animator.SetFloat("Energy", status.Energy);
            status.EnergyUse(1, status.Energy);
        }
    }
    private void SetShieldAnimation()
    {
        if(isParrying) return;
        if(status.isBlocked) return;
        if(sheildCrash) return;

        if(isSheilding = GameMangerInput.inputCheck.isPressingShield())
        {
            GameManager.instance.SetPlayerMove(false);
            animator.SetFloat("Sheild Kind", status.Sheild);
            SheildColliderEnable(true);
            animator.Play("Shield");
        }else
        {
            PutSheildBack();
        }
    }

    private void PutSheildBack()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Shield") || 
        animator.GetCurrentAnimatorStateInfo(0).IsName("Parry") || 
        animator.GetCurrentAnimatorStateInfo(0).IsName("Block") || 
        animator.GetCurrentAnimatorStateInfo(0).IsName("Parry"))
        {
            GameManager.instance.SetPlayerMove(true);
            SheildColliderEnable(false);
            animator.Play("Idle");
        }
    }

    public void StunAnimationStart()
    {
        monoBehaviour.StopAllCoroutines();
        ResetPlayerAnimationState();
        
        monoBehaviour.StartCoroutine(StunStart());
    }

    IEnumerator StunStart()
    {
        if(GameManager.Sheild_Durability_Reducing)
        {
            status.SheildDurabilityChange(1);
        }

        GameManager.instance.SetPausePlayer(true);
        GameManager.instance.SetPlayerMove(false);
        GameManager.instance.EnemyCollideIgnore(true);
        SheildColliderEnable(false);
        vfxAnimation.StunAnimationStart();

        animator.Play("Stun");

        yield return new WaitForEndOfFrame();
        while(!isCurrentAnimationEnd())
        {
            yield return new WaitForEndOfFrame();
        }

        GameManager.instance.SetPausePlayer(false);
        GameManager.instance.SetPlayerMove(true);
        GameManager.instance.EnemyCollideIgnore(false);
        animator.Play("Idle");
        

    }

    public void SetSheildBlock()
    {
        if(status.isBlocked) return;
        monoBehaviour.StartCoroutine(BlockStart());
        
    }

    IEnumerator BlockStart()
    {
        status.isBlocked = true;
        GameAudioManager.instance.PlaySFXMusic(SFXAudioType.Block);
        GameManager.instance.SetPlayerMove(false);
        animator.Play("Block");

        yield return new WaitForEndOfFrame();
        while(!isCurrentAnimationEnd())
        {
            yield return new WaitForEndOfFrame();
        }

        status.SheildDurabilityChange(1);
        GameManager.instance.SetPlayerMove(true);
        status.isBlocked = false;
    }

    private void SetParryAnimation()
    {
        if(isParrying || sheildCrash) return;

        if(GameMangerInput.inputCheck.isPressingShield() && GameMangerInput.inputCheck.isInteractiveButtonDown() && status.Sheild != 2)
        {
            monoBehaviour.StartCoroutine(ParryStart());
        }
    }

    IEnumerator ParryStart()
    {
        isParrying = true;
        SheildCollidertoTrigger(true);

        if(!GameManager.Sheild_Durability_Reducing)
        {
            GameManager.EventManager.Invoke_Sheild_Durability_Reduce_Start_Event();
        }
        GameAudioManager.instance.PlaySFXMusic(SFXAudioType.Parry);

        GameManager.instance.SetPlayerMove(false);
        SheildColliderEnable(false);
        animator.Play("Parry");

        Player1.instance.playerRigidBody2D.velocity = Vector2.zero;

        yield return new WaitForEndOfFrame();
        while(!isCurrentAnimationEnd())
        {
            Player1.instance.playerRigidBody2D.velocity = Vector2.zero;
            yield return new WaitForEndOfFrame();
        }

        GameManager.instance.EnemyCollideIgnore(false);
        SheildCollidertoTrigger(false);
        ResetPlayerAnimationState();

    }


    public void ParryFrameStart()
    {
        status.parryFrame = true;
        SheildColliderEnable(true);
    }

    public void ParryFrameEnd()
    {
        SheildColliderEnable(false);
        status.parryFrame = false;

        if(!status.parrySuccess)
        {
            status.SheildDurabilityChange(1);
        }else
        {
            UI.instance.inGameUI.UpdateIngameUI();
            status.parrySuccess = false;
        }
    }

    public void SetSheildCrash(bool ChangeSheild)
    {
        sheildCrash = true;
        isSheilding = false;
        PutSheildBack();
    }

    public void SetSheildRecovery(bool ChangeSheild)
    {
        sheildCrash = false;
        GameManager.instance.SetPlayerMove(true);
    }

    void EnergyReloadStart()
    {
        if(status.EnergyStore[status.Energy] == 0) return;
        if(status.EnergyUpgrade[status.Energy] == 0) return;
        if(status.EnergyMaganize[status.Energy] == status.EnergyMaganizeMaximum[status.Energy]) return;

        SetAnimationFlag("Float","ReloadEnergy", 1f);
        monoBehaviour.StartCoroutine(ReloadStart());
    }

    void SheildReloadStart()
    {
        if(status.SheildStore == 0) return;
        if(status.SheildUpgrade[status.Sheild] == 0) return;
        if(status.SheildMaganize[status.Sheild] == status.SheildMaganizeMaximum[status.Sheild]) return;

        SetAnimationFlag("Float","ReloadEnergy", 0f);
        monoBehaviour.StartCoroutine(ReloadStart());
    }

    IEnumerator ReloadStart()
    {
        GameAudioManager.instance.PlaySFXMusic(SFXAudioType.reload);
        GameManager.instance.SetPausePlayer(true);
        GameManager.instance.SetPlayerMove(false);
        animator.Play("Reload");

        yield return new WaitForEndOfFrame();
        while(!isCurrentAnimationEnd())
        {
            yield return new WaitForEndOfFrame();
        }

        GameManager.instance.ResetPlayerAnimationState();
        GameManager.instance.SetPausePlayer(false);
        GameManager.instance.SetPlayerMove(true);
        animator.Play("Idle");
    }

    public void WhenPauseReleased()
    {
        if(!isWalkingPress)
        {
            XInput = 0;
            YInput = 0;
        }else
        {
            if(GameMangerInput.inputCheck.isPressingUP())
            {
                if(!(XInput == 0 && YInput == 1))
                {
                    XInput = 0;
                    YInput = 1;
                }
            }

            if(GameMangerInput.inputCheck.isPressingDown())
            {
                if(!(XInput == 0 && YInput == -1))
                {
                    XInput = 0;
                    YInput = -1;
                }
            }

            if(GameMangerInput.inputCheck.isPressingRight())
            {
                if(!(XInput == 1 && YInput == 0))
                {
                    XInput = 1;
                    YInput = 0;
                }
            }

            if(GameMangerInput.inputCheck.isPressingLeft())
            {
                if(!(XInput == -1 && YInput == 0))
                {
                    XInput = -1;
                    YInput = 0;
                }
            }

        }
    }


    public void ResetPlayerAnimationState() // called by animation
    {
        isAttacking = false;
        isParrying = false;
        isSheilding = false;
        SheildColliderEnable(false);
        SheildCollidertoTrigger(false);

        if(status == null) return;

        status.parryFrame = false;
        status.isBlocked = false;
    }


    public void SlashStart()
    {
        
    }

    public void SlashEnd()
    {
        isAttacking = false;
        GameManager.instance.SetPlayerMove(true);
    }

    public void StabStart()
    {
        
    }

    public void StabEnd() 
    {
        isAttacking = false;
        GameManager.instance.SetPlayerMove(true);
    }

    public void EnergyReloadEnd()
    {
        Player1.instance.playerInventory.EnergyReload();
    }

    public void SheildReloadEnd()
    {
        Player1.instance.playerInventory.SheildReLoad();
    }

    public void SheildColliderEnable(bool flag)
    {
        sheildCollider.enabled = flag;
    }
    public void SheildCollidertoTrigger(bool flag)
    {
        sheildCollider.isTrigger = flag;
    }

    public void AttackColliderEnable(bool flag)
    {
        attckCollider.enabled = flag;
    }

    ////////////////logical functions///////////////////////////////////


    //////////////////////delegate player input functions///////////////////////////////////
    public void OnEnable() {
        GameMangerInput.InputEvent.UPPressed += UPPressed;
        GameMangerInput.InputEvent.DownPressed += DownPressed;
        GameMangerInput.InputEvent.RightPressed += RightPressed;
        GameMangerInput.InputEvent.LeftPressed += LeftPressed;

        GameMangerInput.InputEvent.UPJustPressed += UPJustPressed;
        GameMangerInput.InputEvent.DownJustPressed += DownJustPressed;
        GameMangerInput.InputEvent.RightJustPressed += RightJustPressed;
        GameMangerInput.InputEvent.LeftJustPressed += LeftJustPressed;

        GameMangerInput.InputEvent.UPJustReleased += UPJustReleased;
        GameMangerInput.InputEvent.DownJustReleased += DownJustReleased;
        GameMangerInput.InputEvent.RightJustReleased += RightJustReleased;
        GameMangerInput.InputEvent.LeftJustReleased += LeftJustReleased;

        GameManager.EventManager.SheildCrashEvent += SetSheildCrash;
        GameManager.EventManager.SheildRecoveryEvent += SetSheildRecovery;

        GameMangerInput.InputEvent.EnergyReload += EnergyReloadStart;
        GameMangerInput.InputEvent.SheildReload += SheildReloadStart;
    }

    public void OnDisable() {
        GameMangerInput.InputEvent.UPPressed -= UPPressed;
        GameMangerInput.InputEvent.DownPressed -= DownPressed;
        GameMangerInput.InputEvent.RightPressed -= RightPressed;
        GameMangerInput.InputEvent.LeftPressed -= LeftPressed;

        GameMangerInput.InputEvent.UPJustPressed -= UPJustPressed;
        GameMangerInput.InputEvent.DownJustPressed -= DownJustPressed;
        GameMangerInput.InputEvent.RightJustPressed -= RightJustPressed;
        GameMangerInput.InputEvent.LeftJustPressed -= LeftJustPressed;

        GameMangerInput.InputEvent.UPJustReleased -= UPJustReleased;
        GameMangerInput.InputEvent.DownJustReleased -= DownJustReleased;
        GameMangerInput.InputEvent.RightJustReleased -= RightJustReleased;
        GameMangerInput.InputEvent.LeftJustReleased -= LeftJustReleased;

        GameManager.EventManager.SheildCrashEvent -= SetSheildCrash;
        GameManager.EventManager.SheildRecoveryEvent -= SetSheildRecovery;

        GameMangerInput.InputEvent.EnergyReload -= EnergyReloadStart;
        GameMangerInput.InputEvent.SheildReload -= SheildReloadStart;
    }

    //input Aniamtion Event
    // keep presseing
    void UPPressed()
    {
        if(isAttacking || isSheilding || isParrying ) return;
        LastXInput = 0f;
        LastYInput = 1f;
    }

    void DownPressed()
    {
        if(isAttacking || isSheilding || isParrying ) return;
        LastXInput = 0f;
        LastYInput = -1f;
    }

    void RightPressed()
    {
        if(isAttacking || isSheilding || isParrying ) return;
        LastXInput = 1f;
        LastYInput = 0f;
    }

    void LeftPressed()
    {
        if(isAttacking || isSheilding || isParrying ) return;
        LastXInput = -1f;
        LastYInput = 0f;
    }
    // keep presseing


    // just the time press the button
    void UPJustPressed()
    {
        XInput = 0f;
        YInput = 1f;
    }

    void DownJustPressed()
    {
        XInput = 0f;
        YInput = -1f;
    }

    void RightJustPressed()
    {
        XInput = 1f;
        YInput = 0f;
    }

    void LeftJustPressed()
    {
        XInput = -1f;
        YInput = 0f;
    }
    // just the time press the button

    // just the time release the button
    void UPJustReleased()
    {
        if(GameMangerInput.inputCheck.isPressingRight())
        {
            XInput = 1f;
            YInput = 0f;
        }else if(GameMangerInput.inputCheck.isPressingLeft())
        {
            XInput = -1f;
            YInput = 0f;
        }else if(GameMangerInput.inputCheck.isPressingDown())
        {
            XInput = 0f;
            YInput = -1f;
        }else
        {
            XInput = 0f;
            YInput = 0f;
        }
        
    }

    void DownJustReleased()
    {        
        if(GameMangerInput.inputCheck.isPressingRight())
        {
            XInput = 1f;
            YInput = 0f;
        }else if(GameMangerInput.inputCheck.isPressingLeft())
        {
            XInput = -1f;
            YInput = 0f;
        }else if(GameMangerInput.inputCheck.isPressingUP())
        {
            XInput = 0f;
            YInput = 1f;
        }else
        {
            XInput = 0f;
            YInput = 0f;
        }
    }

    void RightJustReleased()
    {        
        if(GameMangerInput.inputCheck.isPressingUP())
        {
            XInput = 0f;
            YInput = 1f;
        }else if(GameMangerInput.inputCheck.isPressingDown())
        {
            XInput = 0f;
            YInput = -1f;
        }else if(GameMangerInput.inputCheck.isPressingLeft())
        {
            XInput = -1f;
            YInput = 0f;
        }else
        {
            XInput = 0f;
            YInput = 0f;
        }
    }

    void LeftJustReleased()
    {        
        if(GameMangerInput.inputCheck.isPressingUP())
        {
            XInput = 0f;
            YInput = 1f;
        }else if(GameMangerInput.inputCheck.isPressingDown())
        {
            XInput = 0f;
            YInput = -1f;
        }else if(GameMangerInput.inputCheck.isPressingRight())
        {
            XInput = 1f;
            YInput = 0f;
        }else
        {
            XInput = 0f;
            YInput = 0f;
        }
    }
    
    // just the time release the button

    //input Aniamtion Event

    //////////////////////delegate player input functions///////////////////////////////////





    


    
}
