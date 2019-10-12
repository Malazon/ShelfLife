using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class PanTexture: MonoBehaviour
{
    // Scroll main texture based on time

    public float MainScrollSpeed = 0.5f;

    public float EmmisScrollSpeed = 0.5f;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float MainOffset = Time.time * MainScrollSpeed;
        float EmissOffset = Time.time * EmmisScrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(MainOffset, 0));
        rend.material.SetTextureOffset("_EmissionMap", new Vector2(EmissOffset, 0));
    }
}