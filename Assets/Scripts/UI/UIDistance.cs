using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDistance : MonoBehaviour
{
    private TextMeshProUGUI m_text;
    private FrameManager m_frameManager;
    
    void Awake()
    {
        m_text = GetComponent<TextMeshProUGUI>();
        m_frameManager = FindObjectOfType<FrameManager>();
    }
    
    void Update()
    {
        m_text.text = "Distance : " + m_frameManager.distance.ToString("#.00") + " meters";
    }
}
