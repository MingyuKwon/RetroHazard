using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class warp : MonoBehaviour
{
    [SerializeField] int sceneIndex;
    [SerializeField] WayPoint wayPoint;
    CameraSetting cameraSetting;

    private void Awake() {
        cameraSetting = FindObjectOfType<CameraSetting>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        cameraSetting.gameObject.SetActive(false);
        GameManagerUI.instance.BlackOut(sceneIndex);
        other.transform.parent.transform.parent.transform.position = new Vector3(wayPoint.x, wayPoint.y, other.gameObject.transform.position.z);
    }   
}
