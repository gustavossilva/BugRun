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

    public float timeToNextLevel = 5f;
    public int nextLevelScore = 50;

    public float currentTime = 0;
    public float gameSpeed = 6f;
    public float spawnTime = 4f;
    public float extraPlayerVelocity = 4f;
    public float extraPlayerVelocityMobile = 4f;

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
    void Start()
    {
        Destroy (GameObject.Find("SceneController"));
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
            gameSpeed = Mathf.Clamp(gameSpeed + 1f, 6f, 11f);
            spawnTime = Mathf.Clamp(spawnTime - 0.5f, 0.7f, 4f);
            extraPlayerVelocity = Mathf.Clamp(extraPlayerVelocity + 1f, 4f, 10f);
            extraPlayerVelocityMobile = Mathf.Clamp(extraPlayerVelocityMobile + 0.8f, 4f, 7f);
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
        Time.timeScale = 1f;
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
