using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;


public class NPCDialogScript : MonoBehaviour
{
    [SerializeField] Dialog dialog;
    [SerializeField] bool visited = false;

    public void showDialog()
    {
        GameManagerUI.instance.showTalkNPCDialog(visited, dialog);
        if(!visited) visited = true;
    }
}
