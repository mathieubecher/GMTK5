using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager m_instance;
    public static GameManager instance
    {
        get
        {
            if (!m_instance)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }
    #endregion

    public void Awake()
    {
        if(FindObjectsOfType<GameManager>().Length > 1) Destroy(this);
        else DontDestroyOnLoad(this.gameObject);
    }

    public bool isGameRunning => true;

    public void StartGame()
    {
        
    }
    
    public void PauseGame()
    {
        
    }

    public void ResumeGame()
    {
        
    }
}
