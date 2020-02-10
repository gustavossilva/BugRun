using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI playerScore;

    public TMP_InputField playerName;

    private int bestScore;
    private void Awake() {
        bestScore = PlayerPrefs.GetInt("bestScore", 0);
        playerScore.SetText(bestScore.ToString());
    }

    public void SendHighScore() {
        if (playerName.text != "") {
            PlayerPrefs.SetString("playerName", playerName.text);
            HighScores.AddNewHighScore(playerName.text, bestScore, false);
        }
        SceneManager.LoadScene("HighScore", LoadSceneMode.Single);
    }
}
