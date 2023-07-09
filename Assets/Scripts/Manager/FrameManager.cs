using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class FrameManager : MonoBehaviour
{
    
    [SerializeField] private float m_speed = 5.0f;
    
    [Header("Frame")]
    [SerializeField] private int m_maxFrame = 10;
    [SerializeField] private int m_spaceAtBegining = 2;
    [SerializeField] private float m_mettersPerFrame = 2.0f;
    [SerializeField] private float m_maxPositiveDistance = 10.0f;
    [Space]
    [SerializeField] private List<GameObject> m_frameObjects;

    [Header("Facade")]
    [SerializeField] private Vector2 m_facadeDistance;
    [SerializeField] private float m_maxPositiveFacadeDistance = 20.0f;
    [SerializeField] private float m_leftStartDistance = 0.0f;
    [SerializeField] private float m_rightStartDistance = 0.0f;
    [SerializeField] private float m_downStartDistance = 0.0f;
    [SerializeField] private float m_upStartDistance = 0.0f;
    [Space]
    [SerializeField] private List<GameObject> m_sideFacadeObjects;
    [SerializeField] private List<GameObject> m_frontFacadeObjects;

    [Header("End")] 
    [SerializeField] private float m_distanceToReach = 10.0f;
    [SerializeField] private float m_spaceAtEnd = 10.0f;
    [SerializeField] private GameObject m_endObject;

    [Header("Spawn Parent")] 
    [SerializeField] private Transform m_framesParent;
    [SerializeField] private Transform m_upWallsParent;
    [SerializeField] private Transform m_downWallsParent;
    [SerializeField] private Transform m_leftWallsParent;
    [SerializeField] private Transform m_rightWallsParent;

    private float m_distance = 0.0f;
    private List<Transform> m_frames;
    
    private List<Transform> m_walls;
    private float m_leftCurrDistance = 0.0f;
    private float m_rightCurrDistance = 0.0f;
    private float m_downCurrDistance = 0.0f;
    private float m_upCurrDistance = 0.0f;

    public float distance => m_distance;

    public void Awake()
    {
        GameManager.OnGameStart += GameStart;
        GameManager.OnGameWin += GameWin;
    }

    public void Update()
    {
        if (GameManager.instance.isGameRunning)
        {
            UpdateWalls();
            UpdateFrames();
        }
    }
    
    private void GameStart()
    {
        m_distance = 0.0f;
        if (m_frames != null)
        {
            m_frames.RemoveAll(frame => frame == null);
            foreach (var frame in m_frames)
            {
                Destroy(frame.gameObject);
            }
        }
        
        m_frames = new List<Transform>();
        
        AddFrame(m_endObject, m_distanceToReach);
        
        for (int i = m_spaceAtBegining; i <= m_maxFrame; ++i)
        {
            float dist = i * m_mettersPerFrame;
            if(dist < m_distanceToReach - m_spaceAtEnd) AddFrame(m_frameObjects[i % m_frameObjects.Count], dist);
        }
        
        
        if (m_walls != null)
        {
            m_walls.RemoveAll(wall => wall == null);
            foreach (var wall in m_walls)
            {
                Destroy(wall.gameObject);
            }
        }
        
        m_leftCurrDistance = m_leftStartDistance;
        m_rightCurrDistance = m_rightStartDistance;
        m_downCurrDistance = m_downStartDistance;
        m_upCurrDistance = m_upStartDistance;
        
        m_walls = new List<Transform>();
    }
    
    private void UpdateFrames()
    {
        m_frames.RemoveAll(frame => frame == null);
        
        float deltaPos = Time.deltaTime * m_speed;

        int i = (int)math.floor((m_distance + deltaPos) / m_mettersPerFrame);
        
        if (i > math.floor(m_distance / m_mettersPerFrame))
        {
            float dist = m_maxFrame * m_mettersPerFrame;
            if(m_distance + dist < m_distanceToReach - m_spaceAtEnd) AddFrame(m_frameObjects[(i + m_maxFrame) % m_frameObjects.Count], dist);
        }
        
        foreach (var frame in m_frames)
        {
            frame.position += Vector3.up * deltaPos;
            if(frame.position.y > m_maxPositiveDistance) Destroy(frame.gameObject);
        }
        m_distance += deltaPos;
    }

    private void UpdateWalls()
    {
        m_walls.RemoveAll(frame => frame == null);
        
        float deltaPos = Time.deltaTime * m_speed;
        
        // FRONT
        while (m_distance + deltaPos + m_maxFrame * m_mettersPerFrame > m_upCurrDistance)
        {
            GameObject facadeObject = m_frontFacadeObjects[Random.Range(0, m_frontFacadeObjects.Count)];
            m_upCurrDistance += facadeObject.GetComponent<Wall>().height;
            
            GameObject facade = Instantiate(facadeObject, 
                Vector3.down * (m_upCurrDistance - m_distance) + Vector3.forward * m_facadeDistance.y, quaternion.identity);
            facade.transform.SetParent(m_upWallsParent);
            m_walls.Add(facade.transform);
        }
        while (m_distance + deltaPos + m_maxFrame * m_mettersPerFrame > m_downCurrDistance)
        {
            GameObject facadeObject = m_frontFacadeObjects[Random.Range(0, m_frontFacadeObjects.Count)];
            m_downCurrDistance += facadeObject.GetComponent<Wall>().height;
            
            GameObject facade = Instantiate(facadeObject, 
                Vector3.down * (m_downCurrDistance - m_distance) + Vector3.back * m_facadeDistance.y, Quaternion.Euler(0.0f, 180.0f, 0.0f));
            facade.transform.SetParent(m_downWallsParent);
            m_walls.Add(facade.transform);
        }
        
        // SIDE
        while (m_distance + deltaPos + m_maxFrame * m_mettersPerFrame > m_leftCurrDistance)
        {
            GameObject facadeObject = m_sideFacadeObjects[Random.Range(0, m_sideFacadeObjects.Count)];
            m_leftCurrDistance += facadeObject.GetComponent<Wall>().height;
            
            GameObject facade = Instantiate(facadeObject, 
                Vector3.down * (m_leftCurrDistance - m_distance) + Vector3.left * m_facadeDistance.x, Quaternion.Euler(0.0f, 180.0f, 0.0f));
            facade.transform.SetParent(m_leftWallsParent);
            m_walls.Add(facade.transform);
        }
        while (m_distance + deltaPos + m_maxFrame * m_mettersPerFrame > m_rightCurrDistance)
        {
            GameObject facadeObject = m_sideFacadeObjects[Random.Range(0, m_sideFacadeObjects.Count)];
            m_rightCurrDistance += facadeObject.GetComponent<Wall>().height;
            
            GameObject facade = Instantiate(facadeObject, 
                Vector3.down * (m_rightCurrDistance - m_distance) + Vector3.right * m_facadeDistance.x, quaternion.identity);
            facade.transform.SetParent(m_rightWallsParent);
            m_walls.Add(facade.transform);
        }

        foreach (var wall in m_walls)
        {
            wall.position += Vector3.up * deltaPos;
            if(wall.position.y > m_maxPositiveFacadeDistance) Destroy(wall.gameObject);
        }
    }

    private void AddFrame(GameObject _frame, float _dist)
    {
        GameObject frame = Instantiate(_frame, Vector3.down * _dist, quaternion.identity);
        frame.transform.SetParent(m_framesParent);
        m_frames.Add(frame.transform);
    }

    private void GameWin()
    {
        m_distance = m_distanceToReach;
    }
}
