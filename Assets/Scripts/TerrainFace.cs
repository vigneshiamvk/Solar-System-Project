﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace 
{

    Mesh mesh;
    ShapeGenerator shapeGenerator;

    int resolution;

    Vector3 localUp;

    Vector3 axisA;

    Vector3 axisB;

    public TerrainFace(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp)
    {
        this.shapeGenerator = shapeGenerator;
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;

        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);
    }

    public void ConstructMesh()
    {
        Vector3[] vertices = new Vector3[resolution * resolution];

        // 2 is the number of triangles in each face, and 3 is the number of vertices for each triangle
        int[] triangles = new int[(resolution - 1) * (resolution - 1) * 2 * 3];

        int triangleIndex = 0;

        for (int y=0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int i = x + y * resolution;
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                vertices[i] = shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere);

                if(x != resolution-1 && y !=resolution-1)
                {
                    triangles[triangleIndex] = i;
                    triangles[triangleIndex + 1] = i + resolution + 1;
                    triangles[triangleIndex + 2] = i + resolution;

                    triangles[triangleIndex + 3] = i;
                    triangles[triangleIndex + 4] = i + 1;
                    triangles[triangleIndex + 5] = i + resolution + 1;

                    triangleIndex += 6;
                }
            }
        }

        mesh.Clear();

        mesh.vertices = vertices;

        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
