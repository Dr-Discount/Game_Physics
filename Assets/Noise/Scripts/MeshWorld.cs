using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshCollider)), RequireComponent(typeof(MeshRenderer))]
public class MeshWorld : MonoBehaviour
{
    [Header("Perlin")]
    [SerializeField, Range(-10.0f, 10.0f)] float height = 0;
    [SerializeField, Range(-0.2f, 0.2f)] float perlinScale = 0.05f;
    [SerializeField, Range(-5.0f, 5.0f)] float perlinRateX = 0.0f;
    [SerializeField, Range(-5.0f, 5.0f)] float perlinRateZ = 0.0f;
    [SerializeField] Gradient gradient;
    float perlinOffsetX = 0.0f;
    float perlinOffsetZ = 0.0f;

    [Header("Mesh Generator")]
	[SerializeField][Range(1, 80)] float sizeX = 40;
	[SerializeField][Range(1, 80)] float sizeZ = 40;
	[SerializeField][Range(2, 80)] int numX = 40;
	[SerializeField][Range(2, 80)] int numZ = 40;

	MeshFilter meshFilter;
	MeshCollider meshCollider;

	Mesh mesh;
	Vector3[] vertices;
	Vector3[,] buffer;
	Color[] colors;

	void Start()
	{
		meshFilter = GetComponent<MeshFilter>();
		meshCollider = GetComponent<MeshCollider>();

		MeshGenerator.Plane(meshFilter, sizeX, sizeZ, numX, numZ);

		mesh = meshFilter.mesh;
		vertices = mesh.vertices;
		colors = mesh.colors;

		buffer = new Vector3[numX, numZ];
	}
	
	void Update()
	{
        perlinOffsetX += perlinRateX * Time.deltaTime;
        perlinOffsetZ += perlinRateZ * Time.deltaTime;

        UpdateWorld();
		UpdateMesh();
	}

    void UpdateWorld()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            float x = i % numX;
            float z = i / numX;

            float noise = SampleNoise(x, z);
            float y = noise * height;

            Vector3 p = Vector3.zero;
            p.x = ((x / (float)(numX - 1)) - 0.5f) * sizeX;
            p.z = ((z / (float)(numZ - 1)) - 0.5f) * sizeZ;
            p.y = y;

            vertices[i] = p;
            colors[i] = gradient.Evaluate(noise);
        }
    }

    void UpdateMesh()
    {
        mesh.vertices = vertices;
        mesh.colors = colors;

        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        mesh.RecalculateBounds();
        meshCollider.sharedMesh = mesh;
    }

    float SampleNoise(float x, float z)
    {
        float sx = (x + perlinOffsetX) * perlinScale;
        float sz = (z + perlinOffsetZ) * perlinScale;
        float value = Mathf.PerlinNoise(sx, sz);

        value = Mathf.Abs(value * 2.0f - 1.0f);

        return value;
    }
}
