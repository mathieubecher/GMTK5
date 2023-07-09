using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private float m_height = 0.0f;

    public float height => m_height;
}
