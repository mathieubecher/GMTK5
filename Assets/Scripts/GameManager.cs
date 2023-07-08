using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public void Awake()
    {
        Controller.OnClick += Click;
        Controller.OnRelease += Release;
    }

    private void Click()
    {
        //Debug.Log("Click");
    }

    private void Release()
    {
        //Debug.Log("Release");
    }
}
