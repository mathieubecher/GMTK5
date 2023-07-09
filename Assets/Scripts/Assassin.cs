using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : MonoBehaviour
{
    [SerializeField] private Texture m_idle;
    [SerializeField] private Texture m_dead;
    [SerializeField] private MeshRenderer m_renderer;

    private void Awake()
    {
        if(m_renderer) m_renderer.material.mainTexture = m_idle;
        GameManager.OnGameWin += Dead;
    }

    private void OnDestroy()
    {
        GameManager.OnGameWin -= Dead;
    }

    private void Dead()
    {
        if(m_renderer) m_renderer.material.mainTexture = m_dead;
    }
}
