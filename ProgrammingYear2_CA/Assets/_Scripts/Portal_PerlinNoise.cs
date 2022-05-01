using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_PerlinNoise : MonoBehaviour
{
    public float scale = 0.25f;
    public float waveSpeed = 1.0f;
    public float waveHeight = 0.33f;

    Vector3[] verts;
    Color[] colors;

    private void Update()
    {
        CalcNoise();
    }

    void CalcNoise()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        
        verts = mf.mesh.vertices;
        colors = new Color[verts.Length];

        for (int i=0; i < verts.Length; i++)
        {
            float pX = (verts[i].x * scale) + (Time.time * waveSpeed);
            float pZ = (verts[i].z * scale) + (Time.time * waveSpeed);
            // Update the Y component of each vertices using perlin noise 
            verts[i].y = Mathf.PerlinNoise(pX, pZ) * waveHeight;
            colors[i] = Color.Lerp(Color.cyan, Color.white, verts[i].y);
        }

        // Update the vertices 
        mf.mesh.vertices = verts;
        mf.mesh.colors = colors;
        // Recalculate normals as triangles have changed 
        mf.mesh.RecalculateNormals();
        // Recalculate bounds as triangles have changed 
        mf.mesh.RecalculateBounds();
    }
}
