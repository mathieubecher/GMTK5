using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haykart : MonoBehaviour
{
    [SerializeField] private float m_speed = 5.0f;

    private Rigidbody m_rigidbody;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }
        
    private void Update()
    {
        if (GameManager.instance.isGameRunning)
            ReadMovement();
        else ResetMovement();
    }

    private void ResetMovement()
    {
        m_rigidbody.velocity = Vector3.zero;
    }

    private void ReadMovement()
    {
        m_rigidbody.velocity = Controller.instance.moveInput * m_speed;
    }
    
    private void OnCollisionEnter(Collision _other)
    {
        Debug.Log("BOOM " + _other.transform.gameObject.name);
        GameManager.instance.PauseGame();
    }
}
