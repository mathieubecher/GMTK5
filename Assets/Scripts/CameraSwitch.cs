using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.OnGameStart += SwitchToIntro;
        Haykart.OnStartFalling += SwitchToMain;
    }

    private void SwitchToMain()
    {
        GetComponent<CinemachineVirtualCamera>().Priority = 0;
        var brain = Camera.main.GetComponent<CinemachineBrain>();
        brain.m_DefaultBlend.m_Time = 2.0f;
    }

    private void SwitchToIntro()
    {
        GetComponent<CinemachineVirtualCamera>().Priority = 40;
        var brain = Camera.main.GetComponent<CinemachineBrain>();
        brain.m_DefaultBlend.m_Time = 0.0f;
    }

}
