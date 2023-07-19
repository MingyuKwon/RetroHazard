using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InStageWarp : MonoBehaviour
{
    static public bool isInStageWarpingNow = false;
    GameObject downInteract;
    GameObject upInteract;

    GameObject beforeOpen;
    GameObject afterOpen;

    bool isCurrentWarpActive = false;
    bool isDownActive = false;

    Vector2 colliderSize;

    AudioSource audioSource = null;
    public AudioClip InteractSound = null;

    private void Start() {
        colliderSize = GetComponent<BoxCollider2D>().size;

        downInteract = transform.GetChild(0).gameObject;
        upInteract = transform.GetChild(1).gameObject;
        beforeOpen = transform.GetChild(2).gameObject;
        afterOpen = transform.GetChild(3).gameObject;

        downInteract.SetActive(false);
        upInteract.SetActive(false);
        beforeOpen.SetActive(true);
        afterOpen.SetActive(false);

        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable() {
        GameManager.EventManager.InStageWarpEvent += DoWarp;
    }

    private void OnDisable() {
        GameManager.EventManager.InStageWarpEvent += DoWarp;
    }

    public void PlayEnvironmentMusic()
    {
        if(audioSource == null) return;
        if(InteractSound == null) return;
        audioSource.volume = GameAudioManager.currentEnvironmentVolume;
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

    public void EnemyCollideIngnore(bool flag)
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player Body"), LayerMask.NameToLayer("Enemy Body"), flag);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player Body"), LayerMask.NameToLayer("Enemy not Body"), flag);
    }

    IEnumerator moveDirect(Vector2 moveDirection)
    {
        isInStageWarpingNow = true;

        bool isGoingUp = false;

        EnemyCollideIngnore(true);


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
