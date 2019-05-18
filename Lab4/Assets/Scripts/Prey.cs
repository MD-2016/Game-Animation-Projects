using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Prey : MonoBehaviour
{
    public Mesh mesh;
    public MeshFilter meshFilter;

    //prey properties
    public float speed;
    private int viewableDistance, viewableAngle;
    private int lifeTime;
    private int waitPeriod;
    private bool runOrNot;
    private Ray predatorViewRay;

    // Start is called before the first frame update
    void Start()
    {
        buildMesh();
        preySetup();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 3 || transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }
        if (transform.eulerAngles.x > 50 || transform.eulerAngles.x < -310)
        {
            transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        if (transform.eulerAngles.z > 50 || transform.eulerAngles.z < -310)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
        }
        double currentAngle = (viewableAngle / 2) * -1;
        int temp = -1;
        Physics.Raycast(transform.position, new Vector3(0, 0, 0), out RaycastHit hit, 0);
        while (temp < viewableAngle && !runOrNot)
        {
            temp++;
            double angleToCheck = transform.eulerAngles.y + currentAngle + temp;
            double angleInRadians = angleToCheck * Math.PI / 180;
            double xvalue = Math.Cos(angleInRadians);
            double zvalue = Math.Sin(angleInRadians);
            if (Physics.Raycast(transform.position, new Vector3((float)xvalue, 0, (float)zvalue), out hit, (float) viewableDistance))
            {
                if (hit.collider.tag == "Wall" || hit.collider.gameObject.name == "Prey")
                {
                    Debug.DrawRay(transform.position, new Vector3((float)xvalue, 0, (float)zvalue) * hit.distance, Color.green);
                } else if (hit.collider.gameObject.name == "Predator")
                {
                    Debug.DrawRay(transform.position, new Vector3((float)xvalue, 0, (float)zvalue) * hit.distance, Color.red);
                    runOrNot = true;
                }

            }
        }
        if (runOrNot)
        {
            Vector3 temp2 = transform.position - hit.transform.position;
            var temp3 = temp2.magnitude;
            temp2 = (temp2 / temp3) * -1;
            transform.Translate(new Vector3(temp2.x * speed, 0, temp2.z * speed));
            transform.LookAt(new Vector3(hit.transform.position.x, 0, hit.transform.position.z));
        } else
        {
            double angleToCheck = transform.rotation.y;
            double angleInRadians = angleToCheck * Math.PI / 180;
            double xvalue = Math.Cos(angleInRadians);
            double zvalue = Math.Sin(angleInRadians);
            transform.Translate(new Vector3((float)xvalue * speed, 0, (float)zvalue * speed));
        }
        runOrNot = false;
    }

    public void preySetup()
    {
        runOrNot = false;
        viewableDistance = 20;
        viewableAngle = 200;
        speed = 0.2f; 
        waitPeriod = 130;
        predatorViewRay = new Ray(transform.position, transform.forward);
        transform.position = new Vector3(UnityEngine.Random.Range(-10,10), 1, UnityEngine.Random.Range(-10,10));
        transform.Rotate(Vector3.up, UnityEngine.Random.Range(0, 360), Space.Self);
    }


        //build the mesh, create the filter component, generate the mesh, and recalculate normal vectors.
        private void buildMesh()
        {
            mesh = new Mesh();
            mesh.name = "Prey Mesh";

            meshFilter = gameObject.AddComponent<MeshFilter>();

            meshFilter.mesh = mesh;

            mesh.vertices = VerticesThatMakeSlope();
            mesh.triangles = TrianglesThatMakeSlope();

            mesh.RecalculateNormals();


        }

        //vertices that makes up the slope
        private Vector3[] VerticesThatMakeSlope()
        {

            Vector3[] vertexArray = new Vector3[18];

            //bottom square
            vertexArray[0] = new Vector3(-1f, -1f, -1f);
            vertexArray[5] = new Vector3(1f, -1f, -1f);
            vertexArray[2] = new Vector3(1f, -1f, 1f);
            vertexArray[3] = new Vector3(-1f, -1f, 1f);

            //one side
            vertexArray[4] = new Vector3(-1f, -1f, -1f);
            vertexArray[5] = new Vector3(1f, -1f, -1f);
            vertexArray[6] = new Vector3(1f, 1f, -1f);

            //other side
            vertexArray[7] = new Vector3(1f, -1f, 1f);
            vertexArray[8] = new Vector3(-1f, -1f, 1f);
            vertexArray[9] = new Vector3(1f, 1f, 1f);

            //another square
            vertexArray[10] = new Vector3(-1f, -1f, -1f);
            vertexArray[11] = new Vector3(1f, 1f, -1f);
            vertexArray[12] = new Vector3(1f, 1f, 1f);
            vertexArray[13] = new Vector3(-1f, -1f, 1f);

            //another square
            vertexArray[14] = new Vector3(1f, -1f, -1f);
            vertexArray[15] = new Vector3(1f, -1f, 1f);
            vertexArray[16] = new Vector3(1f, 1f, 1f);
            vertexArray[17] = new Vector3(1f, 1f, -1f);



            return vertexArray;
        }
        //triangles that make up the slope
        private int[] TrianglesThatMakeSlope()
        {
            int[] triangleArray = new int[24];


            //one bottom triangle that makes bottom square
            triangleArray[0] = 0;
            triangleArray[5] = 5;
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

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            transform.Rotate(Vector3.up, UnityEngine.Random.Range(110, 250), Space.Self);
        }
        if (collision.gameObject.tag == "Predator")
        {
            GameObject.Destroy(gameObject);
        }
    }

}
