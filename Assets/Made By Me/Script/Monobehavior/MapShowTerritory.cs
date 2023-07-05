using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShowTerritory : MonoBehaviour
{
    public MapNameCollection.sceneName sceneName;

    // siblingindex 랑 MapNameCollection 에 있는 배열의 이름 순서와 같아야 한다
    int siblingindex;

    private void Awake() {
        siblingindex = transform.GetSiblingIndex();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        UI.instance.inGameUI.ShowMapShowPanel(MapNameCollection.getMapNameArray((int)sceneName)[siblingindex]);
    }
}
