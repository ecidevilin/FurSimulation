using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VR;
using UnityEngine;
using Strand = System.Collections.Generic.List<Vertex>;
using Hair = System.Collections.Generic.List<System.Collections.Generic.List<Vertex>>;

public sealed class MassSpring : ISimulation
{
    const float AirFriction = 0.003f;
    private float springConstant;
    private float maxLength;
    private float inertia;
    public MassSpring(Hair h, float spConst, float len)
        :base(h)
    {
        springConstant = spConst;
        maxLength = len;
        inertia = h[0][0].Mass * maxLength*maxLength/12.0f;//NOTE: Why 12?
        for (int s = 0; s < nStrand; s++)
        {
            Strand strand = h[s];
            for (int v = 0; v < nVert; v++)
            {
                Vertex vert = strand[v];
                vert.OldPosition = vert.Position;
                strand[v] = vert;
            }
        }
    }

    public override void Update(float dt)
    {
        for (int s = 0; s < nStrand; s++)
        {
            Strand strand = hair[s];
            for (int v = 0; v < nVert; v++)
            {
                Vertex vert = strand[v];
                Vector3 springForce = Vector3.zero;
                Vector3 springVector = Vector3.zero;
                Vector3 force = Vector3.zero;
                if (0 == v)
                {
                    springVector = new Vector3(0, -Vertex.L0, 0);
                    springForce += -vert.Velocity * inertia;
                    float len = springVector.magnitude;
                    springForce += springVector/len*(len - maxLength)*springConstant;
                    vert.Force += springForce;
                }
                else
                {
                    Vertex pre = strand[v - 1];
                    springVector = pre.OldPosition - vert.Position;
                    springForce += (pre.Velocity - vert.Velocity)*inertia;
                    float len = springVector.magnitude;
                    springForce += springVector/len*(len - maxLength)*springConstant;
                    vert.Force += springForce;
                    pre.Force -= springForce;
                    strand[v - 1] = pre;
                }
                // TODO: gravity;
                Vector3 airForce = -vert.Velocity*AirFriction;
                force += airForce;
                vert.Force += force;
                // TODO: wind;
                strand[v] = vert;
            }
        }
        for (int s = 0; s < nStrand; s++)
        {
            Strand strand = hair[s];
            for (int v = 1; v < nVert; v++)
            {
                Vertex vert = strand[v];
                vert.OldPosition = vert.Position;
                vert.Velocity += vert.Force/vert.Mass*dt;
                vert.Position += vert.Velocity*dt;
                strand[v] = vert;
            }
        }
        for (int s = 0; s < nStrand; s++)
        {
            Strand strand = hair[s];
            for (int v = 1; v < nVert; v++)
            {
                Vertex vert = strand[v];
                vert.Force = Vector3.zero;
                strand[v] = vert;
            }
        }
    }
}
