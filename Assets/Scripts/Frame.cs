using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Frame : MonoBehaviour
{
    [SerializeField] private Texture m_texture;
    [SerializeField] private MeshRenderer m_renderer;
    [SerializeField] private List<Transform> m_poi;
    [SerializeField] private Vector2 m_distance;
    
    // Start is called before the first frame update
    private void Awake()
    {
        if(m_renderer) m_renderer.material.mainTexture = m_texture;
    }
    
#if UNITY_EDITOR
    public void SetTexture()
    {
        if(m_renderer) m_renderer.sharedMaterial.mainTexture = m_texture;
    }
#endif
    public Vector3 GetRandomPOI()
    {
        if (m_poi.Count > 0)
        {
            return m_poi[Random.Range(0, m_poi.Count)].position;
        }
        return transform.position + new Vector3(Random.Range(-m_distance.x, m_distance.x), 0.0f, Random.Range(-m_distance.y, m_distance.y));
    }
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