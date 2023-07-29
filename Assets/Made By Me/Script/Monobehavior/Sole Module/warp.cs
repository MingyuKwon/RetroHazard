using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class warp : MonoBehaviour
{
    [SerializeField] WayPoint wayPoint;
    CameraSetting cameraSetting;

    private void Awake() {
        cameraSetting = FindObjectOfType<CameraSetting>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player Body"))
        {
            if(blackOut.blackouting) return;
            cameraSetting.gameObject.SetActive(false);
            GameManagerUI.instance.BlackOut(MapNameCollection.getMapNameArrayEnglish((int)(wayPoint.toSceneName))[wayPoint.index] , new Vector3(wayPoint.x, wayPoint.y, Player1.instance.playerMove.transform.position.z));
        }
        
    }   
}
