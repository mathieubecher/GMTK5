using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDistance : MonoBehaviour
{
    public TextMeshProUGUI m_text;
    private bool DisplayImperial;


    void Start()
    {
        GameObject MetricsInfo = GameObject.Find("MetricsInformation");
        if (MetricsInfo.GetComponent<UI_MetricsInformation>().IsImperial == true)
        {
            DisplayImperial = true;
        } else
        {
            DisplayImperial = false;
        }
    }
    
    void Update()
    {
        if (DisplayImperial == false)
        {
            m_text.text = FrameManager.instance.distance.ToString("#") + " m";
        } else
        {
            m_text.text = (FrameManager.instance.distance / 0.3048f).ToString("#") + " ft";
        }
        
    }
}
