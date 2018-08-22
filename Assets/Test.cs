using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hair = System.Collections.Generic.List<System.Collections.Generic.List<Vertex>>;

public class Test : MonoBehaviour
{
    private ISimulation sim;
    private Hair hair;
    // Use this for initialization
    void Start ()
	{
        hair = HairFactory.GrowHairOnSphere(500, 50, 1f, 1);
	    Visualizer.Init(gameObject.GetComponent<MeshFilter>());
	    sim = new FTL(hair);
	    sim.AddForce(new Vector3(5, 0, 0));
	}

    private static int frame;
	// Update is called once per frame
	void Update () {
	    for (int i = 0; i < 1; i++)
        {
            Visualizer.Update(hair);
            Vector3 gravity = new Vector3(0, -0.3f, 0);
            sim.AddForce(gravity);
            if (++frame % 50 == 0)
            {
                Vector3 force = new Vector3(10 * Mathf.Sin(frame), 0, 10 * Mathf.Cos(frame));
                sim.AddForce(force);
            }
            sim.Update();
        }
	}
}
