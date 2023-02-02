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
    GameObject cameraSetting;

    void OnTriggerEnter2D(Collider2D other) 
    {
        cameraSetting.gameObject.SetActive(false);
        GameManagerUI.instance.BlackOut(sceneIndex);
        other.gameObject.transform.position = new Vector3(wayPoint.x, wayPoint.y, other.gameObject.transform.position.z);
    }   
}
