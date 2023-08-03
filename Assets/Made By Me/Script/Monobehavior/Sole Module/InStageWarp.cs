using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InStageWarp : MonoBehaviour
{
    static public bool isInStageWarpingNow = false;
    GameObject downLeftInteract;
    GameObject upRightInteract;

    GameObject beforeOpen;
    GameObject afterOpen;

    bool isCurrentWarpActive = false;
    bool isDownActive = false;
    bool isLeftActive = false;

    Vector2 colliderSize;

    AudioSource audioSource = null;
    public AudioClip InteractSound = null;


    public bool isHorizontal = false;

    public bool isBottom;

    private void Start() {
        colliderSize = GetComponent<BoxCollider2D>().size;

        downLeftInteract = transform.GetChild(0).gameObject;
        upRightInteract = transform.GetChild(1).gameObject;
        beforeOpen = transform.GetChild(2).gameObject;
        afterOpen = transform.GetChild(3).gameObject;

        downLeftInteract.SetActive(false);
        upRightInteract.SetActive(false);
        beforeOpen.SetActive(true);
        afterOpen.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable() {
        GameManager.EventManager.InStageWarpEvent += DoWarp;
        GameAudioManager.AudioEvent.updateEnvironmentVolume += UpdateEnvironmentVolume;

    }

    private void OnDisable() {
        GameManager.EventManager.InStageWarpEvent -= DoWarp;
        GameAudioManager.AudioEvent.updateEnvironmentVolume -= UpdateEnvironmentVolume;

    }

    private void UpdateEnvironmentVolume()
    {
        if(audioSource == null) return;
        audioSource.volume = GameAudioManager.currentEnvironmentVolume * GameAudioManager.totalVolme;
    }

    public void PlayEnvironmentMusic()
    {
        if(audioSource == null) return;
        if(InteractSound == null) return;
        audioSource.volume = GameAudioManager.currentEnvironmentVolume * GameAudioManager.totalVolme;
        audioSource.clip = InteractSound;
        audioSource.Play();
    }

    private void DoWarp()
    {
        if(!isCurrentWarpActive) return;

        GameMangerInput.blockAllInput = true;
        beforeOpen.SetActive(false);
        afterOpen.SetActive(true);
        PlayEnvironmentMusic();

        if(isHorizontal)
        {
            if(isLeftActive)
        {
            if(isBottom)
            {
                Player1.instance.playerMove.transform.position = new Vector3(transform.position.x - colliderSize.x / 2, 
                transform.position.y + colliderSize.y / 3, 
                transform.position.z); 
                StartCoroutine(moveDirect(Vector2.right));
            }else
            {
                Player1.instance.playerMove.transform.position = new Vector3(transform.position.x - colliderSize.x / 2, 
                transform.position.y, 
                transform.position.z); 
                StartCoroutine(moveDirect(Vector2.right));
            }
            

        }else
        {
            if(isBottom)
            {
                Player1.instance.playerMove.transform.position = new Vector3(transform.position.x + colliderSize.x / 2, 
                transform.position.y + colliderSize.y / 3, 
                transform.position.z); 
                StartCoroutine(moveDirect(Vector2.left));
            }else
            {
                Player1.instance.playerMove.transform.position = new Vector3(transform.position.x + colliderSize.x / 2, 
                transform.position.y, 
                transform.position.z); 
                StartCoroutine(moveDirect(Vector2.left));
            }

        }


        }else
        {
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

        
    }

    public void EnemyCollideIngnore(bool flag)
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player Body"), LayerMask.NameToLayer("Enemy Body"), flag);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player Body"), LayerMask.NameToLayer("Enemy not Body"), flag);
    }

    IEnumerator moveDirect(Vector2 moveDirection)
    {
        isInStageWarpingNow = true;

        int direction = 0; // 0: UP, 1 : down . 2 : left, 3 : right

        EnemyCollideIngnore(true);


        if(moveDirection == Vector2.up)
        {
            direction = 0;
        }else if(moveDirection == Vector2.down)
        {
            direction = 1;
        }else if(moveDirection == Vector2.left)
        {
            direction = 2;
        }else if(moveDirection == Vector2.right)
        {
            direction = 3;
        }

        if(direction == 0)
        {
            GameMangerInput.InputEvent.Invoke_UPJustPressed();
        }else if(direction == 1)
        {
            GameMangerInput.InputEvent.Invoke_DownJustPressed();
        }else if(direction == 2)
        {
            GameMangerInput.InputEvent.Invoke_LeftJustPressed();
        }else if(direction == 3)
        {
            GameMangerInput.InputEvent.Invoke_RightJustPressed();
        }

        Player1.instance.playerSprite.sortingLayerName = "0";
        Player1.instance.playerSprite.sortingOrder = 4;
        Player1.instance.playerRigidBody2D.bodyType = RigidbodyType2D.Kinematic;

        Player1.instance.playerRigidBody2D.velocity = Vector2.zero;

        while(isCurrentWarpActive)
        {
            Player1.instance.playerMove.transform.Translate(moveDirection.normalized * Time.fixedDeltaTime * 2);
            yield return new WaitForFixedUpdate();
        }

        if(direction == 0)
        {
            GameMangerInput.InputEvent.Invoke_UPJustReleased();
        }else if(direction == 1)
        {
            GameMangerInput.InputEvent.Invoke_DownJustReleased();
        }else if(direction == 2)
        {
            GameMangerInput.InputEvent.Invoke_LeftJustReleased();
        }else if(direction == 3)
        {
            GameMangerInput.InputEvent.Invoke_RightJustReleased();
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
            
        EnemyCollideIngnore(false);
        Player1.instance.playerRigidBody2D.bodyType = RigidbodyType2D.Dynamic;
        Player1.instance.playerSprite.sortingLayerName = "Player";
        Player1.instance.playerSprite.sortingOrder = 1;

        beforeOpen.SetActive(true);
        afterOpen.SetActive(false);
        isInStageWarpingNow = false;
    }


    private void OnTriggerStay2D(Collider2D other) {
        GameManager.isPlayerNearStageWarp = true;
        isCurrentWarpActive = true;

        if(isHorizontal)
        {
            if(other.transform.position.x < transform.position.x) // 왼쪽에서 접촉
        {
            isLeftActive = true;

            downLeftInteract.SetActive(true);
            upRightInteract.SetActive(false);
        }else if(other.transform.position.x > transform.position.x) // 오른쪽에서 접촉
        {
            isLeftActive = false;

            downLeftInteract.SetActive(false);
            upRightInteract.SetActive(true);
        }

        }else
        {
            if(other.transform.position.y < transform.position.y) // 아래서 접촉
        {
            isDownActive = true;

            downLeftInteract.SetActive(true);
            upRightInteract.SetActive(false);
        }else if(other.transform.position.y > transform.position.y) // 위에서 접촉
        {
            isDownActive = false;

            downLeftInteract.SetActive(false);
            upRightInteract.SetActive(true);
        }
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) {

        isCurrentWarpActive = false;
        Player1.instance.playerRigidBody2D.bodyType = RigidbodyType2D.Dynamic;
        Player1.instance.playerRigidBody2D.velocity = Vector2.zero;

        GameManager.isPlayerNearStageWarp = false;
        GameMangerInput.blockAllInput = false;
        downLeftInteract.SetActive(false);
        upRightInteract.SetActive(false);
    }
}
