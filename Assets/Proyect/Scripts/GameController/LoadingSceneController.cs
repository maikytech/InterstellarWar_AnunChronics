using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{

    [SerializeField] private string sceneToLoad;
    [SerializeField] private Image progressImage;

    void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        //Class that allows asynchronous loading of the scene.
        AsyncOperation loading;

        loading = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Single);

        //Automatic scene loading is blocked
        loading.allowSceneActivation = false;

        while (loading.progress < 0.9f)
        {
            progressImage.fillAmount = loading.progress;

            yield return null;
        }

        progressImage.fillAmount = 1;
        loading.allowSceneActivation = true;
    }
}
