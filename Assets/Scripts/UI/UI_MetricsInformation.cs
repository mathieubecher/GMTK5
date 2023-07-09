using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MetricsInformation : MonoBehaviour
{
    #region Singleton
    private static UI_MetricsInformation m_instance;
    public static UI_MetricsInformation instance
    {
        get
        {
            if (!m_instance)
            {
                m_instance = FindObjectOfType<UI_MetricsInformation>();
            }
            return m_instance;
        }
    }
    #endregion

    public bool IsImperial = false;


    private void Awake()
    {
        if (FindObjectsOfType<UI_MetricsInformation>().Length > 1) Destroy(this);
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
