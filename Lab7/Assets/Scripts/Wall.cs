using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public float width = 10F;
    public float height = 5F;
    public float depth = .25F;

    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = new Mesh();
        this.GetComponent<MeshFilter>().mesh = mesh;

        mesh.vertices = new Vector3[] {
            new Vector3(0, 0, 0),
            new Vector3(0, height, 0),
            new Vector3(width, height, 0),
            new Vector3(width, 0, 0),
            new Vector3(0, 0, depth),
            new Vector3(0, height, depth),
            new Vector3(width, height, depth),
            new Vector3(width, 0, depth)
        };

        mesh.triangles = new int[] {
            0, 1, 2,
            2, 3, 0,
            6, 5, 4,
            6, 4, 7,
            5, 6, 1,
            6, 2, 1,
            0, 7, 4,
            0, 3, 7,
            5, 1, 4,
            1, 0, 4,
            2, 6, 7,
            2, 7, 3,
        };

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
        meshCollider.convex = true;

        transform.position = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
