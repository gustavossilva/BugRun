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
    public void GameOver() {
        PlayerPrefs.SetInt("bestScore", playerScore);
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}
