using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] int shadowIndex;
    [SerializeField] SpriteRenderer sprite;

    private void OnEnable() {
        GameManager.EventManager.ShadowEvent += ShadowClear;
    }

    private void OnDisable() {
        GameManager.EventManager.ShadowEvent -= ShadowClear;
    }

    private void ShadowClear(int index, bool isClear)
    {
        if(shadowIndex != index) return;

        StartCoroutine(shadowCoroutine(isClear));
    }

    IEnumerator shadowCoroutine(bool isClear)
    {
        float currentA;
        float targetA;

        if(isClear){
            currentA = 1f;
            targetA = 0f;

        }else
        {
            currentA = 0f;
            targetA = 1f;
        }

        int i = 10;
        float unirform = (targetA - currentA) / i;

        for(int j=0; j<i; j++)
        {
            currentA += unirform;
            sprite.color = new Color(1f,1f,1f,currentA);
            yield return new WaitForSeconds(0.025f);
        }

    }
}
