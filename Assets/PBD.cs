using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Strand = System.Collections.Generic.List<Vertex>;
using Hair = System.Collections.Generic.List<System.Collections.Generic.List<Vertex>>;

public class PBD : ISimulation
{
    public PBD(Hair h)
        : base(h)
    {
        
    }

    public override void Update(float dt)
    {
        float Damping = 0.95f;
        float L0 = Vertex.L0;
        for (int s = 0; s < hair.Count; s++)
        {
            Strand strand = hair[s];
            int nVert = strand.Count;
            for (int v = 1; v < nVert; v++)
            { 
                Vertex vert = strand[v];
                vert.OldPosition = vert.Position;
                strand[v] = vert;
            }
        }
        for (int s = 0; s < hair.Count; s++)
        {
            Strand strand = hair[s];
            int nVert = strand.Count;
            for (int v = 1; v < nVert; v++)
            {
                Vertex vert = strand[v];
                Vector3 acceleration = vert.Force/ vert.Mass;
                vert.Velocity += dt * acceleration;
                vert.Position += dt * vert.Velocity;
                vert.Force = Vector3.zero;
                strand[v] = vert;
            }
        }
        for (int s = 0; s < hair.Count; s++)
        {
            Strand strand = hair[s];
            int nVert = strand.Count;
            for (int v = 1; v < nVert; v++)
            {
                Vertex vert = strand[v];
                Vertex vert0 = strand[v - 1];
                Vector3 dir = Vector3.Normalize(vert.Position - vert0.Position);
                vert.Correction = vert.Position;
                vert.Position = vert0.Position + dir*L0;
                vert.Correction = vert.Position - vert.Correction;
                strand[v] = vert;
            }
        }
        for (int s = 0; s < hair.Count; s++)
        {
            Strand strand = hair[s];
            int nVert = strand.Count;
            for (int v = 1; v < nVert; v++)
            {
                Vertex vert = strand[v];
                vert.Velocity = (vert.Position - vert.OldPosition)/ dt;
                vert.Velocity += (v + 1 >= nVert ? Vector3.zero : Damping*(-strand[v + 1].Correction/ dt));
                strand[v] = vert;
            }
        }
    }
}
