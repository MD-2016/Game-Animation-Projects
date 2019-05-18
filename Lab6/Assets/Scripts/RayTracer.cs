using System.Collections;
using System.Collections.Generic;
using UnityEngine.Windows.Speech;
using UnityEngine;
using System.IO;
using System;

public class RayTracer : MonoBehaviour
{
    //color grid and the variables needed to print to file
    public Vector3[,] colorGrid;
    public Color currentColor;
    public RaycastHit cameraRayHit;
    public Camera main;
    public static int width = 355;
    public static int height = 200;
    StreamWriter outputPPM = null;
    public int maxColors = 255;
    string path = "Assets/Color_Values.ppm";
    float turn = 35f;
    public Light[] lights;


    //used to check which rendering method to use (Perspective or orthographic)
    public bool isPerspective = true;

    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
        lights = FindObjectsOfType(typeof(Light)) as Light[];
        colorGrid = new Vector3[width, height];
        //writeToPPM(outputPPM, width, height, maxColors, colorGrid);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right);
        }
        else if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down);
        }
        else if(Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.forward);
        }
        else if(Input.GetKey(KeyCode.E))
        {
            transform.Translate(Vector3.back);
        }
        else if(Input.GetKey(KeyCode.Z))
        {
            transform.Rotate(Vector3.down * turn * Time.deltaTime, Space.World);
        }
        else if(Input.GetKey(KeyCode.X))
        {
            transform.Rotate(Vector3.up * turn * Time.deltaTime, Space.World);
        }
        else if(Input.GetKey(KeyCode.C))
        {
            transform.Rotate(Vector3.forward * turn * Time.deltaTime, Space.World);
        }
        else if(Input.GetKey(KeyCode.V))
        {
            transform.Rotate(Vector3.back * turn * Time.deltaTime, Space.World);
        }
        else if(Input.GetKey(KeyCode.B))
        {
            transform.Rotate(Vector3.left * turn * Time.deltaTime, Space.World);
        }
        else if(Input.GetKey(KeyCode.N))
        {
            transform.Rotate(Vector3.right * turn * Time.deltaTime, Space.World);
        }
        else if(Input.GetKey(KeyCode.M))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if(Input.GetKey(KeyCode.Space))
        {
            //this is where the snapshot is taken

            cameraCastRay(colorGrid);
        }

        //Controls for switching camera from orthographic to perspective.
        //Note: Depending on which option is chosen, the rendering (screenshot) should reflect that choice
        //Example: If user chose orthographic, then user would get the shot rendered orthographically. 
        if(Input.GetKey(KeyCode.O))
        {
            GetComponent<Camera>().orthographic = true;
        }

        if(Input.GetKey(KeyCode.P))
        {
            GetComponent<Camera>().orthographic = false;
        }


    }

    public void writeToPPM(StreamWriter writer, int width, int height, int maxColor, Vector3[,] colorValues)
    {
        System.IO.File.WriteAllText(@"Assets/Color_Values.ppm", string.Empty);
        writer = new StreamWriter(path, false);
        writer.WriteLine("P3");
        writer.WriteLine(width + " " + height);
        writer.WriteLine(maxColor);

        for (int i = height - 1; i >= 0; i--)
        {
            for (int j = 0; j < width; j++)
            {
                writer.Write(" " + colorValues[j, i].x + " " + colorValues[j, i].y + " " + colorValues[j, i].z + " ");
            }
            writer.WriteLine(" ");
        }

        writer.Close();
    }

    public void cameraCastRay(Vector3[,] grid)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float temp1 = ((float)i + 1f) / (float) width;
                float temp2 = ((float)j + 1f) / (float) height;
                Ray ray = main.ViewportPointToRay(new Vector3(temp1, temp2, 0));
                Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity);
                Color temp = Color.clear;
                Vector3 ambientIntensity = colorToVector3(Color.black);
                Vector3 actualIntensity = colorToVector3(Color.black);
                Boolean shadow = false;
                if (hit.collider)
                {
                    temp = hit.collider.GetComponent<Renderer>().material.color;
                    ambientIntensity += colorToVector3(lights[0].color) * .25f;
                    foreach (Light light in lights)
                    {
                        actualIntensity += .5f * light.intensity * (Vector3.Dot(hit.normal, Vector3.Normalize(light.transform.position - hit.point ))) * colorToVector3(light.color);
                        actualIntensity += .75f * light.intensity * colorToVector3(light.color) * (float)Math.Pow((double)Vector3.Dot(2 * Vector3.Dot(Vector3.Normalize(light.transform.position - hit.point),hit.normal) * hit.normal - Vector3.Normalize(light.transform.position - hit.point), Vector3.Normalize(main.transform.position - hit.point)),100);
                        if (Physics.Raycast(hit.point, Vector3.Normalize(light.transform.position - hit.point), out hit, Mathf.Infinity))
                        {
                            shadow = true;
                        }
                    }
                }
                if (!shadow)
                {
                    grid[i, j] = (ambientIntensity + actualIntensity + colorToVector3(temp)) * 255;
                } else
                {
                    grid[i, j] = (ambientIntensity + colorToVector3(Color.black)) * 255;
                }
            }
        }
        writeToPPM(outputPPM, width, height, maxColors, grid);
    }

    public Vector3 colorToVector3(Color temp)
    {
        return new Vector3(temp.r, temp.g, temp.b);
    }
}
