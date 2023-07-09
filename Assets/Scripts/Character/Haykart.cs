using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Haykart : MonoBehaviour
{
    public delegate void LifeChangeDelegate(int _remainingLife);
    public static event LifeChangeDelegate OnHitObstacle;
    public static event LifeChangeDelegate OnAddLife;
    
    public delegate void AddScoreDelegate(int _currentScore, int _scoreAdded);
    public static event AddScoreDelegate OnAddScore;
    
    public delegate void SimpleEvent();
    public static event SimpleEvent OnEnterCircle;
    public static event SimpleEvent OnMissCircle;
    
    
    const float TOLERANCE = 0.01f;

    [SerializeField] private int m_lifeAtStart = 2;
    [SerializeField] private int m_maxLife = 6;
    [SerializeField] private List<float> m_speedPerLife;
    [SerializeField] private float m_speed = 5.0f;
    [SerializeField] private Vector2 m_distance;
    private float speed => (m_speed * m_speedPerLife[m_currentLife]);
    
    private Animator m_animator;
    private Rigidbody m_rigidbody;
    private int m_currentLife;
    private int m_score;
    private int m_chain;
    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        GameManager.OnGameStart += GameStart;
        GameManager.OnResumeGame += ResumeGame;
        GameManager.OnGamePause += StopHaykart;
    }
    
    private void Update()
    {
        if (GameManager.instance.isGameRunning)
        {
            ReadMovement();
            DetectRing();
        }
    }

    private void ReadMovement()
    {
        Vector3 moveInput = Controller.instance.moveInput;

        Vector3 desiredVelocity = moveInput * speed;

        if (math.abs(transform.position.x + desiredVelocity.x * Time.deltaTime) > m_distance.x - 1.0f
            && Math.Abs(math.sign(transform.position.x) - math.sign(desiredVelocity.x)) <= TOLERANCE)
        {
            moveInput.x = 0.0f;
        }
        if (math.abs(transform.position.z + desiredVelocity.z * Time.deltaTime) > m_distance.y - 1.0f
            && Math.Abs(math.sign(transform.position.z) - math.sign(desiredVelocity.z)) <= TOLERANCE)
        {
            moveInput.z = 0.0f;
        }
        m_rigidbody.velocity = moveInput * speed;
        
        moveInput.Normalize();
        m_animator.SetFloat("x", moveInput.x);
        m_animator.SetFloat("y", moveInput.z);
    }
    
    private void OnCollisionEnter(Collision _other)
    {
        if (m_rigidbody.isKinematic) return;
        Vector3 position = transform.position;
        position.y = _other.transform.parent.position.y + 0.1f;
        transform.position = position;

        if (_other.gameObject.layer == LayerMask.NameToLayer("Win"))
        {
            StopHaykart();
            m_animator.SetBool("win", true);
            GameManager.instance.WinGame();
        }
        else
        {
            if (m_currentLife > 0 && _other.gameObject.layer != LayerMask.NameToLayer("Loose"))
            {
                m_currentLife--;
                m_chain = 0;
                OnHitObstacle?.Invoke(m_currentLife);
                return;
            }
            StopHaykart();
            m_animator.SetBool("dead", true);
            GameManager.instance.LooseGame();
        }
        
    }

    private void DetectRing()
    {
        Ring ring = FrameManager.instance.GetNextRing();
        if (ring == null || ring.failed || ring.success) return;
        
        if (ring.transform.position.y > -1.0f)
        {
            Vector3 dist = ring.transform.position - transform.position;
            dist.y = 0.0f;
            if(dist.magnitude < 2.0f)
            {
                OnEnterCircle?.Invoke();
                ring.success = true;
                m_chain++;
                
                if (math.floor((m_score + 5 * m_chain) / 100.0) > math.floor(m_score / 100.0) && m_currentLife < m_maxLife)
                {
                    m_currentLife++;
                    OnAddLife?.Invoke(m_currentLife);
                }
                
                OnAddScore?.Invoke(m_score, 5 * m_chain);
                m_score += 5 * m_chain;
                
            }
            else
            {
                OnMissCircle?.Invoke();
                ring.failed = true;
                m_chain = math.max(m_chain - 1, 0); 
            }
        }
    }
    
    private void StopHaykart()
    {
        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.isKinematic = true;
        m_animator.SetFloat("x", 0.0f);
        m_animator.SetFloat("y", 0.0f);
    }

    private void GameStart()
    {
        transform.position = Vector3.zero;
        ResumeGame();
    }

    private void ResumeGame()
    {
        m_rigidbody.isKinematic = false;
        m_animator.SetBool("dead", false);
        m_animator.SetBool("win", false);
        m_currentLife = m_lifeAtStart;
    }
}
