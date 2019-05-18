using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Predator : MonoBehaviour
{
    //predator properties
    public float speed;
    public int viewableDistance, viewableAngle;
    private Ray sight;
    public bool preyFound = false;

    // Start is called before the first frame update
    void Start()
    {
        setupPredator();
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
        while (temp < viewableAngle && !preyFound)
        {
            temp++;
            double angleToCheck = transform.eulerAngles.y + currentAngle + temp;
            double angleInRadians = angleToCheck * Math.PI / 180;
            double xvalue = Math.Cos(angleInRadians);
            double zvalue = Math.Sin(angleInRadians);
            if (Physics.Raycast(transform.position, new Vector3((float)xvalue, 0, (float)zvalue), out hit, (float)viewableDistance))
            {
                if (hit.collider.tag == "Wall")
                {
                    Debug.DrawRay(transform.position, new Vector3((float)xvalue, 0, (float)zvalue) * hit.distance, Color.green);
                }
                else if (hit.collider.gameObject.name == "Prey")
                {
                    Debug.DrawRay(transform.position, new Vector3((float)xvalue, 0, (float)zvalue) * hit.distance, Color.yellow);
                    preyFound = true;
                }

            }
        }
        if (preyFound)
        {
            Vector3 temp2 = transform.position - hit.transform.position;
            var temp3 = temp2.magnitude;
            temp2 = temp2 / temp3;
            transform.Translate(new Vector3(temp2.x * speed, 0, temp2.z * speed));
            transform.LookAt(new Vector3(hit.transform.position.x,0, hit.transform.position.z));
        }
        else
        {
            double angleToCheck = transform.rotation.y;
            double angleInRadians = angleToCheck * Math.PI / 180;
            double xvalue = Math.Cos(angleInRadians);
            double zvalue = Math.Sin(angleInRadians);
            transform.Translate(new Vector3((float)xvalue * speed, 0, (float)zvalue * speed));
        }
        preyFound = false;
    }

    public void setupPredator()
    {
        speed = 0.3f;
        name = "Predator";
        viewableDistance = 40;
        viewableAngle = 45;
        sight = new Ray(transform.position, transform.forward);

        //position
        transform.position = new Vector3(UnityEngine.Random.Range(-20, 20), 1, UnityEngine.Random.Range(-20, 20));
    }


    public void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.tag == "Prey")
        {
            Destroy(collision.gameObject);
        }
       
        if(collision.gameObject.tag == "Wall")
        {
            transform.Rotate(Vector3.up, UnityEngine.Random.Range(110, 250), Space.Self);
        }
    }



}


