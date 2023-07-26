using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HideCanvas : MonoBehaviour
{
    public Text loadingText;
    public Slider loadingBar;
    private void Awake() {
        StartCoroutine(LoadingText());

        if(SceneManager.GetActiveScene().name ==  "Before Main Menu")
        {
            loadingBar.gameObject.SetActive(false);
            return;
        }else if(GameAudioManager.LoadManager.nextSceneName != null)
        {
            loadingBar.gameObject.SetActive(true);
            StartCoroutine(LoadGameSceneAsync());
        }

        

    }

    IEnumerator LoadingText()
    {
        while(true)
        {
            loadingText.text = "Loading";
            yield return new WaitForSecondsRealtime(0.02f);

            loadingText.text = "Loading .";
            yield return new WaitForSecondsRealtime(0.02f);

            loadingText.text = "Loading . .";
            yield return new WaitForSecondsRealtime(0.02f);

            loadingText.text = "Loading . . .";
            yield return new WaitForSecondsRealtime(0.02f);
        }
        
    }

    private IEnumerator LoadGameSceneAsync()
{
    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(GameAudioManager.LoadManager.nextSceneName);

    // 씬 전환을 일시 중단합니다.
    asyncLoad.allowSceneActivation = false;

    while (!asyncLoad.isDone)
    {
        if (asyncLoad.progress >= 0.9f)
        {
            // 여기서 필요한 작업을 수행한 후, 씬 전환을 허용합니다.
            asyncLoad.allowSceneActivation = true;
        }

        loadingBar.value = asyncLoad.progress;
        yield return null;
    }
}
}
