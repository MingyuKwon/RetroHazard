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
    private Player player;

    public VFXAnimation vfxAnimation;
    private PlayerAnimationLogic animationLogic;

    
    private void Awake() {
        animator = GetComponent<Animator>();

        player = ReInput.players.GetPlayer(0);

        vfxAnimation = new VFXAnimation(GetComponentInChildren<VFX>().gameObject.GetComponent<Animator>());

        animationLogic = new PlayerAnimationLogic(player, animator);

        animationLogic.delegateInpuiFunctions();
    }

    private void Start() {
        animationLogic.Start();
    }

    private void OnDestroy() {
        animationLogic.delegateInpuiFunctions();
    }

    private void OnEnable() {
        animationLogic.delegateFunctions();
    }

    private void OnDisable() {
        animationLogic.removeFunctions();
    }

    void Update()
    {
        animationLogic.Update();
    }

    public void StunAnimationStart()
    {
        animationLogic.StunAnimationStart();
    }

    public void ResetPlayerAnimationState()
    {
        animationLogic.ResetPlayerAnimationState();
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

    public void EnergyReloadEnd()
    {
        animationLogic.EnergyReloadEnd();
    }

    public void SheildReloadEnd()
    {
         animationLogic.SheildReloadEnd();
    }

    //Animation event

}