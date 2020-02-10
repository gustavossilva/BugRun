using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class HighScores : Singleton<HighScores>
{
    const string privateCode = "RilY7vycREe8-3ued_e7yAHsCu1HMfzUaW0XZlhEqr0w";
    const string publicCode = "5e2681b1fe224b0478282e95";
    const string webURL = "http://dreamlo.com/lb/";

    public Highscore[] highscoresList;
    DisplayHighscores highscoreDisplay;
    // Start is called before the first frame update

    protected override void Awake()
    {
        this.IsPersistentBetweenScenes = false;
        highscoreDisplay = GetComponent<DisplayHighscores>();
        base.Awake();
    }

    public static void AddNewHighScore(string username, int score, bool downloadHighScore) {
        Instance.StartCoroutine(Instance.UploadNewHighScore(username, score, downloadHighScore));
    }

    public void DownloadHighscores() {
        StartCoroutine("DownloadHighscoresFromDatabase");
    }

    IEnumerator UploadNewHighScore(string username, int score, bool downloadHighScore) {
        Debug.Log("Trying");
        UnityWebRequest uploadRequest = UnityWebRequest.Get(webURL + privateCode + "/add/" + UnityWebRequest.EscapeURL(username) + "/" + score);
        yield return uploadRequest.SendWebRequest();

        if (uploadRequest.isNetworkError || uploadRequest.isHttpError) {
            Debug.LogError("Upload network error");
        } else {
            print ("Upload Sucessful");
            if(downloadHighScore) {
                DownloadHighscores();
            }
        }
    }
    IEnumerator DownloadHighscoresFromDatabase() {
        UnityWebRequest downloadHighScoreRequest = UnityWebRequest.Get(webURL + publicCode + "/pipe/");
        yield return downloadHighScoreRequest.SendWebRequest();
        if (downloadHighScoreRequest.isNetworkError || downloadHighScoreRequest.isHttpError) {
            Debug.LogError("Download network error");
        } else {
            FormatHighscores (downloadHighScoreRequest.downloadHandler.text);
            highscoreDisplay.OnHighScoresDownloaded(highscoresList);
        }
    }

    void FormatHighscores(string textStream) {
        string[] entries = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];
        for (int i = 0; i<entries.Length; i++) {
            string[] entryInfo = entries[i].Split(new char[] {'|'});
            string usernameWithPlus = entryInfo[0];
            string username = usernameWithPlus.Replace('+',' ');
            int score = int.Parse(entryInfo[1]);
            highscoresList[i] = new Highscore(username, score);
        }
    }
}

public struct Highscore {
    public string username;
    public int score;

    public Highscore(string _username, int _score) {
        username = _username;
        score = _score;
    }
}
