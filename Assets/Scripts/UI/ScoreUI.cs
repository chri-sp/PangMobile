using System;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI _scoreText;

    private void Awake()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        GameManager.OnScoreUpdate += UpdateScoreUI;
    }

    void OnDisable()
    {
        GameManager.OnScoreUpdate -= UpdateScoreUI;
    }

    void UpdateScoreUI(int score)
    {
        _scoreText.text = "SCORE: " + score;
    }
}