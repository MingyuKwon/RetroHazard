using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    private Player player;

    [Header("changeable stat")]
    public float moveSpeed = 1f;
    public bool canMove = true;


    [Header("Debug stat")]
    [SerializeField] private float SpeedScholar;
    private float moveForceSchloar = 0f;

    [SerializeField] float HorizontalMoveSpeed;
    [SerializeField] float VerticalMoveSpeed;

    private void Awake() {
        player = ReInput.players.GetPlayer(0);
    }
    private void FixedUpdate() {

        if(canMove && !(GameManager.instance.isPlayerPaused))
        {
            if(player.GetButton("Move Up"))
            {
                VerticalMoveSpeed =  1f;
            }else if(player.GetButton("Move Down"))
            {
                VerticalMoveSpeed =  -1f;
            }else
            {
                VerticalMoveSpeed =  0f;
            }

            if(player.GetButton("Move Right"))
            {
                HorizontalMoveSpeed =  1f;
            }else if(player.GetButton("Move Left"))
            {
                HorizontalMoveSpeed =  -1f;
            }else
            {
                HorizontalMoveSpeed =  0f;
            }

            if(VerticalMoveSpeed != 0f && HorizontalMoveSpeed != 0f)
            {
                moveForceSchloar = 1 / Mathf.Sqrt(HorizontalMoveSpeed * HorizontalMoveSpeed + VerticalMoveSpeed * VerticalMoveSpeed);
            }else
            {
                moveForceSchloar = 1f;
            }
            
            VerticalMoveSpeed =  VerticalMoveSpeed * moveSpeed * Time.fixedDeltaTime * moveForceSchloar;
            HorizontalMoveSpeed = HorizontalMoveSpeed * moveSpeed * Time.fixedDeltaTime * moveForceSchloar;

            transform.position = new Vector2( transform.position.x + HorizontalMoveSpeed , transform.position.y + VerticalMoveSpeed);
        }
    }

}
