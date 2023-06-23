using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

// ==================== code cleaned by making PlayerMoveLogic =====================
public class PlayerMove : MonoBehaviour
{
    private PlayerMoveLogic playerMoveLogic;

    [SerializeField] float moveSpeed = 5f;

    private void Awake() {
        playerMoveLogic = new PlayerMoveLogic(GetComponent<Transform>(), moveSpeed);
    }

    private void OnEnable() {
        playerMoveLogic.OnEnable();
    }

    private void OnDisable() {
        playerMoveLogic.OnDisable();
    }

    private void FixedUpdate() {
        playerMoveLogic.FixedUpdate();
    }

    public void SetPlayerMove(bool flag)
    {
        playerMoveLogic.canMove = flag;
    }

    public void WhenPauseReleased()
    {
        StartCoroutine(checkWalkMove());
    }

    IEnumerator checkWalkMove()
    {
        yield return new WaitForEndOfFrame(); // wait for input system to be changed
        yield return new WaitForEndOfFrame(); // wait for update will change value

        playerMoveLogic.WhenPauseReleased();
    }

}
