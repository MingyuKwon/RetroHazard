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
    public static blackOut instance;
    float delayTime = 1.5f;
    [SerializeField] Image image;

    void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.transform.parent.gameObject);
        }else
        {
            Destroy(this.transform.parent.gameObject);
        }

    }

    public void BlackOut(int index)
    {
        instance.StartCoroutine(blackout(index));
    }

     IEnumerator blackout(int index)
    {
        GameManager.instance.SetPlayerMove(false);
        while(image.color.a < 1f)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.MoveTowards(image.color.a, 1f, delayTime * Time.deltaTime));
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadScene(index);
        while(image.color.a > 0f)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.MoveTowards(image.color.a, 0f, delayTime * Time.deltaTime));
            yield return new WaitForEndOfFrame(); 
        }
        GameManager.instance.SetPlayerMove(true);
    }
}
