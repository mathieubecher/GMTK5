using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousMoveUp : MonoBehaviour
{
    private Vector3 origin;
    void Awake()
    {
        origin = transform.position;
        GameManager.OnGameStart += Reset;
    }

    private void Reset()
    {
        transform.position = origin;
    }

    void Update()
    {
        if (GameManager.instance.isGameRunning && Haykart.fall)
        {
            transform.position += Vector3.up * Time.deltaTime * 40.0f;
        }
    }

}