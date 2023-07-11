using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    public float upwardSpeed = 0.6f;
    public float downwardSpeed = 1.2f;
    public Vector2 upwardDirection = Vector2.up;

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player Body") )
        {
            Vector2 playerDirection = Player1.instance.playerMove.playerDirection;

            float angle = Vector2.Angle(upwardDirection, playerDirection);

            if (angle < 50f)
            {
                // 플레이어가 위쪽으로 움직이는 경우
                Player1.instance.playerMove.moveSpeedScholar = upwardSpeed;
            }
            else if(angle > 130f)
            {
                // 플레이어가 아래쪽으로 움직이는 경우
                Player1.instance.playerMove.moveSpeedScholar = downwardSpeed;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player Body") )
        {
            Player1.instance.playerMove.moveSpeedScholar = 1;
        }

    }
}


