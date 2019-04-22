using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]

public class CircularSectorMeshRenderer : MonoBehaviour
{
    public float intervalDegree = 5;
    public float degree = 45;
    public float radiusMin = 5.0f;
    public float radius = 15.0f;
	public bool autoUpdate = false;

    Mesh mesh;
    //public Material material;
    MeshFilter meshFilter;

    Vector3[] vertices;
    int[] triangles;
    Vector2[] uvs;

    float uvRadius = 0.5f;
    Vector2 uvCenter = new Vector2(0.5f, 0.5f);
    float currentIntervalDegree = 0;

    int count;
    int lastCount;

    void Awake()
    {
        //mesh = new Mesh();

        //currentIntervalDegree = Mathf.Abs(intervalDegree);

        //count = (int)(Mathf.Abs(degree) / currentIntervalDegree);
        //if (degree % intervalDegree != 0)
        //{
        //    ++count;
        //}
        //if (degree < 0)
        //{
        //    currentIntervalDegree = -currentIntervalDegree;
        //}

        //mesh.vertices = new Vector3[4 * count];
        //mesh.triangles = new int[3 * 2 * count];

        //Vector3[] normals = new Vector3[4 * count];
        //Vector2[] uv = new Vector2[4 * count];

        //for (int i = 0; i < uv.Length; i++)
        //    uv[i] = new Vector2(0, 0);
        //for (int i = 0; i < normals.Length; i++)
        //    normals[i] = new Vector3(0, 1, 0);

        //mesh.uv = uv;
        //mesh.normals = normals;

        mesh = new Mesh();
        meshFilter = (MeshFilter)GetComponent("MeshFilter");
		//UpdateMesh ();

    }

    void OnEnable()
    {
        UpdateMesh();
    }

	void LateUpdate()
	{
		if(autoUpdate)
			UpdateMesh ();
	}

    public void UpdateMesh()
    {
        currentIntervalDegree = Mathf.Abs(intervalDegree);

        count = (int)(Mathf.Abs(degree) / currentIntervalDegree);
        if (degree % intervalDegree != 0)
        {
            ++count;
        }
        if (degree < 0)
        {
            currentIntervalDegree = -currentIntervalDegree;
        }

        if (lastCount != count)
        {
            mesh.Clear();
            vertices = new Vector3[4 * count];
            triangles = new int[3 * 2 * count];
            uvs = new Vector2[4 * count];
            //vertices[0] = Vector3.zero;
            //uvs[0] = uvCenter;
            lastCount = count;
        }

        //float angle_lookat = GetEnemyAngle();
        float angle_lookat = 90; //由于方向是由父节点决定的，因此这里的方向应该是个定值，90度相当于Unity坐标系的0度

        float angle_start = angle_lookat + degree / 2;
        float angle_end = angle_lookat - degree / 2;
        float angle_delta = (angle_end - angle_start) / count;

        float angle_curr = angle_start;
        float angle_next = angle_start + angle_delta;

        //uv的起始角度根据操作方向来决定，支持360度旋转，开autoUpdate支持动态刷新贴图区域
        float dirction = GetEnemyAngle();
        float uv_curr = dirction + degree / 2;
        float uv_next = uv_curr + angle_delta;

        Vector3 pos_curr_min = Vector3.zero;
        Vector3 pos_curr_max = Vector3.zero;

        Vector3 pos_next_min = Vector3.zero;
        Vector3 pos_next_max = Vector3.zero;

        for (int i = 0; i < count; i++)
        {
            Vector3 sphere_curr = new Vector3(
            Mathf.Cos(Mathf.Deg2Rad * (angle_curr)), 0,//beginCos
            Mathf.Sin(Mathf.Deg2Rad * (angle_curr)));//beginSin

            Vector3 sphere_next = new Vector3(
            Mathf.Cos(Mathf.Deg2Rad * (angle_next)), 0,//endCos
            Mathf.Sin(Mathf.Deg2Rad * (angle_next)));//endSin

            pos_curr_min = sphere_curr * radiusMin;
            pos_curr_max = sphere_curr * radius;

            pos_next_min = sphere_next * radiusMin;
            pos_next_max = sphere_next * radius;

            int a = 4 * i;
            int b = 4 * i + 1;
            int c = 4 * i + 2;
            int d = 4 * i + 3;

            vertices[a] = pos_curr_min;
            vertices[b] = pos_curr_max;
            vertices[c] = pos_next_max;
            vertices[d] = pos_next_min;

            triangles[6 * i] = a;
            triangles[6 * i + 1] = b;
            triangles[6 * i + 2] = c;
            triangles[6 * i + 3] = c;
            triangles[6 * i + 4] = d;
            triangles[6 * i + 5] = a;

            if (i == 0)
            {
                uvs[a].x = 0.016f;
                uvs[a].y = 0.48f;
                uvs[b].x = 0.005f;
                uvs[b].y = 0.48f;
                uvs[c].x = 0.005f;
                uvs[c].y = 0.5f;
                uvs[d].x = 0.016f;
                uvs[d].y = 0.5f;
            }
            else if (i == count - 1)
            {
                uvs[a].x = 0.016f;
                uvs[a].y = 0.5f;
                uvs[b].x = 0.005f;
                uvs[b].y = 0.5f;
                uvs[c].x = 0.005f;
                uvs[c].y = 0.52f;
                uvs[d].x = 0.016f;
                uvs[d].y = 0.52f;
            }
            else
            {
                uvs[b].x = Mathf.Cos(Mathf.Deg2Rad * (uv_curr)) * uvRadius + uvCenter.x;
                uvs[b].y = Mathf.Sin(Mathf.Deg2Rad * (uv_curr)) * uvRadius + uvCenter.y;
                uvs[a].x = uvCenter.x;
                uvs[a].y = uvCenter.y;
                uvs[d].x = uvCenter.x;
                uvs[d].y = uvCenter.y;
                uvs[c].x = Mathf.Cos(Mathf.Deg2Rad * (uv_next)) * uvRadius + uvCenter.x;
                uvs[c].y = Mathf.Sin(Mathf.Deg2Rad * (uv_next)) * uvRadius + uvCenter.x;
            }

            angle_curr += angle_delta;
            angle_next += angle_delta;
            uv_curr += angle_delta;
            uv_next += angle_delta;

        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshFilter.sharedMesh = mesh;
        meshFilter.sharedMesh.name = "CircularSectorMesh";

    }

    float GetEnemyAngle()
    {
        return Mathf.Rad2Deg * Mathf.Atan2(transform.forward.z, transform.forward.x);
    }

}