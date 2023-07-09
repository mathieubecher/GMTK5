using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class UIChain : MonoBehaviour
{
    private TextMeshProUGUI m_text;
    private int m_chain = 0;
    
    void Awake()
    {
        m_text = GetComponent<TextMeshProUGUI>();
        GameManager.OnGameStart += ResetChain;
        Haykart.OnMissCircle += MissCircle;
        Haykart.OnEnterCircle += EnterCircle;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStart -= ResetChain;
        Haykart.OnMissCircle -= MissCircle;
        Haykart.OnEnterCircle -= EnterCircle;
    }
    
    private void ResetChain()
    {
        m_chain = 0;
    }

    private void EnterCircle()
    {
        m_chain++;
    }

    private void MissCircle()
    {
        m_chain = math.max(m_chain - 1, 0);
    }

    void Update()
    {
        m_text.text = "Chain : " + m_chain;
    }
}
