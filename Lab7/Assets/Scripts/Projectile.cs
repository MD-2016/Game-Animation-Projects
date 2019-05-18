using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    Vector3 shootDirection;

    private void FixedUpdate()
    {
        speed = 10f;
        this.transform.Translate(shootDirection * speed, Space.World);

    }

    public void FireProj(Ray shoot)
    {
        this.shootDirection = shoot.direction;
        this.transform.position = shoot.origin;
    }

    private void OnCollisionEnter(Collision collision)
    {
    }
}
