using UnityEngine;

#if UNITY_EDITOR
[ExecuteInEditMode]
#endif

public class EditorOnlyObject : MonoBehaviour
{
    void Awake()
    {
#if !UNITY_EDITOR
        var renderer = GetComponent<Renderer>();
        renderer.enabled = true;
        if (renderer != null)
        {
            Material mat = new Material(Shader.Find("Standard"));
            mat.color = new Color(1, 0, 0, 0.3f);
            renderer.material = mat;
        }
#endif
    }

}
