using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Com.LuisPedroFonseca.ProCamera2D;

public class CameraSetting : MonoBehaviour
{
    
    BoxCollider2D tilemap;
    GameObject pm; 
    Vector3 maxLimit;
    Vector3 minLimit;

    [SerializeField] int cameraHeight = -5;

    float playerWidth = 0.2f;

    float cameraVerticalHalf;
    float cameraHorizontalHalf;
    private void Awake() {
        tilemap = FindObjectOfType<WholeWorld>().GetComponent<BoxCollider2D>();
        pm = FindObjectOfType<PlayerHealth>().gameObject;

        ProCamera2D.Instance.AddCameraTarget(pm.transform);
        
    }

    void Start() {
        transform.position = new Vector3(transform.position.x, transform.position.y, cameraHeight);

        maxLimit = new Vector3(tilemap.bounds.max.x-playerWidth, tilemap.bounds.max.y-playerWidth, tilemap.bounds.max.z) ;
        minLimit = new Vector3(tilemap.bounds.min.x + playerWidth , tilemap.bounds.min.y + playerWidth , tilemap.bounds.max.z) ;
    }

    private void FixedUpdate() {

        cameraVerticalHalf = Camera.main.orthographicSize;
        cameraHorizontalHalf = Camera.main.aspect * cameraVerticalHalf;

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
