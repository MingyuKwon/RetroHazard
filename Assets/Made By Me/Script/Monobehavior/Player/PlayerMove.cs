using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

// ==================== code cleaned by making PlayerMoveLogic =====================
public class PlayerMove : MonoBehaviour
{
    private Player player;
    private PlayerMoveLogic playerMoveLogic;

    [SerializeField] float moveSpeed = 5f;

    private void Awake() {
        player = ReInput.players.GetPlayer(0);

        playerMoveLogic = new PlayerMoveLogic(player, GetComponent<Transform>(), moveSpeed);

        playerMoveLogic.delegateInpuiFunctions();

    }

    private void OnDestroy() {
        playerMoveLogic.removeInpuiFunctions();
    }

    private void FixedUpdate() {
        playerMoveLogic.FixedUpdate();
    }

    public void SetPlayerMove(bool flag)
    {
        playerMoveLogic.canMove = flag;
    }
}
