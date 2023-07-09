using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EndScript : MonoBehaviour
{
    [SerializeField] private Transform m_assassinPos;
    [SerializeField] private Vector2 m_distance;
    
    private void Awake()
    {
        Vector3 position = m_assassinPos.localPosition;
        position.x = Random.Range(- m_distance.x + 2.0f, m_distance.x - 2.0f);
        position.z = Random.Range(- m_distance.y + 2.0f, m_distance.y - 2.0f);
        Debug.Log(position);
        m_assassinPos.localPosition = position;
    }
}
