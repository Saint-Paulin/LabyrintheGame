using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] int scoreToAdd;
    BoxCollider2D chestCollider;
    ScoreKeeper scoreKeeper;
    void Start()
    {
        chestCollider = GetComponent<BoxCollider2D>();
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (chestCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            FindObjectOfType<AudioPlayer>().PlayPickupClip();
            Destroy(gameObject);
            scoreKeeper.AddScore(scoreToAdd);
            FindFirstObjectByType<UIDisplay>().UpdateScoreUI();
        }
    }
}
