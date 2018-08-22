using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Strand = System.Collections.Generic.List<Vertex>;
using Hair = System.Collections.Generic.List<System.Collections.Generic.List<Vertex>>;

public static class Visualizer
{
    public static Hair hair;
    private static Mesh mesh;

    public static void Init(MeshFilter mf)
    {
        mesh = new Mesh();
        mf.sharedMesh = mesh;
    }

    public static void Update(Hair h)
    {
        hair = h;
        DrawHair();
    }

    private static void DrawHair()
    {
        Hair h = hair;
        int nVert = h[0].Count;
        Vector3[] vertices = new Vector3[h.Count * nVert];
        int[] lines = new int[h.Count * (nVert - 1) * 2];
        for (int s = 0; s < hair.Count; s++)
        {
            int v0 = s * nVert;
            int l0 = s * (nVert - 1) * 2;
            Strand strand = hair[s];
            vertices[v0] = strand[0].Position;
            for (int v = 1; v < nVert; v++)
            {
                vertices[v0 + v] = strand[1].Position;
                lines[l0 + v * 2 - 2] = v0 + v - 1;
                lines[l0 + v * 2 - 1] = v0 + v;
            }
        }
        mesh.vertices = vertices;
        mesh.SetIndices(lines, MeshTopology.Lines, 0, true, 0);
    }
}
