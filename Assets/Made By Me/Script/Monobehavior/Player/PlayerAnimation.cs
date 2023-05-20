using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

// ================ code cleaned by making PlayerAnimationLogic ==============================

public class PlayerAnimation : MonoBehaviour
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
    }


    public Animator animator; // Player's animator
    private Rigidbody2D rb; // Player's rigidBody
    private PlayerStatus status; // Player's Status Script

    private Player player;

    public VFXAnimation vfxAnimation;
    private PlayerAnimationLogic animationLogic;

    
    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        status = GetComponentInChildren<PlayerStatus>();

        player = ReInput.players.GetPlayer(0);

        vfxAnimation = new VFXAnimation(GetComponentInChildren<VFX>().gameObject.GetComponent<Animator>());

        animationLogic = new PlayerAnimationLogic(player, animator, GameManager.instance);

        animationLogic.delegateInpuiFunctions();
    }

    private void OnDestroy() {
        animationLogic.delegateInpuiFunctions();
    }

    private void OnEnable() {
        PlayerStatus.SheildCrashEvent += animationLogic.SetSheildCrash;
        PlayerStatus.SheildRecoveryEvent += animationLogic.SetSheildRecovery;
    }

    private void OnDisable() {
        PlayerStatus.SheildCrashEvent -= animationLogic.SetSheildCrash;
        PlayerStatus.SheildRecoveryEvent -= animationLogic.SetSheildRecovery;
    }

    void Update()
    {
        GameManager.instance.isPlayerSheilding = animationLogic.isSheilding;
        animationLogic.isWalkingPress = player.GetButton("Move Up") || player.GetButton("Move Down") || player.GetButton("Move Right") || player.GetButton("Move Left");
        if(GameManager.instance.isPlayerPaused) return;
        
        animationLogic.SetWalkAnimation();
        SetAttackAnimation();
        SetShieldAnimation();
        SetParryAnimation();
    }

    private void SetAttackAnimation()
    {
        if(status.EnergyMaganize[status.Energy] == 0) return;
        if(player.GetButtonDown("Attack"))
        {
            if(animationLogic.isAttacking) return;
            if(animationLogic.isSheilding) return;
            if(animationLogic.isParrying) return;

            animator.SetTrigger("Attack");
            animationLogic.isAttacking = true;
            GameManager.instance.SetPlayerMove(false);
            animator.SetFloat("Energy", status.Energy);
            status.EnergyUse(1, status.Energy);
        }
    }

    private void SetShieldAnimation()
    {
        if(animationLogic.isParrying) return;

        if(animationLogic.sheildCrash) return;

        if(player.GetButtonUp("Shield"))
        {
            if(status.isBlocked) return;
            GameManager.instance.SetPlayerMove(true);
        }

        if(player.GetButton("Shield"))
        {
            GameManager.instance.SetPlayerMove(false);
            animator.SetFloat("Sheild Kind", status.Sheild);
        }

        animationLogic.isSheilding = player.GetButton("Shield");
        animator.SetBool("Shield", animationLogic.isSheilding);
    }

    private void SetParryAnimation()
    {
        if(animationLogic.isParrying || animationLogic.sheildCrash) return;

        if(player.GetButton("Shield") && player.GetButtonDown("Interactive") && status.Sheild != 2)
        {
            animator.SetTrigger("Parry");
            animationLogic.isParrying = true;
        }
    }

    public void ResetPlayerAnimationState()
    {
        animationLogic.ResetPlayerAnimationState();
        status.parryFrame = false;
    }

    public void ResetPlayerAnimationState_CalledByGameManager()
    {
        animationLogic.ResetPlayerAnimationState_CalledByGameManager();
    }


    public void SetAnimationFlag(string type ,string flag, float value = 0)
    {
        if(type == "Trigger")
        {
            animator.SetTrigger(flag);
        }else if(type == "Float")
        {
            animator.SetFloat(flag, value);
        }else if(type == "Int")
        {
            animator.SetInteger(flag, (int)value);
        }else if(type == "Bool")
        {
            if(value == 0)
            {
                animator.SetBool(flag, false);
            }else
            {
                animator.SetBool(flag, true);
            }
        }
    }

    public void SetPausePlayer(bool flag)
    {
        if(flag)
        {
            animationLogic.BeforePauseXInput = animationLogic.LastXInput;
            animationLogic.BeforePauseYInput = animationLogic.LastYInput;
            animator.SetFloat("XInput", 0);
            animationLogic.YInput = 0;
            animator.SetFloat("YInput", 0);
        }else
        {
            StartCoroutine(TwoFrameSkip());
        }
    }

    IEnumerator TwoFrameSkip()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        if(animationLogic.isWalkingPress)
        {
            animationLogic.XInput = animationLogic.LastXInput;
            animator.SetFloat("XInput", animationLogic.XInput);
            animationLogic.YInput = animationLogic.LastYInput;
            animator.SetFloat("YInput", animationLogic.YInput);
        }else
        {
            animationLogic.LastXInput = animationLogic.BeforePauseXInput;
            animationLogic.LastYInput = animationLogic.BeforePauseYInput;
            animator.SetFloat("LastXInput", animationLogic.BeforePauseXInput);
            animator.SetFloat("LastYInput", animationLogic.BeforePauseYInput);
        }
    }

    //Animation event
    public void SlashStart()
    {
        animationLogic.SlashStart();
    }

    public void SlashEnd()
    {
        animationLogic.SlashEnd();
    }

    public void StabStart()
    {
        animationLogic.StabStart();
    }

    public void StabEnd() 
    {
        animationLogic.StabEnd();
    }

    //Animation event

}