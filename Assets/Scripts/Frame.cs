using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Frame : MonoBehaviour
{
    [SerializeField] private Texture m_texture;
    [SerializeField] private MeshRenderer m_renderer;
    
    // Start is called before the first frame update
    private void Awake()
    {
        m_renderer.material.mainTexture = m_texture;
    }
    
#if UNITY_EDITOR
    public void SetTexture()
    {
        m_renderer.sharedMaterial.mainTexture = m_texture;
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(Frame))] class FrameEditor : Editor {
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        base.OnInspectorGUI();
        if (GUILayout.Button("Apply Texture"))
            ((Frame)target).SetTexture();
    }
}
#endif