using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NoticeInteract : MonoBehaviour
{
    [SerializeField] NoticeDialog dialog;

    bool isAlive = true;

    private void Start() {
        if(SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_Notice_Destroy[transform.GetSiblingIndex()])
        {
            isAlive = false;
            this.gameObject.SetActive(false);
        }

    }


    private void OnTriggerEnter2D(Collider2D other) {
        if(isAlive && other.gameObject.layer == LayerMask.NameToLayer("Player Body") )
        {
            GameManager.EventManager.InvokeShowNotice("SaveSlotUI", dialog.noticeDialog , true, 900 ,250);
            
            SaveSystem.instance.ActiveStageSaves[SceneManager.GetActiveScene().buildIndex].is_Notice_Destroy[transform.GetSiblingIndex()] = true;
            isAlive = false;
            gameObject.SetActive(false);
        }
    }

}
