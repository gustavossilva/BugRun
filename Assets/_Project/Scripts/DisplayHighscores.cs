using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHighscores : MonoBehaviour
{
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI beTheFirst;
    public TextMeshProUGUI[] highScoreNames;
    public TextMeshProUGUI[] highScores;
    public CanvasGroup highScoreCanvasGroup;
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
        if (highscoreList.Length == 0) {
            this.beTheFirst.gameObject.SetActive(true);
            this.highScoreCanvasGroup.alpha = 0;
            return;
        }
        if (beTheFirst.gameObject.activeSelf) {
            beTheFirst.gameObject.SetActive(false);
        }
        this.highScoreCanvasGroup.alpha = 1;
        for (int i = 0; i < this.highScoreNames.Length; i ++) {
            if (highscoreList.Length > i) { 
                this.highScoreNames[i].text = highscoreList[i].username;
                this.highScores[i].text = highscoreList[i].score.ToString();
            }
        }        
    }

    IEnumerator RefreshHighscores() {
        while (true) {
            HighScores.instance.DownloadHighscores();
            yield return new WaitForSeconds(30);
        }
    }
}
