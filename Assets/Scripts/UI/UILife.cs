using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILife : MonoBehaviour
{
    private TextMeshProUGUI m_text;
    private int m_chain = 0;
    public Image Mask;
    public Image Gauge;

    void Awake()
    {
        //m_text = GetComponent<TextMeshProUGUI>();
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
        //m_text.text = "Life : 2";
        Mask.rectTransform.sizeDelta = new Vector2((448/6) * 2, 64);
        Gauge.color = new Color32(200, 166, 82, 255);
    }
    
    private void OnLifeChange(int _remainingLife)
    {
        //m_text.text = "Life : " + _remainingLife;
        Mask.rectTransform.sizeDelta = new Vector2((448 / 6) * _remainingLife, 64);
        if (_remainingLife > 1)
        {
            Gauge.color = new Color32(200, 166, 82, 255);
        } else
        {
            Gauge.color = new Color32(210, 0, 0, 255);
        }
    }
}
