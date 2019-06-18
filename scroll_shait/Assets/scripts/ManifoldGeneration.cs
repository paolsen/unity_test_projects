using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ManifoldGeneration : MonoBehaviour {

    public GameObject stone;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public Rigidbody rigidBody;
    public MeshCollider meshCollider;
    public Mesh mesh;
    public Material mat;
    public Material temp;
    public Texture tex;
    public Vector3 position;
    public Quaternion rotation;
    public float size;
    public int layers;
    public int complexity;
    public float variance;
    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvMap;

    public static Vector3 cross(Vector3 left, Vector3 right)
    {
        return new Vector3(left.y * right.z - right.y * left.z, -(left.x * right.z - left.z * right.x), left.y * right.z - left.z * right.y);
    }

    public Vector3 variate(Vector3 vec, float variance)
    {
        return new Vector3(vec.x + Random.Range(-variance, variance), vec.y + Random.Range(-variance, variance), vec.z + Random.Range(-variance, variance));
    }

    void createMesh(float size, float variance, int layers, int complexity)
    {
        Vector3 start = rigidBody.transform.position;
        vertices[0] = start;
        float x = start.x;
        float y = start.y;
        float z = start.z;
        float dy = size /((float) layers+2);
        for (uint i = 0; i < layers; i++) {
            for (uint j = 0; j < complexity; j++)
            {
                Vector3 tempVector = variate(new Vector3(x + size/2f*Mathf.Cos(j*2*Mathf.PI/((float)complexity)),y + dy*(1+i),z + size / 2f * Mathf.Sin(j * 2 * Mathf.PI / ((float)complexity))), variance);
                vertices[1 + i * complexity + j] = tempVector;
            }
        }
        Vector3 stop = variate(new Vector3(x, y + size, z), variance);
        vertices[1+ complexity*layers] = stop;
        //print("stop is good\n");
        mesh.vertices = vertices;
        int counter = 0;
        for(int i = 0; i < complexity; i++)
        {
            counter += 3;
            triangles[i * 3] = 0; // start
            triangles[i * 3 + 1] = (i + 1);
            triangles[i * 3 + 2] = (i + 1) % complexity + 1;

        }
        //print("small for is good\n");
        for (int i = 0; i < layers-1; i++)
        {
            for(int j = 0; j < complexity; j++)
            {
                //lower
                triangles[counter] = i * complexity + j + 1;
                //print(i * complexity + j + 1);//print
                triangles[counter + 1] = (i + 1) * complexity + (j) + 1;
                //print((i + 1) * complexity + (j) + 1);//print
                triangles[counter + 2] = i * complexity + (j) % complexity + 2;
                //print(i * complexity + (j) % complexity + 1);//print
                counter += 3;
                //upper
                triangles[counter] = (i + 1) * complexity + j;
                //print((i + 1) * complexity + j);//print
                triangles[counter + 1] = (i + 1) * complexity + j % complexity + 1;
                //print((i + 1) * complexity + j % complexity + 1);//print
                triangles[counter + 2] = i * complexity + j % complexity + 1;
                //print(i * complexity + j % complexity + 1);//print
                counter += 3;

            }
        }
        print("large for is good\n");
        print(complexity * layers + 1);
        for (int i = 0; i < complexity; i++)
        {
            //counter += 3;
            triangles[counter] = complexity*layers + 1; // stop
            triangles[counter + 1] = complexity * (layers - 1) + (i+1)%complexity +1;
            triangles[counter + 2] = complexity * (layers - 1) + (i+1);
            counter += 3;

        }
        //counter -= 3;
        //triangles[counter] = complexity * layers + 1; // start
        //triangles[counter + 2] = complexity * (layers - 1) + complexity;
        //triangles[counter + 1] = complexity * (layers - 1) + complexity % complexity + 1;
        print("small for is good\n");
        mesh.triangles = triangles;
    }

    void uvMapping()
    {
        Vector2[] coordinates = new Vector2[]{
            new Vector2(0, 0),
            new Vector2(0,1),
            new Vector2(1,1),
            new Vector2(1,0)
        };
        uvMap[0] = coordinates[0];
        int mode = 0;
        mode = 2 * (layers % 2) + complexity % 2;
        switch (mode)
        {
            case 0:
                // layers and complexity are even
                for (int l = 0; l < layers; l++)
                {
                    for(int c = 0; c < complexity; c++)
                    {
                        if (l%2 == 0)//even layer
                        {
                            if (c == complexity - 1)
                                uvMap[1 + l * complexity + c] = coordinates[3];
                            else
                                uvMap[1 + l * complexity + c] = coordinates[1 + c % 2];
                        }
                        else //odd layer
                        {
                            if (c == complexity - 1)
                                uvMap[1 + l * complexity + c] = coordinates[2];
                            else
                                uvMap[1 + l * complexity + c] = coordinates[3*(c % 2)];
                        }
                    }
                    uvMap[layers * complexity + 1] = coordinates[1];
                }
                break;
            case 1:
                // layers are even, complexity is odd
                for (int l = 0; l < layers; l++)
                {
                    for (int c = 0; c < complexity; c++)
                    {
                        if (l % 2 == 0)//even layer
                        {
                            if (c == (complexity - 1))
                                uvMap[1 + l * complexity + c] = coordinates[3];
                            else
                                uvMap[1 + l * complexity + c] = coordinates[1 + c % 2];
                        }
                        else //odd layer
                        {
                            if (c == (complexity -2))
                                uvMap[1 + l * complexity + c] = coordinates[1];
                            else
                                uvMap[1 + l * complexity + c] = coordinates[3 * (c % 2)];
                        }
                    }
                    uvMap[layers * complexity + 1] = coordinates[2];
                }
                break;
            case 2:
                // layers are odd, complexity is even
                for (int l = 0; l < layers; l++)
                {
                    for (int c = 0; c < complexity; c++)
                    {
                        if (l % 2 == 0)//even layer
                        {
                            if (c == complexity - 1)
                                uvMap[1 + l * complexity + c] = coordinates[3];
                            else
                                uvMap[1 + l * complexity + c] = coordinates[1 + c % 2];
                        }
                        else //odd layer
                        {
                            if (c == complexity - 1)
                                uvMap[1 + l * complexity + c] = coordinates[2];
                            else
                                uvMap[1 + l * complexity + c] = coordinates[3 * (c % 2)];
                        }
                    }
                    uvMap[layers * complexity + 1] = coordinates[0];
                }
                break;
            case 3:
                // layers and complexity are odd
                uvMap[layers * complexity + 1] = coordinates[3];
                for (int l = 1; l < layers; l++)
                {
                    for (int c = 0; c < complexity; c++)
                    {
                        if (l % 2 == 1)
                        {
                            if (c == complexity - 1)
                                uvMap[l * complexity + c] = coordinates[3];
                            else
                                uvMap[l * complexity + c] = coordinates[1 + c % 2];
                        }
                        else
                        {
                            if (c == complexity - 1)
                                uvMap[l * complexity + c] = coordinates[2];
                            else
                                uvMap[l * complexity + c] = coordinates[c % 2];
                        }
                    }
                }
                break;

        }
        //print("UV-mapping is good\n");
    }

    // Use this for initialization
    void Start () {
        //stone = new GameObject("tores_balls_001");
        meshFilter = stone.AddComponent<MeshFilter>();
        //print("meshfilter added\n");
        meshRenderer = stone.AddComponent<MeshRenderer>();
        //print("meshRenderer added\n");
        mesh = meshFilter.mesh;
        //print("mesh added\n");
        rigidBody = stone.AddComponent<Rigidbody>();
        //print("rigidbody added\n");
        rigidBody.transform.SetPositionAndRotation(position, rotation);
        //print("rigidBody added\n");
        
        vertices = new Vector3[layers * complexity + 2];
        //print("vertices created\n");
        triangles = new int[3*(2*complexity + 2 * (layers-1) * complexity)];
        //print("triangles created\n");
        //uvMap = new Vector2[(2 * complexity + 2 * (layers - 1) * complexity)*4];
        uvMap = new Vector2[layers * complexity + 2];
        
        //print("uvMap created\n");

        mesh.Clear();
        //print("mesh cleared\n");
        createMesh(size, variance, layers, complexity);
        //print("mesh created\n");
        uvMapping();
        mesh.uv = uvMap;
        print("uvMap created\n");
        mesh.RecalculateNormals();
        //print("recalculated normals\n");
        meshCollider = stone.AddComponent<MeshCollider>();
        meshRenderer.material = mat;
        //print("meshcollider added\n");
        rigidBody.mass = 500000;
        //print("mass added\n");
        meshCollider.convex = true;
        //print("meshcollider convex true\n");
        rigidBody.useGravity = true;
        //print("rigidbody gravity false\n");

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
