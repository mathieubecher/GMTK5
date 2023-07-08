using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Frame : MonoBehaviour
{
    [SerializeField] private Texture m_texture;
    [SerializeField] private MeshRenderer m_renderer;
    
    // Start is called before the first frame update
    private void Awake()
    {
        m_renderer.material.mainTexture = m_texture;
    }
    
    public void SetTexture()
    {
        m_renderer.sharedMaterial.mainTexture = m_texture;
    }
}

[CustomEditor(typeof(Frame))] class FrameEditor : Editor {
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();
        if (GUILayout.Button("Apply Texture"))
            ((Frame)target).SetTexture();
    }
}