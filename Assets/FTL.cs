using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Strand = System.Collections.Generic.List<Vertex>;
using Hair = System.Collections.Generic.List<System.Collections.Generic.List<Vertex>>;

public class FTL : ISimulation
{

    public FTL(Hair h)
        : base(h)
    {

    }

    public override void Update(float dt)
    {
        float Damping = 0.9f;
        float L0 = Vertex.L0;

        for (int s = 0; s < hair.Count; s++)
        {
            Strand strand = hair[s];
            int nVert = strand.Count;
            for (int v = nVert - 1; v >= 1; v--)
            {
                Vertex vert = strand[v];
                vert.OldPosition = vert.Position;
                vert.Position = vert.OldPosition + dt * vert.Velocity + dt * dt * vert.Force;

                Vertex pre = strand[v - 1];
                Vector3 dir = Vector3.Normalize(vert.Position - pre.Position);
                Vector3 pBackup = vert.Position;
                vert.Position = pre.Position + dir*L0;
                vert.Velocity = (vert.Position - vert.OldPosition)/ dt;
                vert.Velocity += (v + 1 >= nVert) ? Vector3.zero : Damping*(-strand[v + 1].Correction/ dt);
                vert.Correction = vert.Position - pBackup;
                vert.Force = Vector3.zero;
                strand[v] = vert;
            }
        }
    }
}
