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
    
    [HideInInspector] public Vector3 moveInput;
    
    public delegate void ClickEvent();
    public static event ClickEvent OnClick;
    public static event ClickEvent OnRelease;
    
    public delegate void SimpleEvent();
    public static event SimpleEvent OnSpace;
    
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
        Vector2 input = _context.ReadValue<Vector2>();
        moveInput = new Vector3(input.x, 0.0f, input.y);
    }
    
    public void ReadSpaceInput(InputAction.CallbackContext _context)
    {
        if (_context.performed)
        {
            OnSpace?.Invoke();
        }
    }
}
