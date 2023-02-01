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

        status = GetComponentInChildren<PlayerStatus>();
    }

    void ChangeWeapon(InputActionEventData data)
    {
        status.Energy++;
        if(status.Energy > 3)
        {
            status.Energy = 0;
        }
    }

    void ChangeSheild(InputActionEventData data)
    {
        status.Sheild++;
        if(status.Sheild > 2)
        {
            status.Sheild = 0;
        }
    }
}
