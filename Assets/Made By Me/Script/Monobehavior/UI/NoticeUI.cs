using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class NoticeUI : MonoBehaviour
{
    NoticeUILogic noticeUILogic;
    public static NoticeUI instance;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else
        {
            Destroy(this.gameObject);
        }

        noticeUILogic = new NoticeUILogic(this);
    }

    private void OnEnable() {
        noticeUILogic.OnEnable();
    }

    private void OnDisable() {
        noticeUILogic.OnDisable();
    }
}
