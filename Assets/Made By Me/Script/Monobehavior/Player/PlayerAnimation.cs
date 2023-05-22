using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

// ================ code cleaned by making PlayerAnimationLogic ==============================

// 원래는 여기에서만 inpnut을 받을 생각이었기 때문에 animation과 상관 없는 flag들이 많다.
// 사실상 player에서 input을 받는 기능 + animation 기능 이렇게 설계가 되어있다 (잘못된 방법... 나중에 개선하자)

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

        public void StunAnimationStart()
        {
            vfxAnimator.SetTrigger("Stun");
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
        GameManager.isPlayerSheilding = animationLogic.isSheilding;
        animationLogic.isWalkingPress = player.GetButton("Move Up") || player.GetButton("Move Down") || player.GetButton("Move Right") || player.GetButton("Move Left");
        if(GameManager.isPlayerPaused) return;
        
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

    public void StunAnimationStart()
    {
        animationLogic.StunAnimationStart();
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

    public void WhenPauseReleased()
    {
        StartCoroutine(checkWalkAnimation());
    }

    IEnumerator checkWalkAnimation()
    {
        yield return new WaitForEndOfFrame(); // wait for input system to be changed
        yield return new WaitForEndOfFrame(); // wait for update will change value

        animationLogic.WhenPauseReleased();
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