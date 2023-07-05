using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShowTerritory : MonoBehaviour
{
    public MapNameCollection.sceneName sceneName;

    int siblingindex;

    private void Awake() {
        siblingindex = transform.GetSiblingIndex();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        UI.instance.inGameUI.ShowMapShowPanel(MapNameCollection.getMapNameArray((int)sceneName)[siblingindex]);
    }
}
