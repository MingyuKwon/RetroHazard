using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapShowInteract : MonoBehaviour
{
    public MapNameCollection.SceneName sceneName;

    [Header("is Passage is Vertiacl? or Horizontal")]
    public bool isVertical;

    [Header("What is the name of Plus side of Colldier? what about Minus?")]
    public int inPlus;
    public int inMinus;

    Vector2 enterVector;



    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player Body") )
        {
            enterVector = Player1.instance.playerMove.playerDirection;
            
            if(isVertical)
            {
                float angle = Vector2.Angle(Vector2.up, enterVector);
                if (angle < 90f)
                {
                    enterVector = Vector2.up;
                }else
                {
                    enterVector = Vector2.down;
                }
            }else
            {
                float angle = Vector2.Angle(Vector2.right, enterVector);
                if (angle < 90f)
                {
                    enterVector = Vector2.right;
                }else
                {
                    enterVector = Vector2.left;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player Body") )
        {
            float angle = Vector2.Angle(Player1.instance.playerMove.playerDirection, enterVector);

            if (angle < 90f) // 유턴 안하고 직진으로 빠져나감 -> 바꿔야 함
            {
                if(isVertical)
                {
                    if(enterVector == Vector2.up)
                    {
                        UI.instance.inGameUI.ShowMapShowPanel(MapNameCollection.getMapNameArray((int)sceneName)[inPlus]);
                    }else if(enterVector == Vector2.down)
                    {
                        UI.instance.inGameUI.ShowMapShowPanel(MapNameCollection.getMapNameArray((int)sceneName)[inMinus]);
                    }
                }else
                {
                    if(enterVector == Vector2.right)
                    {
                        UI.instance.inGameUI.ShowMapShowPanel(MapNameCollection.getMapNameArray((int)sceneName)[inPlus]);
                    }else if(enterVector == Vector2.left)
                    {
                        UI.instance.inGameUI.ShowMapShowPanel(MapNameCollection.getMapNameArray((int)sceneName)[inMinus]);
                    }
                }

            
            }else // 유턴 하고 있던 곳으로 되돌아감 -> 표시 안함
            {
                
            }
        }
    }
}
