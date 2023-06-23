using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
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



    // keep presseing
    public void UPPressed()
    {
        playerMoveLogic.UPPressed();
    }

    public void DownPressed()
    {
        playerMoveLogic.DownPressed();
    }

    public void RightPressed()
    {
        playerMoveLogic.RightPressed();
    }

    public void LeftPressed()
    {
        playerMoveLogic.LeftPressed();
    }
    // keep presseing


    public void UPJustPressed()
    {
        playerMoveLogic.UPJustPressed();
    }

    public void DownJustPressed()
    {
        playerMoveLogic.DownJustPressed();
    }

    public void RightJustPressed()
    {
        playerMoveLogic.RightJustPressed();
    }

    public void LeftJustPressed()
    {
        playerMoveLogic.LeftJustPressed();
    }


    // just the time release the button
    public void UPJustReleased()
    {
        playerMoveLogic.UPJustReleased();
    }

    public void DownJustReleased()
    {        
        playerMoveLogic.DownJustReleased();
    }

    public void RightJustReleased()
    {        
        playerMoveLogic.RightJustReleased();
    }

    public void LeftJustReleased()
    {        
        playerMoveLogic.LeftJustReleased();
    }
    // just the time release the button
}
