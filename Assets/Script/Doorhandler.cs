using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorhandler : MonoBehaviour
{
    [SerializeField]
    float speed = 0.1f;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void FixedUpdate()
    {
        this.transform.position += new Vector3(0, 0, -1 * speed);
    }
}
