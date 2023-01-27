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
        player.AddInputEventDelegate(ChangeEnergy, UpdateLoopType.Update, InputActionEventType.ButtonJustPressed, "Energy Change");

        status = GetComponentInChildren<PlayerStatus>();
    }

    void ChangeWeapon(InputActionEventData data)
    {
        if(status.Weapon % 10 == 0)
        {
            status.Weapon = 101;
        }else
        {
            status.Weapon = 100;
        }
    }

    void ChangeEnergy(InputActionEventData data)
    {

    }
}
