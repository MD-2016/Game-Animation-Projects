using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{

    //global start of cube
    Vector3 startSpot;

    // Start is called before the first frame update
    void Start()
    {
        startSpot = new Vector3(0,0.5f,0);
    }

    // Update is called once per frame
    void Update()
    {
        //method is called which handles the cube movements.
        CubeMovements();

    }


    void CubeMovements()
    {
        float turn = 35f; 

        if (Input.GetKey(KeyCode.A))
        {
            //transform.Translate(Vector3.left * Time.deltaTime);
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //transform.Translate(Vector3.right * Time.deltaTime);
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            //transform.Translate(Vector3.up * Time.deltaTime);
           transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            //transform.Translate(Vector3.down * Time.deltaTime);
            transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            //transform.Translate(Vector3.forward * Time.deltaTime);
            transform.Translate(new Vector3(0, 0, 1) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            //transform.Translate(Vector3.back * Time.deltaTime);
            transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.R))
        {
            transform.position = startSpot;
        }

        else if(Input.GetKey(KeyCode.Z))
        {
            transform.localScale += new Vector3(0.1f,0.1f, 0.1f);
        }
        else if(Input.GetKey(KeyCode.X))
        {
            transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);
        }
        else if(Input.GetKey(KeyCode.C))
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if(Input.GetKey(KeyCode.F))
        {
            transform.Rotate(Vector3.down * turn * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.G))
        {
            transform.Rotate(Vector3.up * turn * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.H))
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
}
