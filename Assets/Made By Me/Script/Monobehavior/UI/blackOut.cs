using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Rewired;
using Sirenix.OdinInspector;
using DG.Tweening;

public class blackOut : MonoBehaviour
{
    float delayTime = 2;
    [SerializeField] Image image;

    Vector3 destination;

    public void BlackOut(string sceneName, Vector3 destination)
    {
        StartCoroutine(blackout(sceneName));
        this.destination = destination;
    }

    public void BlackOutSpeed(float time)
    {
        StartCoroutine(blackout(time));
    }

    IEnumerator blackout(string sceneName)
    {
        GameManager.instance.EnemyCollideIgnore(true);
        GameManager.instance.SetPausePlayer(true);
        while(image.color.a < 1f)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.MoveTowards(image.color.a, 1f, delayTime * Time.deltaTime));
            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadSceneAsync(sceneName);
        Player1.instance.playerMove.transform.position = destination;

        GameManager.instance.SetPausePlayer(false);

        yield return new WaitForSeconds(0.1f); 

        while(image.color.a > 0f)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.MoveTowards(image.color.a, 0f, delayTime * Time.deltaTime));
            yield return new WaitForEndOfFrame(); 
        }

        GameManager.instance.EnemyCollideIgnore(false);
    }

    IEnumerator blackout(float speed)
    {
        GameManager.instance.SetPausePlayer(true);
        image.color = new Color(image.color.r, image.color.g, image.color.b, 1);

        while(image.color.a > 0f)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.MoveTowards(image.color.a, 0f, speed * Time.deltaTime));
            yield return new WaitForEndOfFrame(); 
        }
        GameManager.instance.SetPausePlayer(false);
        
    }
}
