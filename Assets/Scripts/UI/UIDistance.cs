using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDistance : MonoBehaviour
{
    private TextMeshProUGUI m_text;
    
    void Awake()
    {
        m_text = GetComponent<TextMeshProUGUI>();
    }
    
    void Update()
    {
        m_text.text = "Distance : " + FrameManager.instance.distance.ToString("#.00") + " meters";
    }
}
