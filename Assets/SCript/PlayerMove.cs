using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;

public class PlayerMove : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rb;

    [Header("changeable stat")]
    public float moveSpeed = 1f;


    [Header("Debug stat")]
    [SerializeField] private float SpeedScholar;
    [SerializeField] private float HorizontalInput;
    [SerializeField] private float HorizontalMoveSpeed;
    [SerializeField] private float VerticalInput;
    [SerializeField] private float VerticalMoveSpeed;

    private void Awake() {
        player = ReInput.players.GetPlayer(0);
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        SpeedScholar = Mathf.Clamp( 1 / Mathf.Sqrt(HorizontalInput * HorizontalInput + VerticalInput * VerticalInput) , 0 , 1) ;

        if((HorizontalInput = player.GetAxis("Move Horizontal")) != 0f)
        {
            HorizontalMoveSpeed =  SpeedScholar * HorizontalInput * moveSpeed * Time.fixedDeltaTime;
            rb.velocity = new Vector2( HorizontalMoveSpeed ,rb.velocity.y);
        }

        if((VerticalInput = player.GetAxis("Move Vertical")) != 0f)
        {
            VerticalMoveSpeed =  SpeedScholar * VerticalInput * moveSpeed * Time.fixedDeltaTime;
            rb.velocity = new Vector2( rb.velocity.x, VerticalMoveSpeed);
        }
    }
}
