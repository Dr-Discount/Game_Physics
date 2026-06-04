using System;
using UnityEngine;

[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshCollider)), RequireComponent(typeof(MeshRenderer))]
public class WaterWorld : MonoBehaviour
{
	[System.Serializable]
	struct Wave
	{
		[Range(0, 360)] public float direction;
		[Range(0,  10)] public float amplitude;
		[Range(0,  10)] public float rate;
		[Range(0.1f,  30)] public float length;
		[Range(0,  10)] public float roll;
	}

	[System.Serializable]
	enum WaveType
	{
		Sin,
		Gerstner
	}

	[SerializeField] Wave[] waves;
	[SerializeField] Gradient gradient;
	[SerializeField] WaveType waveType;

	[Header("Mesh Generator")]
	[SerializeField][Range(1, 80)] float sizeX = 40;
	[SerializeField][Range(1, 80)] float sizeZ = 40;
	[SerializeField][Range(2, 80)] int numX = 40;
	[SerializeField][Range(2, 80)] int numZ = 40;

	MeshFilter meshFilter;
	MeshCollider meshCollider;

	Mesh mesh;
	Vector3[] vertices;
	Color[] colors;

	void Start()
	{
		meshFilter = GetComponent<MeshFilter>();
		meshCollider = GetComponent<MeshCollider>();

		MeshGenerator.Plane(meshFilter, sizeX, sizeZ, numX, numZ);

		mesh = meshFilter.mesh;
		vertices = mesh.vertices;
		colors = mesh.colors;
	}

	void FixedUpdate()
	{
		UpdateWave();
		UpdateMesh();
	}

	void UpdateWave()
	{
		// update vertex values
		for (int i = 0; i < vertices.Length; i++)
		{
			float x = i % numX;
			float z = i / numX;

            Vector3 position = Vector3.zero;
			
			position.x = ((x / numX) - 0.5f) * sizeX;
			position.z = ((z / numZ) - 0.5f) * sizeZ;
			
			Vector3 offset = Vector3.zero;
			foreach (var wave in waves)
			{
				Vector2 direction = new Vector2(Mathf.Cos(wave.direction * Mathf.Deg2Rad), Mathf.Sin(wave.direction * Mathf.Deg2Rad));

				switch(waveType)
				{
					case WaveType.Sin:
						offset += SinWave(position, direction, Time.time * wave.rate, wave.length, wave.amplitude, wave.roll);
						break;
					case WaveType.Gerstner:
						offset += GerstnerWave(position, direction, Time.time * wave.rate, wave.length, wave.amplitude, wave.roll);
						break;

				}
			}

			vertices[i] = position + offset;
			colors[i] = gradient.Evaluate(position.y + offset.y);
		}
	}

	Vector3 SinWave(Vector3 position, Vector2 direction, float phase, float length, float amplitude, float roll)
	{
		Vector3 v = Vector3.zero;

		float coord = position.x * direction.x + position.z * direction.y;
		float k = (Mathf.PI * 2) / length;
		float f = (coord * k) + phase;

		// vertical displacement: the actual up-and-down motion. Standard sine wave.
		v.y = Mathf.Sin(f) * amplitude;

		return v;
	}

	Vector3 GerstnerWave(Vector3 position, Vector2 direction, float phase, float length, float amplitude, float roll)
	{
        Vector3 v = Vector3.zero;

        float coord = position.x * direction.x + position.z * direction.y;
        float k = (Mathf.PI * 2) / length;
        float f = (coord * k) + phase;

		v.x = direction.x * Mathf.Cos(f) * roll;
		v.z = direction.y * Mathf.Cos(f) * roll;

        v.y = Mathf.Sin(f) * amplitude;

        return v;
    }

	void UpdateMesh()
	{
		// recalculate mesh with new vertice values
		mesh.SetVertices(vertices);
		mesh.SetColors(colors);

		mesh.RecalculateNormals();
		mesh.RecalculateTangents();
		mesh.RecalculateBounds();

        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = mesh;
    }

}

