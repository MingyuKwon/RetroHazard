using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InStageWarp : MonoBehaviour
{
    GameObject downInteract;
    GameObject upInteract;

    bool isCurrentWarpActive = false;
    bool isDownActive = false;

    Vector2 colliderSize;

    private void Start() {
        colliderSize = GetComponent<BoxCollider2D>().size;

        downInteract = transform.GetChild(0).gameObject;
        upInteract = transform.GetChild(1).gameObject;

        downInteract.SetActive(false);
        upInteract.SetActive(false);
    }

    private void OnEnable() {
        GameManager.EventManager.InStageWarpEvent += DoWarp;
    }

    private void OnDisable() {
        GameManager.EventManager.InStageWarpEvent += DoWarp;
    }

    private void DoWarp()
    {
        if(!isCurrentWarpActive) return;

        GameMangerInput.blockAllInput = true;

        if(isDownActive)
        {
            Player1.instance.playerMove.transform.position = new Vector3(transform.position.x, 
            transform.position.y - colliderSize.y / 2, 
            transform.position.z); 
            StartCoroutine(moveDirect(Vector2.up));

        }else
        {
            Player1.instance.playerMove.transform.position = new Vector3(transform.position.x, 
            transform.position.y + colliderSize.y / 2, 
            transform.position.z); 

            StartCoroutine(moveDirect(Vector2.down));

        }
    }

    IEnumerator moveDirect(Vector2 moveDirection)
    {
        bool isGoingUp = false;

        if(moveDirection == Vector2.up)
        {
            isGoingUp = true;
        }else if(moveDirection == Vector2.down)
        {
            isGoingUp = false;
        }

        if(isGoingUp)
        {
            GameMangerInput.InputEvent.Invoke_UPJustPressed();
        }else
        {
            GameMangerInput.InputEvent.Invoke_DownJustPressed();
        }

        Player1.instance.playerSprite.sortingLayerName = "0";
        Player1.instance.playerSprite.sortingOrder = 2;
        Player1.instance.playerRigidBody2D.bodyType = RigidbodyType2D.Kinematic;

        Player1.instance.playerRigidBody2D.velocity = Vector2.zero;

        while(isCurrentWarpActive)
        {
            Player1.instance.playerMove.transform.Translate(moveDirection.normalized * Time.fixedDeltaTime * 2);
            yield return new WaitForFixedUpdate();
        }

        if(isGoingUp)
        {
            GameMangerInput.InputEvent.Invoke_UPJustReleased();
        }else
        {
            GameMangerInput.InputEvent.Invoke_DownJustReleased();
        }

            if(GameMangerInput.inputCheck.isPressingUP())
            {
                GameMangerInput.InputEvent.Invoke_UPJustPressed();

            }else if(GameMangerInput.inputCheck.isPressingRight())
            {
                GameMangerInput.InputEvent.Invoke_RightJustPressed();
            }else if(GameMangerInput.inputCheck.isPressingLeft())
            {
                GameMangerInput.InputEvent.Invoke_LeftJustPressed();

            }else if(GameMangerInput.inputCheck.isPressingDown())
            {
                GameMangerInput.InputEvent.Invoke_DownJustPressed();
            }

        Player1.instance.playerRigidBody2D.bodyType = RigidbodyType2D.Dynamic;
        Player1.instance.playerSprite.sortingLayerName = "Player";
        Player1.instance.playerSprite.sortingOrder = 1;
    }


    private void OnTriggerStay2D(Collider2D other) {
        GameManager.isPlayerNearStageWarp = true;
        isCurrentWarpActive = true;
        if(other.transform.position.y < transform.position.y) // 아래서 접촉
        {
            isDownActive = true;

            downInteract.SetActive(true);
            upInteract.SetActive(false);
        }else if(other.transform.position.y > transform.position.y) // 위에서 접촉
        {
            isDownActive = false;

            downInteract.SetActive(false);
            upInteract.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {

        isCurrentWarpActive = false;
        Player1.instance.playerRigidBody2D.bodyType = RigidbodyType2D.Dynamic;
        Player1.instance.playerRigidBody2D.velocity = Vector2.zero;

        GameManager.isPlayerNearStageWarp = false;
        GameMangerInput.blockAllInput = false;
        downInteract.SetActive(false);
        upInteract.SetActive(false);
    }
}
