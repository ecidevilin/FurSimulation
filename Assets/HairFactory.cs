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
        Vector3 startPos = new Vector3(0, 0.5f, 0);
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

    public static Hair GrowHairOnPlane(int nStrands, int nVert)
    {

        Hair hair = new Hair(nStrands);
        Vector3 startPos = new Vector3(0, 0.5f, 0);
        int layers = 2;

        float len = 0.5f;
        Vertex.L0 = len / nVert;

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
                startPos.x += 0.015f;
            }
        }
        return hair;
    }
}
