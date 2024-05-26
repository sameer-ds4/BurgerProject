using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftBody : MonoBehaviour
{
    public float mass = 1;
    public float stiffness = 1;
    public float damping = 1;
    public float intensity = 1;

    private MeshRenderer meshRenderer;
    private Mesh originalMesh, meshClone;
    private SoftBodyMesh[] ms;
    private Vector3[] vertexArray;

    private void Start() 
    {
        originalMesh = GetComponent<MeshFilter>().sharedMesh;
        meshClone = Instantiate(originalMesh);
        GetComponent<MeshFilter>().sharedMesh = meshClone;
        meshRenderer = GetComponent<MeshRenderer>();
        ms = new SoftBodyMesh[meshClone.vertices.Length];

        for (int i = 0; i < meshClone.vertices.Length; i++)
        {
            ms[i] = new SoftBodyMesh(i, transform.TransformPoint(meshClone.vertices[i]));
        }
    }

    private void FixedUpdate()
    {
        vertexArray = originalMesh.vertices;

        for (int i = 0; i < ms.Length; i++)
        {
            Vector3 target = transform.TransformPoint(vertexArray[ms[i].iD]);
            float _intensity = (1 - (meshRenderer.bounds.max.y - target.y)/meshRenderer.bounds.size.y) * intensity;
            ms[i].Shake(target, mass, stiffness, damping);
            target = transform.InverseTransformPoint(ms[i].position);
            vertexArray[ms[i].iD] = Vector3.Lerp(vertexArray[ms[i].iD], target, intensity);
        }
        meshClone.vertices = vertexArray;
    }

}

[Serializable]
public class SoftBodyMesh
{
    public int iD;
    public Vector3 position;
    public Vector3 velocity, force;

    public SoftBodyMesh(int iD,Vector3 pos)
    {
        this.iD = iD;
        this.position = pos;
    }

    public void Shake(Vector3 target, float m, float s, float d)
    {
        force = (target - position) * s;
        velocity = (velocity + force/m) * d;
        position += velocity;

        if((velocity + force + force/ m).magnitude < 0.001f)
            position = target;
    }
}
