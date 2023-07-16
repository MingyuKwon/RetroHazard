using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShowTerritory : MonoBehaviour
{
    public MapNameCollection.sceneName sceneName;
    // siblingindex 랑 MapNameCollection 에 있는 배열의 이름 순서와 같아야 한다
    public int index;

    public BackGroundAudioType musicName;


    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player Body"))
        {
            UI.instance.inGameUI.ShowMapShowPanel(MapNameCollection.getMapNameArray((int)sceneName)[index]);
            GameAudioManager.instance.PlayBackGroundMusic(musicName);
        }
    }
}
