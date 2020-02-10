using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : Singleton<GameController>
{
	public Action<int> updateScore;
    public int playerScore = 0;

    public float timeToNextLevel = 10f;
    public int nextLevelScore = 50;

    public float currentTime = 0;

    public bool gameOver = false;
    public bool gamePaused = false;

    public GameObject pauseButton;
    public GameObject pausePanel;
    public GameObject pausePanelInitial;
    public GameObject pausePanelConfirm;

    protected override void Awake() {
        this.IsPersistentBetweenScenes = false;
        base.Awake();
    }

    public void SetScore(int value) {
        this.playerScore += value;
        if(updateScore != null) {
            updateScore(value);
        }
    }

    private void Update() {
        currentTime += Time.deltaTime;
        if(currentTime >= timeToNextLevel) {
            //Change game speed here;
            //Change Spawn time here;
            SetScore(nextLevelScore);
            currentTime = 0;
        }
    }
    public void GameOver() {
        PlayerPrefs.SetInt("bestScore", playerScore);
        this.GoTo("GameOver");
    }

    public void GoTo(string scene) {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void Pause() {
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        pausePanel.SetActive(true);
        gamePaused = true;
    }
    public void Play() {
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
        gamePaused = false;
    }

    public void ShowConfirmMessage(bool show) {
        pausePanelInitial.SetActive(!show);
        pausePanelConfirm.SetActive(show);
    }
}
