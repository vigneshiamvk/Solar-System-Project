using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class OrbitDebuggingTool : MonoBehaviour
{
    public int OrbitTimeStep;

    // Update is called once per frame
    void Update()
    {
        if(!Application.isPlaying)
        {
            for (int i = 0; i < OrbitTimeStep; i++)
            {
                Physics.autoSimulation = false;
                Physics.Simulate(Time.fixedDeltaTime);
                Debug.Log("Test");
                CelestialBody[] bodies = FindObjectsOfType<CelestialBody>();
                foreach (CelestialBody body in bodies)
                {
                    body.Start();
                    body.FixedUpdate();
                    body.Update();
                }
                Physics.autoSimulation = true;
            }
        }
    }
}
