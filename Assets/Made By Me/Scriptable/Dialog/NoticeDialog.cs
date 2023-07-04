using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Notice Dialog", fileName = " Notice Dialog")]
public class NoticeDialog : ScriptableObject
{
    [TextArea]
    public string[] noticeDialog;
}
