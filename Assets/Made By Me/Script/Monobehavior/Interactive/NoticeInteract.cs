using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class NoticeInteract : MonoBehaviour
{
    [SerializeField] NoticeDialog dialog;

    [Header("Pick Only One")]
    [SerializeField] bool isEdge;
    [SerializeField] bool isSquare;

    [Header("0 will be ignore and only square collider is valid")]
    [SerializeField] int isSquareIndex;

    bool isAlive = true;

    private void Awake() {
        GameManager.EventManager.InteractNoticeEvent += AwakeNoticeInteract;
    }

    private void OnDestroy() {
        GameManager.EventManager.InteractNoticeEvent -= AwakeNoticeInteract;
    }

    private void Start() {
        if(SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_Notice_Destroy[transform.GetSiblingIndex()])
        {
            removePermenantly();
        }

        if(isSquare)
        {
            isAlive = false;
            this.gameObject.SetActive(false);
        }

    }

    private void removePermenantly()
    {
        isAlive = false;
        gameObject.SetActive(false);

        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();

        if(boxCollider2D != null)
        {
            Destroy(boxCollider2D);
        }
    }

    [Button]
    private void AwakeNoticeInteract(int index, bool isCreate)
    {
        if(isSquare)
        {
            if(index == isSquareIndex)
            {
                if(isCreate)
                {
                    isAlive = true;
                    gameObject.SetActive(true);
                }else
                {
                    removePermenantly();
                }
                
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {

            if(isAlive && other.gameObject.layer == LayerMask.NameToLayer("Player Body") )
            {
                if(GameAudioManager.LanguageManager.currentLanguage == "E")
                {
                    GameManager.EventManager.InvokeShowNotice("Field", dialog.englishNoticeDialog , true, 900 ,250);
                }else if(GameAudioManager.LanguageManager.currentLanguage == "K")
                {
                    GameManager.EventManager.InvokeShowNotice("Field", dialog.koreanNoticeDialog , true, 900 ,250);
                }
            
                SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_Notice_Destroy[transform.GetSiblingIndex()] = true;
                removePermenantly();
            }
    }

}
