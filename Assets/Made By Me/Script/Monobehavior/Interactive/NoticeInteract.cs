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

    private void Start() {
        if(SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_Notice_Destroy[transform.GetSiblingIndex()])
        {
            isAlive = false;
            this.gameObject.SetActive(false);
        }

        if(isSquare)
        {
            isAlive = false;
            this.gameObject.SetActive(false);
        }

    }

    [Button]
    private void AwakeNoticeInteract(int index)
    {
        if(isSquare)
        {
            if(index == isSquareIndex)
            {
                isAlive = true;
                gameObject.SetActive(true);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {

            if(isAlive && other.gameObject.layer == LayerMask.NameToLayer("Player Body") )
            {
                GameManager.EventManager.InvokeShowNotice("Field", dialog.noticeDialog , true, 900 ,250);
            
                SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_Notice_Destroy[transform.GetSiblingIndex()] = true;
                isAlive = false;
                gameObject.SetActive(false);
            }

        
    }

}