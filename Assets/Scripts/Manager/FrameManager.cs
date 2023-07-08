using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FrameManager : MonoBehaviour
{
    
    [SerializeField] private float m_speed = 5.0f;
    [SerializeField] private float m_mettersPerFrame = 2.0f;
    [SerializeField] private float m_maxPositiveDistance = 10.0f;
    [SerializeField] private int m_maxFrame = 10;
    [SerializeField] private int m_spaceAtBegining = 2;
    [SerializeField] private List<GameObject> m_frameObjects;

    private List<Transform> m_frames;
    private float m_distance = 0.0f;

    public float distance => m_distance;

    public void Awake()
    {
        GameManager.OnGameStart += GameStart;
    }

    public void Update()
    {
        if (GameManager.instance.isGameRunning)
            UpdateFrames();
    }

    private void UpdateFrames()
    {
        m_frames.RemoveAll(frame => frame == null);
        
        float deltaPos = Time.deltaTime * m_speed;

        int i = (int)math.floor((m_distance + deltaPos) / m_mettersPerFrame);
        
        if (i > math.floor(m_distance / m_mettersPerFrame))
        {
            GameObject frame = Instantiate(m_frameObjects[(i + m_maxFrame) % m_frameObjects.Count], Vector3.down * (m_maxFrame * m_mettersPerFrame), quaternion.identity);
            frame.transform.SetParent(transform);
            m_frames.Add(frame.transform);
        }
        
        foreach (var frame in m_frames)
        {
            frame.position += Vector3.up * deltaPos;
            if(frame.position.y > m_maxPositiveDistance) Destroy(frame.gameObject);
        }
        m_distance += deltaPos;
    }

    private void GameStart()
    {
        m_distance = 0.0f;
        if (m_frames != null)
        {
            foreach (var frame in m_frames)
            {
                Destroy(frame.gameObject);
            }
        }
        
        m_frames = new List<Transform>();
        for (int i = m_spaceAtBegining; i <= m_maxFrame; ++i)
        {
            GameObject frame = Instantiate(m_frameObjects[i % m_frameObjects.Count], Vector3.down * (i * m_mettersPerFrame), quaternion.identity);
            frame.transform.SetParent(transform);
            m_frames.Add(frame.transform);
        }
    }

}
