using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    int score = 0;
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    static GameSession instance;

    void ManageSingleton()
    {
        // int instanceCount = FindObjectsOfType(GetType()).Length;
        // if(instanceCount > 1)
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        //ManageSingleton();
        //ManageSingleton_bak();
    }

    private void ManageSingleton_bak()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // To do :
    // if scene = MainScene = disable scoreText SINON enable

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        UpdateScoreUI();
        Debug.Log(score);
    }

    public int GetScore()
    {
        return score;
    }

    public void UpdateScoreUI()
    {
        scoreText.text = GetScore().ToString("000000000");
    }

    public IEnumerator RelaodGame(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        //FindObjectOfType<ScenePersist>().ResetScenePersist();
        //score = 0;
        //scoreText.text = GetScore().ToString("000000000");
        // LoadLevel("Level 1");
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentSceneIndex);
    }

    public void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
    }

    public void NewGame()
    {
        // FindObjectOfType<ScenePersist>().ResetScenePersist();
        //UpdateScoreUI();
        //Debug.Log(score);
        //Debug.Log(GetScore());
        //scoreKeeper.ResetScore();
        FindObjectOfType<ScoreKeeper>().ResetScore();
        LoadLevel("Level 1");
    }

    public void LoadLevel(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    [ContextMenu("Load main Level")]
    public void MainLevel()
    {
        SceneManager.LoadScene("MainScene");
    }

    [ContextMenu("Next Level")]
    public void NextLevel()
    {
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int NextSceneIndex = CurrentSceneIndex + 1;
        SceneManager.LoadScene(NextSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quiting app ....");
    }
}
