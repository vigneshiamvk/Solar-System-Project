using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialBody : MonoBehaviour
{
    public Rigidbody rb;

    public GameObject isChildOf;

    public Vector3 initialVelocity;

    public Vector3 lineStart;

    public bool isSun;

    public Vector3 lineEnd;

    public void Start()
    {
        rb.AddForce(initialVelocity);

        lineStart = this.gameObject.transform.position;
        lineEnd = this.gameObject.transform.position;
    }
    public void FixedUpdate()
    {
        CelestialBody[] bodies = FindObjectsOfType<CelestialBody>();
        foreach (CelestialBody body in bodies)
        {
            if (body != this && !body.isSun)
            {
                Attract(body);
            }
        }
    }

    public void Update()
    {
        lineStart = lineEnd;
        lineEnd = this.gameObject.transform.position;
        //Debug.DrawLine(lineStart, lineEnd, Color.white, 2500f);
    }
    public void Attract(CelestialBody otherBody)
    {
        Rigidbody otherBodyRB = otherBody.rb;

        Vector3 direction = rb.position - otherBodyRB.position;

        float distance = direction.sqrMagnitude;

        float forceMagnitude = Universe.gravitationalConstant * (rb.mass * otherBodyRB.mass) / distance;

        Vector3 force = direction.normalized * forceMagnitude;
    
        otherBodyRB.AddForce(force);
    }
}
