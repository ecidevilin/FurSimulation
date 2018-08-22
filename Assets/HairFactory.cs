using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Strand = System.Collections.Generic.List<Vertex>;
using Hair = System.Collections.Generic.List<System.Collections.Generic.List<Vertex>>;

public static class HairFactory
{
    public static Hair GrowHair(int nStrands, int nVert)
    {
        Hair hair = new Hair(nStrands);
        Vector3 startPos = Vector3.zero;
        float len = 50f;
        Vertex.L0 = len/nVert;
        for (int s = 0; s < nStrands; s++)
        {
            Strand strand = new Strand(nVert);
            hair.Add(strand);
            Vertex particle = new Vertex(startPos, 1.0f);
            for (int v = 0; v < nVert; v++)
            {
                Vector3 p = particle.Position;
                p.y += Vertex.L0;
                particle.Position = p;
                strand.Add(particle);
            }
            startPos.x += 0.0007f;
        }
        return hair;
    }

    public static Hair GrowHairOnPlane(int nStrands, int nVert, float interDis, float xDis)
    {

        Hair hair = new Hair(nStrands);
        Vector3 startPos = Vector3.zero;
        int layers = 2;

        Vertex.L0 = interDis;

        for (int l = 0; l < layers; l++)
        {
            startPos.x = -0.8f + (l*0.015f/layers);
            startPos.z = l*0.5f;

            for (int s = 0; s < nStrands; s++)
            {
                Strand strand = new Strand(nVert);
                hair.Add(strand);
                Vertex particle = new Vertex(startPos, 1.0f);
                for (int v = 0; v < nVert; v++)
                {
                    Vector3 p = particle.Position;
                    p.y += Vertex.L0;
                    particle.Position = p;
                    strand.Add(particle);
                }
                startPos.x += xDis;
            }
        }
        return hair;
    }
    public static Hair GrowHairOnCircle(int nStrands, int nVert, float interDis, float radius)
    {
        Hair hair = new Hair(nStrands);
        Vector3 startPos = Vector3.zero;
        Vertex.L0 = interDis;
        for (int s = 0; s < nStrands; s++)
        {
            Strand strand = new Strand(nVert);
            hair.Add(strand);
            float sigma = 2 * nStrands * Mathf.PI;
            Vertex particle = new Vertex(startPos, 1.0f);
            Vector3 pos = startPos + radius * new Vector3(Mathf.Cos(sigma * s), 0, Mathf.Sin(sigma * s));
            for (int v = 0; v < nVert; v++)
            {
                pos.y += interDis * v;
                particle.Position = pos;
                strand.Add(particle);
            }
        }
        return hair;
    }

    public static Hair GrowHairOnSphere(int nStrands, int nVert, float interDis, float radius)
    {
        Hair hair = new Hair(nStrands);
        Vector3 startPos = Vector3.zero;
        Vertex.L0 = interDis;
        for (int s = 0; s < nStrands; s++)
        {
            Strand strand = new Strand(nVert);
            hair.Add(strand);

            Vertex particle = new Vertex(startPos, 1.0f);
            float uu = Random.Range(0, 1000) / 1000.0f;
            float vv = Random.Range(0, 1000) / 1000.0f;
            float theta = 2 * Mathf.PI * uu;
            float phi = Mathf.Acos(2 * vv - 1);
            Vector3 pos = startPos + radius *
                new Vector3(Mathf.Sin(theta) * Mathf.Sin(phi), Mathf.Cos(phi), Mathf.Cos(theta) * Mathf.Sin(phi));
            for (int v = 0; v < nVert; v++)
            {
                pos.y += interDis * v;
                particle.Position = pos;
                strand.Add(particle);
            }
        }
        return hair;
    }

    public static Hair GrowHairOnDefinedPos(int nVert, float interDis, List<Vector3> positions)
    {
        int nStrands = positions.Count;
        Hair hair = new Hair(nStrands);
        Vertex.L0 = interDis;
        for (int w = 0; w < nStrands; w++)
        {
            Strand strand = new Strand(nVert);
            hair.Add(strand);
            Vector3 pos = positions[w];
            Vertex particle = new Vertex(pos, 1.0f);
            for (int v = 0; v < nVert; v++)
            {
                pos.y -= Vertex.L0;
                particle.Position = pos;
                strand.Add(particle);
            }
        }
        return hair;
    }
}
