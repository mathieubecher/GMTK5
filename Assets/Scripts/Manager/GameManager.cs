using System;
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
    
    public delegate void SimpleEvent();
    public static event SimpleEvent OnGameStart;
    public static event SimpleEvent OnGamePause;
    public static event SimpleEvent OnGameLoose;
    public static event SimpleEvent OnGameWin;
    public static event SimpleEvent OnResumeGame;
    
    
    private bool m_isGameRunning = true;
    public bool isGameRunning => m_isGameRunning;
    public void Awake()
    {
        if(FindObjectsOfType<GameManager>().Length > 1) Destroy(this);
        else
        {
            Controller.OnSpace += StartGame;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void Start()
    {
        StartGame();
    }
    
    public void StartGame()
    {
        OnGameStart?.Invoke();
        m_isGameRunning = true;
    }
    
    public void PauseGame()
    {
        OnGamePause?.Invoke();
        m_isGameRunning = false;
    }

    public void LooseGame()
    {
        OnGameLoose?.Invoke();
        m_isGameRunning = false;
    }

    public void WinGame()
    {
        OnGameWin?.Invoke();
        m_isGameRunning = false;
    }

    public void ResumeGame()
    {
        OnResumeGame?.Invoke();
        m_isGameRunning = true;
    }
}
