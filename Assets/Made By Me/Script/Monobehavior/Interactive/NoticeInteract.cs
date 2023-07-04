using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoticeInteract : MonoBehaviour
{
    [SerializeField] NoticeDialog dialog;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player Body") )
        {
            GameManager.EventManager.InvokeShowNotice("SaveSlotUI", dialog.noticeDialog , true, 900 ,250);
            gameObject.SetActive(false);
        }
    }

}
