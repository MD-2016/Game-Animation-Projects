using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk5 : MonoBehaviour
{
    public float width = 10F;
    public float height = 5F;
    public float depth = .25F;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void start(Vector3 point)
    {
        Mesh mesh = new Mesh();
        this.GetComponent<MeshFilter>().mesh = mesh;

        mesh.vertices = new Vector3[] {
            new Vector3(width, 0, 0),
            new Vector3(width, height, 0),
            new Vector3(point.x, point.y, 0),
            new Vector3(width, 0, depth),
            new Vector3(width, height, depth),
            new Vector3(point.x, point.y, depth),
        };

        mesh.triangles = new int[] {
            0, 2, 1,
            5, 3, 4,
            0, 3, 5,
            5, 2, 0,
            0, 1, 4,
            4, 3, 0,
            1, 2, 5,
            5, 4, 1,
        };

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
        meshCollider.convex = true;


        transform.position = new Vector3(0f, 0f, 0f);
    }
}
