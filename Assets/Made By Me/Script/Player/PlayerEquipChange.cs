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
    PlayerInventory inventory;
    private Animator animator;

    private void Awake() {
        player = ReInput.players.GetPlayer(0);
        player.AddInputEventDelegate(ChangeWeapon, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Weapon Change");
        player.AddInputEventDelegate(ChangeSheild, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Sheild Change");
        player.AddInputEventDelegate(EnergyReloadStart, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Energy Refill");
        player.AddInputEventDelegate(SheildReloadStart, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Sheild Refill");

        status = GetComponentInChildren<PlayerStatus>();
        animator = GetComponent<Animator>();
        inventory = GetComponentInChildren<PlayerInventory>();
    }

    void ChangeWeapon(InputActionEventData data)
    {
        status.Energy++;
        if(status.Energy > 3)
        {
            status.Energy = 0;
        }

        status.Attack = status.EnergyDamage[status.Energy];
        status.UpdateIngameUI();
    }

    void ChangeSheild(InputActionEventData data)
    {
        status.Sheild++;
        if(status.Sheild > 2)
        {
            status.Sheild = 0;
        }

        status.SheildDurabilityChange(0);
        animator.SetFloat("Sheild Kind", status.Sheild);
        status.UpdateIngameUI();
    }



    void EnergyReloadStart(InputActionEventData data)
    {
        if(status.EnergyStore[status.Energy] == 0) return;
        if(status.EnergyMaganize[status.Energy] == status.EnergyMaganizeMaximum[status.Energy]) return;
        GameManager.instance.SetPausePlayer(true);
        animator.SetTrigger("Reload");
        animator.SetFloat("ReloadEnergy", 1f);
        GameManager.instance.SetPlayerMove(false);
    }

    void SheildReloadStart(InputActionEventData data)
    {
        if(status.SheildStore == 0) return;
        if(status.SheildMaganize[status.Sheild] == status.SheildMaganizeMaximum) return;
        GameManager.instance.SetPausePlayer(true);
        animator.SetTrigger("Reload");
        animator.SetFloat("ReloadEnergy", 0f);
        GameManager.instance.SetPlayerMove(false);
    }

    public void EnergyReloadEnd()
    {
        inventory.EnergyReload();
        GameManager.instance.SetPlayerFree();
        GameManager.instance.SetPlayerMove(true);
        GameManager.instance.SetPausePlayer(false);
    }

    public void SheildReloadEnd()
    {
        inventory.SheildReLoad();
        GameManager.instance.SetPlayerFree();
        GameManager.instance.SetPlayerMove(true);
        GameManager.instance.SetPausePlayer(false);
    }

    
}
