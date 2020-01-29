using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;				//Libreria para el manejo de escenas.


public class NextScene : MonoBehaviour
{
    public bool goToNextScene;

    [SerializeField] float timeLoadScene;                       //Tiempo de carga de la proxima escena.
    [SerializeField] GameObject LoadPanelNextStage;

    private int indexCurrentScene;
    private UnityADSInterstitial UnityADSInterstitialClass;

    private void Awake()
    {
        if(indexCurrentScene > 1)
        {
            UnityADSInterstitialClass = GameObject.FindWithTag("GameController").GetComponent<UnityADSInterstitial>();
        }   
    }

    void Start()
	{
        goToNextScene = false;
        indexCurrentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void InvokeNextStage()
    {
        Invoke("LoadNextScene", timeLoadScene);
    }

    void LoadNextScene()							
	{
        LoadPanelNextStage.SetActive(true);


        if(indexCurrentScene > 1)
        {
            UnityADSInterstitialClass.ShowInterstitial();

            SaveLoad.saveLoad.previousStageScore = UXController.score;
            SaveLoad.saveLoad.indexCurrentSceneBeforeDie = indexCurrentScene + 1;

            SaveLoad.saveLoad.Save();
        }
    }
}
