using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    }

    private void OnEnable() {
        
    }

    void Start() {
        maxLimit = new Vector3(tilemap.localBounds.max.x-playerWidth, tilemap.localBounds.max.y-playerWidth, tilemap.localBounds.max.z) ;
        minLimit = new Vector3(tilemap.localBounds.min.x + playerWidth , tilemap.localBounds.min.y + playerWidth , tilemap.localBounds.max.z) ;

        cameraVerticalHalf = Camera.main.orthographicSize;
        cameraHorizontalHalf = Camera.main.aspect * cameraVerticalHalf;
    }

    void Update(){
            Vector3 pmPosition = pm.transform.position;
            float clampXPosition = Mathf.Clamp(pmPosition.x , minLimit.x + cameraHorizontalHalf, maxLimit.x-cameraHorizontalHalf);
            float clampYPosition = Mathf.Clamp(pmPosition.y , minLimit.y + cameraVerticalHalf, maxLimit.y - cameraVerticalHalf);
            transform.position = new Vector3(clampXPosition, clampYPosition, -10);
    }
}
