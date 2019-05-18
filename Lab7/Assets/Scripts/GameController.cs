using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //public Projectile proj;
    public Rigidbody proj;
    public float speed = 150f;
    public float force = 20f;
    public Camera main;
    public int points;
    public Chunk1 chunk1;
    public Chunk2 chunk2;
    public Chunk3 chunk3;
    public Chunk4 chunk4;
    public Chunk5 chunk5;
    public Chunk6 chunk6;

    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //since the camera is the player, these controls move the camera
        if(Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left);
        }
        else if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right);
        }
        else if(Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back);
        }
        else if(Input.GetKey(KeyCode.Z))
        {
            transform.Rotate(Vector3.down * 55f * Time.deltaTime, Space.World);
        }
        else if(Input.GetKey(KeyCode.X))
        {
            transform.Rotate(Vector3.up * 55f * Time.deltaTime, Space.World);
        }
        else if(Input.GetKey(KeyCode.C))
        {
            transform.Rotate(Vector3.left * 55f * Time.deltaTime, Space.World);
        }
        else if (Input.GetKey(KeyCode.F))
        {
            transform.Rotate(Vector3.right * 55f * Time.deltaTime, Space.World);
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            //fires the projectile
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "Wall")
            {
                Destroy(hit.collider.gameObject);
                chunk1.start(hit.point);
                chunk2.start(hit.point);
                chunk3.start(hit.point);
                chunk4.start(hit.point);
                chunk5.start(hit.point);
                chunk6.start(hit.point);
            }
        }
        Rigidbody project = Instantiate(proj, main.transform.position, Quaternion.LookRotation(hit.normal));
        project.velocity = transform.TransformDirection(Vector3.forward * 300);
    }
}
