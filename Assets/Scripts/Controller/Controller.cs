using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    #region Singleton
    private static Controller m_instance;
    public static Controller instance
    {
        get
        {
            if (!m_instance)
            {
                m_instance = FindObjectOfType<Controller>();
            }
            return m_instance;
        }
    }
    #endregion
    
    [HideInInspector] public Vector2 moveInput;
    
    public delegate void ClickEvent();
    public static event ClickEvent OnClick;
    public static event ClickEvent OnRelease;
    
    public void ReadClickInput(InputAction.CallbackContext _context)
    {
        if (_context.performed)
        {
            OnClick?.Invoke();
        }
        else if (_context.canceled)
        {
            OnRelease?.Invoke();
        }
    }
    public void ReadMoveInput(InputAction.CallbackContext _context)
    {
        moveInput = _context.ReadValue<Vector2>();
    }
}
