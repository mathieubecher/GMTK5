using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float m_speed = 4.0f;
    [SerializeField] private float m_animSpeed = 10.0f;
    [SerializeField] private MeshRenderer m_renderer;
    [SerializeField] private List<Texture> m_textures;

    private float time = 0.0f;

    void Awake()
    {
        transform.position += -transform.forward * (m_speed * (-transform.position.y - 40.0f) / FrameManager.instance.speed);
    }
    
    void Update()
    {
        if (GameManager.instance.isGameRunning && Haykart.fall)
        {
            time += Time.deltaTime * m_speed;
            m_renderer.material.mainTexture = m_textures[(int)math.floor(time) % m_textures.Count];
            transform.position += transform.forward * (m_speed * Time.deltaTime);
            
        }
    }
}
