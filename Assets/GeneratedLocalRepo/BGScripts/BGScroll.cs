using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    public static float scroll_Speed = 1f;
    private MeshRenderer mesh_Renderer;

    void Awake()
    {
        mesh_Renderer = GetComponent<MeshRenderer>();
    }
    void Update()
    {
        scroll_Speed += 0.00005f;
        Scroll();
    }
    void Scroll()
    {
        Vector2 offset = mesh_Renderer.sharedMaterial.GetTextureOffset("_MainTex");
        offset.y += Time.deltaTime * scroll_Speed;
        mesh_Renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
