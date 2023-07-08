using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haykart : MonoBehaviour
{
    [SerializeField] private float m_speed = 5.0f;

    private Animator m_animator;
    private Rigidbody m_rigidbody;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
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
        m_rigidbody.velocity = moveInput * m_speed;
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
}
