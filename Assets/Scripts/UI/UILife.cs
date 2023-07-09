using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UILife : MonoBehaviour
{
    private TextMeshProUGUI m_text;
    private int m_chain = 0;
    
    void Awake()
    {
        m_text = GetComponent<TextMeshProUGUI>();
        GameManager.OnGameStart += ResetLife;
        Haykart.OnAddLife += OnLifeChange;
        Haykart.OnHitObstacle += OnLifeChange;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStart -= ResetLife;
        Haykart.OnAddLife -= OnLifeChange;
        Haykart.OnHitObstacle -= OnLifeChange;
    }
    
    private void ResetLife()
    {
        m_text.text = "Life : 2";
    }
    
    private void OnLifeChange(int _remaininglife)
    {
        m_text.text = "Score : " + _remaininglife;
    }
}
