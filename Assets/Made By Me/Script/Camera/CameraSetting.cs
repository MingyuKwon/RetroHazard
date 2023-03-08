using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Com.LuisPedroFonseca.ProCamera2D;

public class CameraSetting : MonoBehaviour
{
    Tilemap tilemap;
    GameObject pm; 
    Vector3 maxLimit;
    Vector3 minLimit;

    [SerializeField] float playerWidth = 1f;

    float cameraVerticalHalf;
    float cameraHorizontalHalf;
    private void Awake() {
        tilemap = FindObjectOfType<WholeWorld>().GetComponent<Tilemap>();
        pm = FindObjectOfType<PlayerHealth>().gameObject;

        ProCamera2D.Instance.AddCameraTarget(pm.transform);
    }

    void Start() {
        maxLimit = new Vector3(tilemap.localBounds.max.x-playerWidth, tilemap.localBounds.max.y-playerWidth, tilemap.localBounds.max.z) ;
        minLimit = new Vector3(tilemap.localBounds.min.x + playerWidth , tilemap.localBounds.min.y + playerWidth , tilemap.localBounds.max.z) ;

        cameraVerticalHalf = Camera.main.orthographicSize;
        cameraHorizontalHalf = Camera.main.aspect * cameraVerticalHalf;

    }

    private void FixedUpdate() {

        if(transform.position.x > maxLimit.x-cameraHorizontalHalf)
        {
            transform.position = new Vector3(maxLimit.x-cameraHorizontalHalf, transform.position.y, transform.position.z);
        }

        if(transform.position.x < minLimit.x+cameraHorizontalHalf)
        {
            transform.position = new Vector3(minLimit.x+cameraHorizontalHalf, transform.position.y, transform.position.z);
        }

        if(transform.position.y > maxLimit.y - cameraVerticalHalf)
        {
            transform.position = new Vector3(transform.position.x, maxLimit.y - cameraVerticalHalf, transform.position.z);
        }

        if(transform.position.y < minLimit.y + cameraVerticalHalf)
        {
            transform.position = new Vector3(transform.position.x, minLimit.y + cameraVerticalHalf, transform.position.z);
        }
        
    } 
}
