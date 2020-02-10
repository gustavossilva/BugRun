using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreController : MonoBehaviour
{
    public void GoTo(string screen) {
        SceneManager.LoadScene(screen, LoadSceneMode.Single);
    }
}
