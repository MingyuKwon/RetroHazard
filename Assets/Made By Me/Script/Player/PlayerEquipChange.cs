using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerEquipChange : MonoBehaviour
{
    private Player player;
    private PlayerStatus status;

    private Animator animator;

    private void Awake() {
        player = ReInput.players.GetPlayer(0);
        player.AddInputEventDelegate(ChangeWeapon, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Weapon Change");
        player.AddInputEventDelegate(ChangeSheild, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Sheild Change");
        player.AddInputEventDelegate(EnergyReloadStart, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Energy Refill");
        player.AddInputEventDelegate(SheildReloadStart, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Sheild Refill");

        status = GetComponentInChildren<PlayerStatus>();
        animator = GetComponent<Animator>();
    }

    void ChangeWeapon(InputActionEventData data)
    {
        status.Energy++;
        if(status.Energy > 3)
        {
            status.Energy = 0;
        }

        status.Attack = status.EnergyDamage[status.Energy];
        status.EnergyAmount = status.EnergyMaganize[status.Energy];
        status.EnergyChange(0);
    }

    void ChangeSheild(InputActionEventData data)
    {
        status.Sheild++;
        if(status.Sheild > 2)
        {
            status.Sheild = 0;
        }

        status.SheildDurability = status.SheildMaganize[status.Sheild];
        status.SheildDurabilityChange(0);
    }
    void EnergyReLoad()
    {
        
        float temp = 0;
        temp = status.EnergyMaganizeMaximum[status.Energy];
        temp -= status.EnergyAmount;

        if(temp > status.EnergyStore[status.Energy])
        {
            temp = status.EnergyStore[status.Energy];
        }
        status.EnergyStore[status.Energy] -= temp;
        status.EnergyChange(-temp);
    }
    void SheildReLoad()
    {
        float temp = 0;
        temp = status.SheildMaganizeMaximum;
        temp -= status.SheildDurability;

        if(temp > status.SheildStore)
        {
            temp = status.SheildStore;
        }
        status.SheildStore -= temp;
        status.SheildDurabilityChange(-temp);
    }

    void EnergyReloadStart(InputActionEventData data)
    {
        if(status.EnergyStore[status.Energy] == 0) return;
        if(status.EnergyAmount == status.EnergyMaganizeMaximum[status.Energy]) return;
        GameManager.instance.SetPausePlayer(true);
        animator.SetTrigger("Reload");
        animator.SetFloat("ReloadEnergy", 1f);
        GameManager.instance.SetPlayerMove(false);
    }

    void SheildReloadStart(InputActionEventData data)
    {
        if(status.SheildStore == 0) return;
        if(status.SheildDurability == status.SheildMaganizeMaximum) return;
        GameManager.instance.SetPausePlayer(true);
        animator.SetTrigger("Reload");
        animator.SetFloat("ReloadEnergy", 0f);
        GameManager.instance.SetPlayerMove(false);
    }

    public void EnergyReloadEnd()
    {
        EnergyReLoad();
        GameManager.instance.SetPlayerFree();
        GameManager.instance.SetPlayerMove(true);
        GameManager.instance.SetPausePlayer(false);
    }

    public void SheildReloadEnd()
    {
        SheildReLoad();
        GameManager.instance.SetPlayerFree();
        GameManager.instance.SetPlayerMove(true);
        GameManager.instance.SetPausePlayer(false);
    }

    
}
