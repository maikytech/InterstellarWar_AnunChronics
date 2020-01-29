//
// Programa que controla el comportamiento del player.
//
// Created by Poseto Studio on 2/14/17.
// Copyright © 2017 Poseto Studio. All rights reserved.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UXController : MonoBehaviour
{
    public static bool isGameOver;                      //Variable bool que indica si el juego ha terminado.
    public static int score;

    public bool IsShellButtonActive;
    public bool hasShellBeenfired;
    public GameObject FadeOff;                          //Black plane that comes out each time the scene is changed.
    public GameObject RestartGameobjectReference;       //Referencia al boton Restart.
    public Animator shellButtonAnimator;
    public Image shellButtonImage;
    public Image missileButtonImage;

    [SerializeField] float timeShellActive;
    [SerializeField] float timeViewDamage;						//Variable de tiempo para la visualizacion del daño.
    [SerializeField] float StartShellButtonAnimation;
    [SerializeField] Text ScoreTextReference;                       //Referencia al texto del score.
    [SerializeField] Text title;                                    //Referencia al titulo.
    [SerializeField] Text Stage;
    [SerializeField] GameObject gameoverGameobjectReference;		//Referencia al Gameover.	
    [SerializeField] GameObject congratulationGameobject;           //Referencia al objeto "Congratulation".
    [SerializeField] GameObject resumeButton;                       //Referencia al boton Play.                                                                  //public GameObject exitButton;						//Referencia al boton Exit.
    [SerializeField] GameObject pauseConver;                        //Referencia al objeto PauseCover.
    [SerializeField] GameObject damagePanel;						//Referencia al panel de daño.
    [SerializeField] GameObject fireButton;
    [SerializeField] GameObject shellButton;
    [SerializeField] GameObject missileButton;
    [SerializeField] GameObject ShellButtonBackground;
    [SerializeField] GameObject VirtualControl;
    [SerializeField] GameObject NextStage;
    [SerializeField] GameObject GoodJob;
   
    private int indexCurrentScene;
    private SpawnEnemies spawnEnemiesClass;				//Referencia a la clase "SpawnEnemies".
	private AudioSource audioGame;						//Referencia al audio del juego.
    private Animator fireButtonAnimator;
    private UnityADSInterstitial UnityADSInterstitialClass;

    void Awake()
	{
		spawnEnemiesClass = GameObject.FindWithTag ("GameController").GetComponent<SpawnEnemies> ();
		audioGame = GameObject.FindWithTag ("GameController").GetComponent<AudioSource> ();
        UnityADSInterstitialClass = GameObject.FindWithTag("GameController").GetComponent<UnityADSInterstitial>();
        fireButtonAnimator = fireButton.GetComponent<Animator>();
        shellButtonAnimator = shellButton.GetComponent<Animator>();
        shellButtonImage = shellButton.GetComponent<Image>();
        missileButtonImage = missileButton.GetComponent<Image>();
    }

	void Start()
	{
        shellButtonImage.fillAmount = 0f;
        missileButtonImage.fillAmount = 0f;
        isGameOver = false;
		gameoverGameobjectReference.SetActive (false);
		RestartGameobjectReference.SetActive (false);
        IsShellButtonActive = false;
        hasShellBeenfired = false;
        indexCurrentScene = SceneManager.GetActiveScene().buildIndex;
        UpdateScore ();
        Invoke ("TitleSetup", 10f);
        Invoke("FireButtonAnimation", 12f);
        Invoke("StageEnable", 11f);
        Invoke("StageDisable", 15f);
        InvokeRepeating("ShellButtonAnimation", StartShellButtonAnimation, 0.5f);
        LoadScore();
    }

    public void AddScore (int _score)					//Funcion que aumenta el valor de la variable "score".
	{
		score += _score;
		UpdateScore ();
        ScoreTextReference.color = Color.yellow;
        Invoke("ScoreColorConfig", 1f); 
    }
		
	public void GameOver()
	{
		isGameOver = true;								//Se activa el flag "isGameOver".
        VirtualControl.SetActive(false);
        UnityADSInterstitialClass.ShowInterstitial();
        

		if (spawnEnemiesClass.finalBossDestroyed)			//Si el Boss Final es destruido...
		{
            Invoke("CongratulationsSetup", 2f);
            VirtualControl.SetActive(true);

        } else {
			
			gameoverGameobjectReference.SetActive (true);	//Se visualiza el texto "Game Over".

		}
	}

	public void Restart()
	{
        Debug.Log("Se active el Restart");

        if (spawnEnemiesClass.finalBossDestroyed)
        {
            SceneManager.LoadScene("Stage1X");
            score = 0;
        }
        else
        {
            SceneManager.LoadScene(indexCurrentScene);
        }
	}

	public void Pause()
	{
		pauseConver.SetActive (true);				//Se activa la cubierta del pause.
		resumeButton.SetActive (true);				//Se activa el boton de continuar.
		audioGame.Pause();							//Se pausa el audio del juego.
		Time.timeScale = 0f;						//Se para toda la acción.
		title.enabled = false;						//Se desactiva el titulo del juego.
        Stage.enabled = false;
        GoodJob.SetActive(false);
        NextStage.SetActive(false);
	}

	public void Resume()
	{
		pauseConver.SetActive (false);				//Se activa la cubierta del pause.
		resumeButton.SetActive (false);				//Se activa el boton de continuar.
		audioGame.Play();							//Se pausa el audio del juego.
		Time.timeScale = 1f;						//Se para toda la acción.
	}

	public void Exit()
	{
		Application.Quit ();
	}

	public void Congratulation()
	{
		isGameOver = true;							//Se activa el flag "isGameOver".
	}

	public void damagePanelSetup()
	{
		damagePanel.SetActive (true);					
		Invoke("damagePanelDesactive", timeViewDamage);		
	}

	void damagePanelDesactive()
	{
		damagePanel.SetActive (false);
	}

	void TitleSetup()
	{
		title.enabled = false;
	}

    void StageEnable()
    {
        Stage.enabled = true;
    }

    void StageDisable()
    {
        Stage.enabled = false;
    }

    void UpdateScore()
	{
		ScoreTextReference.text = "Score: " + score;
    }

    void ScoreColorConfig()
    {
        ScoreTextReference.color = Color.white;
    }

    void FireButtonAnimation()
    {
        fireButtonAnimator.SetBool("IsFireButtonAnimActive", true);
        Invoke("FireButtonAnimDesactive", 16f);
    }

    void FireButtonAnimDesactive()
    {
        fireButtonAnimator.SetBool("IsFireButtonAnimActive", false);
    }

    //Config the fill efect of the shell button.
    void ShellButtonAnimation()
    {
        if (shellButtonImage.fillAmount < 1)
        {
            shellButtonImage.fillAmount += timeShellActive;
            missileButtonImage.fillAmount += timeShellActive;
            shellButtonAnimator.SetBool("hasShellBeenfired", false);
            hasShellBeenfired = false;
        }
        else if(IsShellButtonActive == false && hasShellBeenfired == false && !isGameOver)
        {
            shellButtonAnimator.SetBool("IsShellButtonActivated", true);
            IsShellButtonActive = true;
        }
    }

    public void InvokeMissionCompletedUXActive()
    {
        Invoke("MissionCompletedUXActive", 3f);
    }

    void MissionCompletedUXActive()
    {
        GoodJob.SetActive(true);
        NextStage.SetActive(true);
        Invoke("MissionCompletedUXDesaactive", 10f);
    }

    void MissionCompletedUXDesaactive()
    {
        GoodJob.SetActive(false);
        NextStage.SetActive(false);

    }

    void LoadScore()
    {
        if(indexCurrentScene == 2)
        {
            score = 0;
        }
        else
        {
            score = SaveLoad.saveLoad.previousStageScore;

        }

        UpdateScore();
    }

    void CongratulationsSetup()
    {
        congratulationGameobject.SetActive(true);
    }
}
