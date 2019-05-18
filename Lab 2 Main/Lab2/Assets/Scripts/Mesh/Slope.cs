using UnityEngine;
using System.Collections;

public class Slope : MonoBehaviour
{
    //filter used to build mesh and the slope mesh

    public MeshFilter meshFilter;

    public Mesh mesh;


    //call  the mesh building method
    private void Start()
    {
        buildMesh();
    }

    //build the mesh, create the filter component, generate the mesh, and recalculate normal vectors.
    private void buildMesh()
    {
        mesh = new Mesh();
        mesh.name = "Slope Mesh";

        meshFilter = gameObject.AddComponent<MeshFilter>();

        meshFilter.mesh = mesh;

        mesh.vertices = VerticesThatMakeSlope();
        mesh.triangles = TrianglesThatMakeSlope();

        mesh.RecalculateNormals();


    }

    //vertices that makes up the slope
    private Vector3[] VerticesThatMakeSlope(){

        Vector3[] vertexArray = new Vector3[18];

        //bottom square
        vertexArray[0] = new Vector3(-0.39f, -0.39f, -0.39f);
        vertexArray[1] = new Vector3(0.39f, -0.39f, -0.39f);
        vertexArray[2] = new Vector3(0.39f, -0.39f, 0.39f);
        vertexArray[3] = new Vector3(-0.39f, -0.39f, 0.39f);

        //one side
        vertexArray[4] = new Vector3(-0.39f, -0.39f, -0.39f);
        vertexArray[5] = new Vector3(0.39f, -0.39f, -0.39f);
        vertexArray[6] = new Vector3(0.39f, 0.39f, -0.39f);

        //other side
        vertexArray[7] = new Vector3(0.39f, -0.39f, 0.39f);
        vertexArray[8] = new Vector3(-0.39f, -0.39f, 0.39f);
        vertexArray[9] = new Vector3(0.39f, 0.39f, 0.39f);

        //another square
        vertexArray[10] = new Vector3(-0.39f, -0.39f, -0.39f);
        vertexArray[11] = new Vector3(0.39f, 0.39f, -0.39f);
        vertexArray[12] = new Vector3(0.39f, 0.39f, 0.39f);
        vertexArray[13] = new Vector3(-0.39f, -0.39f, 0.39f);

        //another square
        vertexArray[14] = new Vector3(0.39f, -0.39f, -0.39f);
        vertexArray[15] = new Vector3(0.39f, -0.39f, 0.39f);
        vertexArray[16] = new Vector3(0.39f, 0.39f, 0.39f);
        vertexArray[17] = new Vector3(0.39f, 0.39f, -0.39f);
       
        

        return vertexArray;
}
    //triangles that make up the slope
    private int[] TrianglesThatMakeSlope()
    {
        int[] triangleArray = new int[24];

        
        //one bottom triangle that makes bottom square
        triangleArray[0] = 0;
        triangleArray[1] = 1;
        triangleArray[2] = 2;

        //second bottom triangle that makes bottom square
        triangleArray[3] = 0;
        triangleArray[4] = 2;
        triangleArray[5] = 3;

        //side triangle
        triangleArray[6] = 4;
        triangleArray[7] = 6;
        triangleArray[8] = 5;

        //side triangle
        triangleArray[9] = 8;
        triangleArray[10] = 7;
        triangleArray[11] = 9;

        //part of front square
        triangleArray[12] = 10;
        triangleArray[13] = 12;
        triangleArray[14] = 11;

        //other part of front square
        triangleArray[15] = 10;
        triangleArray[16] = 13;
        triangleArray[17] = 12;

        //part of back square
        triangleArray[18] = 14;
        triangleArray[19] = 17;
        triangleArray[20] = 15;

        //other part of back square
        triangleArray[21] = 15;
        triangleArray[22] = 17;
        triangleArray[23] = 16;

        return triangleArray;
    }


}