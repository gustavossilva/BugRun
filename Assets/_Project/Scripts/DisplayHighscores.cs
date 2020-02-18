using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DisplayHighscores : MonoBehaviour
{
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI playerNamePosition;
    public TextMeshProUGUI playerCurrentScore;
    public TextMeshProUGUI[] highScoreNames;
    public TextMeshProUGUI[] highScores;
    public CanvasGroup highScoreCanvasGroup;
    public AudioSource highScoreAudio;
    public TextMeshProUGUI congratsText;

    public bool removeHighScoreUpdate = false;
    // Start is called before the first frame update
    void Start()
    {
        if (highScoreCanvasGroup) {
            highScoreCanvasGroup.alpha = 0;
        }
        if (PlayerPrefs.HasKey("bestScore")) {
            this.bestScore.text = PlayerPrefs.GetInt("bestScore").ToString();
        }
        else {
            this.bestScore.text = "0";
        }

        StartCoroutine("RefreshHighscores");
    }

    public void OnHighScoresDownloaded(Highscore[] highscoreList) {
        int playerPosition = 0;
        string userName = PlayerPrefs.GetString("playerName");
        int playerScore =  PlayerPrefs.GetInt("bestScore");
        for (int i = 0; i < highscoreList.Length; i++) {
            if(highscoreList[i].username == userName) {
                if(highscoreList[i].score > playerScore) {
                    playerScore = highscoreList[i].score;
                }
                playerPosition = i + 1;
            }
        }
        this.highScoreCanvasGroup.alpha = 1;
        for (int i = 0; i < this.highScoreNames.Length; i ++) {
            string character =  i < 10 ? "0":"";
            if (highscoreList.Length > i) { 
                this.highScoreNames[i].text = character+(i + 1).ToString() + ". "+highscoreList[i].username;
                this.highScores[i].text = highscoreList[i].score.ToString();
            }
        }
        if(SceneManager.GetActiveScene().name == "HighScore") {
            string character = playerPosition < 10 ? "0":"";
            playerNamePosition.text = character+playerPosition.ToString() + ". "+userName;
            playerCurrentScore.text = playerScore.ToString();
        }
        if (playerPosition < 10 && SceneManager.GetActiveScene().name == "HighScore") {
            highScoreAudio.Play();
            congratsText.gameObject.SetActive(true);
        }
        if(removeHighScoreUpdate)
            StopCoroutine("RefreshHighscores");
    }

    IEnumerator RefreshHighscores() {
        while (true) {
            HighScores.Instance.DownloadHighscores();
            yield return new WaitForSeconds(30);
        }
    }
}
