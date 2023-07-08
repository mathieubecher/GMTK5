using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Haykart : MonoBehaviour
{
    const float TOLERANCE = 0.01f;
    
    [SerializeField] private float m_speed = 5.0f;
    [SerializeField] private Vector2 m_distance;

    private Animator m_animator;
    private Rigidbody m_rigidbody;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        GameManager.OnGameStart += GameStart;
    }
    
    private void Update()
    {
        if (GameManager.instance.isGameRunning) ReadMovement();
        else ResetMovement();
    }
    
    private void ResetMovement()
    {
        m_rigidbody.velocity = Vector3.zero;
    }

    private void ReadMovement()
    {
        Vector3 moveInput = Controller.instance.moveInput;

        Vector3 desiredVelocity = moveInput * m_speed;

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
        m_rigidbody.velocity = moveInput * m_speed;
        
        moveInput.Normalize();
        m_animator.SetFloat("x", moveInput.x);
        m_animator.SetFloat("y", moveInput.z);
    }
    
    private void OnCollisionEnter(Collision _other)
    {
        Vector3 position = transform.position;
        position.y = _other.transform.parent.position.y + 0.1f;
        transform.position = position;
        
        Loose();
    }

    private void Loose()
    {
        ResetMovement();
        m_rigidbody.isKinematic = true;
        GameManager.instance.PauseGame();
        
        m_animator.SetFloat("x", 0.0f);
        m_animator.SetFloat("y", 0.0f);
        m_animator.SetBool("dead", true);
    }

    private void GameStart()
    {
        m_rigidbody.isKinematic = false;
        m_animator.SetBool("dead", false);
        
    }
}
