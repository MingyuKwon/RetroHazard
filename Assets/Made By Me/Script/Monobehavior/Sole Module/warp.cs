using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class warp : MonoBehaviour
{
    public static bool isPlayerNearWarp = false;
    [SerializeField] WayPoint wayPoint;
    [SerializeField] GameObject check;
    CameraSetting cameraSetting;


    private void Awake() {
        cameraSetting = FindObjectOfType<CameraSetting>();
        check.SetActive(false);
    }

    private void OnEnable() {
        GameManager.EventManager.WarpEvent += Warp;
    }

    private void OnDisable() {
        GameManager.EventManager.WarpEvent -= Warp;
    }

    bool isNowContacting = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player Body"))
        {
            isNowContacting = true;
            isPlayerNearWarp = true;
            check.SetActive(true);
        }
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player Body"))
        {
            isNowContacting = true;
            isPlayerNearWarp = true;
            check.SetActive(true);
        }
        
    }   

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player Body"))
        {
            isNowContacting = false;
            isPlayerNearWarp = false;
            check.SetActive(false);
        }
    }



    public void Warp()
    {
        if(blackOut.blackouting) return;
        if(!isNowContacting) return;

        cameraSetting.gameObject.SetActive(false);
        GameManagerUI.instance.BlackOut(MapNameCollection.getMapNameArrayEnglish(
            (int)(wayPoint.toSceneName))[wayPoint.index] , 
            new Vector3(wayPoint.x, wayPoint.y, 
            Player1.instance.playerMove.transform.position.z));
    }
}
