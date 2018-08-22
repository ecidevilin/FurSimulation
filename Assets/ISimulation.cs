using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hair = System.Collections.Generic.List<System.Collections.Generic.List<Vertex>>;

public abstract class ISimulation
{
    protected Hair hair;

    protected ISimulation(Hair h)
    {
        hair = h;
    }

    public void AddForce(Vector3 force)
    {
        foreach (var s in hair)
        {
            int nVert = s.Count;
            for (int v = 0; v < nVert; v++)
            {
                Vertex vert = s[v];
                vert.Force += force;
                s[v] = vert;
            }
        }
    }

    public abstract void Update();
}
