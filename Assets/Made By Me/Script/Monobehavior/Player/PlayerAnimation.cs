using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

// ================ code cleaned by making PlayerAnimationLogic ==============================

// 원래는 여기에서만 inpnut을 받을 생각이었기 때문에 animation과 상관 없는 flag들이 많다.
// 사실상 player에서 input을 받는 기능 + animation 기능 이렇게 설계가 되어있다 (잘못된 방법... 나중에 개선하자)

public class PlayerAnimation : MonoBehaviour
{
    public PlayerAnimationLogic.VFXAnimation vfxAnimation{
        get{
            return animationLogic.vfxAnimation;
        }
    }
    private PlayerAnimationLogic animationLogic;

    
    private void Awake() {

        animationLogic = new PlayerAnimationLogic(this);
    }

    private void OnEnable() {
        animationLogic.OnEnable();
    }

    private void OnDisable() {
        animationLogic.OnDisable();
    }

    private void Start() {
        animationLogic.Start();
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

    public void SetAnimationFlag(string type ,string flag, float value = 0)
    {
        animationLogic.SetAnimationFlag(type, flag, value);
    }

    public void SetPlayerAnimationObtainKeyItem(bool flag)
    {
        animationLogic.SetPlayerAnimationObtainKeyItem(flag);
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

    public void StunStart()
    {
        animationLogic.StunStart();
    }

    public void StunEnd()
    {
        animationLogic.StunEnd();
    }

    public void ParryStart()
    {
        animationLogic.ParryStart();
    }

    public void ParryEnd()
    {
        animationLogic.ParryEnd();
    }

    public void ParryFrameStart()
    {
        animationLogic.ParryFrameStart();
    }

    public void ParryFrameEnd()
    {
       animationLogic.ParryFrameEnd();
    }


    public void BlockStart()
    {
        animationLogic.BlockStart();
    }

    public void BlockEnd()
    {
        animationLogic.BlockEnd();
    }

    public void EnergyReloadEnd()
    {
        animationLogic.EnergyReloadEnd();
    }

    public void SheildReloadEnd()
    {
        animationLogic.SheildReloadEnd();
    }


    public void SheildColliderEnable(bool flag)
    {
        animationLogic.SheildColliderEnable(flag);
    }
    public void AttackColliderEnable(bool flag)
    {
        animationLogic.AttackColliderEnable(flag);
    }

    //Animation event

}