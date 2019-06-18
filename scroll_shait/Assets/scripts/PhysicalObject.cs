using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SharpPoint
{
    Vector3 position;
    Vector4 direction;
    float sharpness;
}

public struct SharpEdge
{
    Vector3 startPos;
    Vector3 stopPos;
    Vector3 direction;
    float sharpness;
}

public class PhysicalObject{

    public GameObject physicalObject_m;
    public MeshFilter meshFilter_m;
    public MeshRenderer meshRenderer_m;
    public Rigidbody rigidBody_m;
    public MeshCollider meshCollider_m;
    public Mesh convexMesh_m;
    public Material material_m;
    public Texture texture_m;
    public Vector3 position_m;
    public Quaternion rotation_m;
    public Vector3 size_m;
    public float distortion_m;
    Vector3[] vertices_m;
    int[] triangles_m;
    public bool sharpGeometry_m;
    Vector2[] uvMap;
    public SharpEdge[] sharpEdges;
    public SharpPoint[] sharpPoints;

}
