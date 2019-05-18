using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSphere : MonoBehaviour
{
    //global start of cube
    Vector3 startSpot;

    // Start is called before the first frame update
    void Start()
    {
        startSpot = new Vector3(-2.75f, 0.36f, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        sphereMovements();
    }

    void sphereMovements()
    {
        float turn = 35f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.Translate(Vector3.left * Time.deltaTime);
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.Translate(Vector3.right * Time.deltaTime);
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            //transform.Translate(Vector3.up * Time.deltaTime);
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            //transform.Translate(Vector3.down * Time.deltaTime);
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.B))
        {
            //transform.Translate(Vector3.forward * Time.deltaTime);
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.N))
        {
            //transform.Translate(Vector3.back * Time.deltaTime);
            transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.J))
        {
            transform.position = startSpot;
        }

        else if (Input.GetKey(KeyCode.K))
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
        else if (Input.GetKey(KeyCode.L))
        {
            transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);
        }
        else if (Input.GetKey(KeyCode.Y))
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetKey(KeyCode.U))
        {
            transform.Rotate(Vector3.down * turn * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.I))
        {
            transform.Rotate(Vector3.up * turn * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.O))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
}
