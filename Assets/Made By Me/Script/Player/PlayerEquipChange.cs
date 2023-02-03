using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerEquipChange : MonoBehaviour
{
    private Player player;
    private PlayerStatus status;

    private void Awake() {
        player = ReInput.players.GetPlayer(0);
        player.AddInputEventDelegate(ChangeWeapon, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Weapon Change");
        player.AddInputEventDelegate(ChangeSheild, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Sheild Change");
        player.AddInputEventDelegate(EnergyRefill, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Energy Refill");
        player.AddInputEventDelegate(SheildRefill, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Sheild Refill");

        status = GetComponentInChildren<PlayerStatus>();
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
    void EnergyRefill(InputActionEventData data)
    {
        int temp = 0;
        temp = status.EnergyMaganizeMaximum[status.Energy];
        temp -= (int)status.EnergyAmount;
        status.EnergyChange(-temp);
    }
    void SheildRefill(InputActionEventData data)
    {
        int temp = 0;
        temp = status.SheildMaganizeMaximum[status.Sheild];
        temp -= (int)status.SheildDurability;
        status.SheildDurabilityChange(-temp);
    }
}
