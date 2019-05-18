using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeController : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    //calls move slope method
    void Update()
    {
        moveSlope();
    }

    //commands to move slope 
    private void moveSlope()
    {
        float turn = 45f;


        //moves slope object forward
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime, Space.Self);

        }

        //moves slope object back
        if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime, Space.Self);
        }

        //moves slope object left
        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime, Space.Self);
        }

        //moves slope object right
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime, Space.Self);
        }

        //rotates slope object about the y-axis
        if(Input.GetKey(KeyCode.F))
        {
            transform.Rotate(Vector3.up * turn * Time.deltaTime, Space.Self);
        }
    }
}
