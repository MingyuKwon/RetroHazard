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
            cameraSetting.gameObject.SetActive(false);
            GameManagerUI.instance.BlackOut(wayPoint.toIndexNum);
            other.transform.parent.transform.parent.transform.position = new Vector3(wayPoint.x, wayPoint.y, other.gameObject.transform.position.z);
        }
        
    }   
}
