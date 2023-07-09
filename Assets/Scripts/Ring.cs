using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public bool success = false;
    public bool failed = false;

    private Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }
    
    public void Success()
    {
        success = true;
        m_animator.SetTrigger("Success");
    }

    public void Fail()
    {
        failed = true;
        m_animator.SetTrigger("Fail");
    }
}
