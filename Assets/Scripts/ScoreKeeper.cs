using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int currentScore = 0;
    static ScoreKeeper instance;

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
        ManageSingleton();
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        currentScore = Mathf.Clamp(currentScore, 0, int.MaxValue);
        //Debug.Log(currentScore);
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }

    public void DecreaseScore(int scoreToDecrease)
    {
        currentScore -= scoreToDecrease;
        currentScore = Mathf.Clamp(currentScore, 0, int.MaxValue);
    }
}
