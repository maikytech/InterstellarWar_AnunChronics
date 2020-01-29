using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingContinueSceneController : MonoBehaviour
{
    //[SerializeField] private Text percentText;
    [SerializeField] private Image progressImage;

    void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        //Class that allows asynchronous loading of the scene.
        AsyncOperation loading;
        
        loading = SceneManager.LoadSceneAsync(SaveLoad.saveLoad.indexCurrentSceneBeforeDie, LoadSceneMode.Single);

        //Automatic scene loading is blocked
        loading.allowSceneActivation = false;

        while (loading.progress < 0.9f)
        {

            //percentText.text = string.Format("{0}%", loading.progress * 100);

            progressImage.fillAmount = loading.progress;

            yield return null;
        }

        //percentText.text = "100%";
        progressImage.fillAmount = 1;
        loading.allowSceneActivation = true;
    }
}