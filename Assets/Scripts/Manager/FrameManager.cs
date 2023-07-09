using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class FrameManager : MonoBehaviour
{
    #region Singleton
    private static FrameManager m_instance;
    public static FrameManager instance
    {
        get
        {
            if (!m_instance)
            {
                m_instance = FindObjectOfType<FrameManager>();
            }
            return m_instance;
        }
    }
    #endregion
    [Serializable] struct ObjectsPerDistance
    {
        public float distance;
        public List<GameObject> objects;
    }
    
    [SerializeField] private float m_speed = 5.0f;
    
    [Header("Frame")]
    [SerializeField] private int m_maxFrame = 10;
    [SerializeField] private int m_spaceAtBegining = 2;
    [SerializeField] private float m_mettersPerFrame = 2.0f;
    [SerializeField] private float m_maxPositiveDistance = 10.0f;
    [Space]
    [SerializeField] private List<ObjectsPerDistance> m_frameObjects;
    [SerializeField] private GameObject m_circleObject;

    [Header("Facade")]
    [SerializeField] private Vector2 m_facadeDistance;
    [SerializeField] private float m_maxPositiveFacadeDistance = 20.0f;
    [SerializeField] private float m_leftStartDistance = 0.0f;
    [SerializeField] private float m_rightStartDistance = 0.0f;
    [SerializeField] private float m_downStartDistance = 0.0f;
    [SerializeField] private float m_upStartDistance = 0.0f;
    [Space]
    [SerializeField] private List<ObjectsPerDistance> m_leftFacadeObjects;
    [SerializeField] private List<ObjectsPerDistance> m_rightFacadeObjects;
    [SerializeField] private List<ObjectsPerDistance> m_upFacadeObjects;
    [SerializeField] private List<ObjectsPerDistance> m_downFacadeObjects;

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
    private Frame m_lastFrame;
    private int m_lastGen = -1;
    
    private List<Transform> m_walls;
    private List<Transform> m_circles;
    private int m_lastLeftGen = -1;
    private float m_leftCurrDistance = 0.0f;
    private int m_lastRightGen = -1;
    private float m_rightCurrDistance = 0.0f;
    private int m_lastDownGen = -1;
    private float m_downCurrDistance = 0.0f;
    private int m_lastUpGen = -1;
    private float m_upCurrDistance = 0.0f;

    public float distance => m_distance;

    public void Awake()
    {
        GameManager.OnGameStart += GameStart;
        GameManager.OnGameWin += GameWin;
        
        m_frames = new List<Transform>();
        m_walls = new List<Transform>();
        m_circles = new List<Transform>();
    }

    public void Update()
    {
        if (GameManager.instance.isGameRunning)
        {
            UpdateWalls();
            UpdateFrames();
            UpdateCircles();
        }
    }
    
    public Ring GetNextRing()
    { 
        var circle = m_circles.Find(x => x.transform.position.y < 5.0f);
        
        return !circle? null : circle.GetComponent<Ring>();
    }

    private void GameStart()
    {
        m_leftCurrDistance = m_leftStartDistance;
        m_rightCurrDistance = m_rightStartDistance;
        m_downCurrDistance = m_downStartDistance;
        m_upCurrDistance = m_upStartDistance;

        m_lastGen = -1;
        m_lastUpGen = -1;
        m_lastDownGen = -1;
        m_lastLeftGen = -1;
        m_lastRightGen = -1;
        
        m_distance = 0.0f;

        if (m_circles != null)
        {
            m_circles.RemoveAll(wall => wall == null);
            foreach (var circle in m_circles)
            {
                Destroy(circle.gameObject);
            }
        }
        m_circles = new List<Transform>();
        
        if (m_frames != null)
        {
            m_frames.RemoveAll(frame => frame == null);
            foreach (var frame in m_frames)
            {
                Destroy(frame.gameObject);
            }
        }
        
        m_frames = new List<Transform>();
        m_lastFrame = null;
        
        AddEnd(m_distanceToReach);
        
        for (int i = m_spaceAtBegining; i <= m_maxFrame; ++i)
        {
            float dist = i * m_mettersPerFrame;
            if(dist < m_distanceToReach - m_spaceAtEnd) AddFrame(dist);
        }
        
        
        if (m_walls != null)
        {
            m_walls.RemoveAll(wall => wall == null);
            foreach (var wall in m_walls)
            {
                Destroy(wall.gameObject);
            }
        }
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
            if(m_distance + dist < m_distanceToReach - m_spaceAtEnd) AddFrame(dist);
        }
        
        foreach (var frame in m_frames)
        {
            frame.position += Vector3.up * deltaPos;
            if(frame.position.y > m_maxPositiveDistance) Destroy(frame.gameObject);
        }
        m_distance += deltaPos;
    }

    private void UpdateCircles()
    {
        m_circles.RemoveAll(frame => frame == null);
        
        float deltaPos = Time.deltaTime * m_speed;
        foreach (var circle in m_circles)
        {
            circle.position += Vector3.up * deltaPos;
            if(circle.position.y > m_maxPositiveFacadeDistance) Destroy(circle.gameObject);
        }
    }
    
    private void UpdateWalls()
    {
        m_walls.RemoveAll(frame => frame == null);
        
        float deltaPos = Time.deltaTime * m_speed;
        
        // FRONT
        while (m_distance + deltaPos + m_maxFrame * m_mettersPerFrame > m_upCurrDistance)
        {
            GenerateWall(ref m_lastUpGen, ref m_upCurrDistance, m_upFacadeObjects, 
                Vector3.forward * m_facadeDistance.y, Quaternion.identity);
        }
        while (m_distance + deltaPos + m_maxFrame * m_mettersPerFrame > m_downCurrDistance)
        {
            GenerateWall(ref m_lastDownGen, ref m_downCurrDistance, m_downFacadeObjects,
                Vector3.back * m_facadeDistance.y, Quaternion.Euler(0.0f, 180.0f, 0.0f));
        }
        
        // SIDE
        while (m_distance + deltaPos + m_maxFrame * m_mettersPerFrame > m_leftCurrDistance)
        {
            GenerateWall(ref m_lastLeftGen, ref m_leftCurrDistance, m_leftFacadeObjects,
                Vector3.left * m_facadeDistance.x, Quaternion.Euler(0.0f, 180.0f, 0.0f));
        }
        while (m_distance + deltaPos + m_maxFrame * m_mettersPerFrame > m_rightCurrDistance)
        {
            GenerateWall(ref m_lastRightGen, ref m_rightCurrDistance, m_rightFacadeObjects,
                Vector3.right * m_facadeDistance.x, quaternion.identity);
        }

        foreach (var wall in m_walls)
        {
            wall.position += Vector3.up * deltaPos;
            if(wall.position.y > m_maxPositiveFacadeDistance) Destroy(wall.gameObject);
        }
    }

    private void GenerateWall(ref int _lastGen, ref float _currDistance, List<ObjectsPerDistance> _objects, Vector3 _deltaPos, Quaternion _rotation)
    {
        int gen = _lastGen;

        float distance = _currDistance;
        ObjectsPerDistance objectsPerDistance = _objects.FindLast(x => distance >= x.distance);
        while (gen == _lastGen && (objectsPerDistance.objects.Count > 1 || gen < 0))
        {
            gen = Random.Range(0, objectsPerDistance.objects.Count);
        }
        _lastGen = gen;
            
        GameObject facadeObject = objectsPerDistance.objects[gen];
        _currDistance += facadeObject.GetComponent<Wall>().height;
            
        GameObject facade = Instantiate(facadeObject, 
            Vector3.down * (_currDistance - m_distance) + _deltaPos, _rotation);
        facade.transform.SetParent(m_upWallsParent);
        m_walls.Add(facade.transform);
    }
    
    private void AddFrame(float _dist)
    {
        ObjectsPerDistance objectsPerDistance = m_frameObjects.FindLast(x => _dist + m_distance >= x.distance);
        int gen = m_lastGen;
        while (gen == m_lastGen && (objectsPerDistance.objects.Count > 1 || gen < 0))
        {
            gen = Random.Range(0, objectsPerDistance.objects.Count);
        }
        m_lastGen = gen;   
        GameObject frameInstance = Instantiate(objectsPerDistance.objects[gen], Vector3.down * _dist, quaternion.identity);
        Frame frame = frameInstance.GetComponent<Frame>();
        frame.transform.SetParent(m_framesParent);

        if (m_lastFrame)
        {
            Vector3 circlePosition = Vector3.Lerp(m_lastFrame.GetRandomPOI(), frame.GetRandomPOI(), 0.5f);
            GameObject circleInstance = Instantiate(m_circleObject, circlePosition, quaternion.identity);
            m_circles.Add(circleInstance.transform);
        }
        
        m_frames.Add(frame.transform);
        m_lastFrame = frame;
    }

    private void AddEnd(float _dist)
    {
        GameObject frame = Instantiate(m_endObject, Vector3.down * _dist, quaternion.identity);
        frame.transform.SetParent(m_framesParent);
        m_frames.Add(frame.transform);
    }

    private void GameWin()
    {
        m_distance = m_distanceToReach;
    }
}
