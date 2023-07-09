using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    private TextMeshProUGUI m_text;
    
    void Awake()
    { 
        m_text = GetComponent<TextMeshProUGUI>();
        GameManager.OnGameStart += ResetScore;
        Haykart.OnAddScore += AddScore;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStart -= ResetScore;
        Haykart.OnAddScore -= AddScore;
    }

    private void AddScore(int _currentScore, int _scoreAdded)
    {
        m_text.text = "Score : " + (_currentScore + _scoreAdded);
    }

    private void ResetScore()
    {
        m_text.text = "Score : 0";
    }

}
