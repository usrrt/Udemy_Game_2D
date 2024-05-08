using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public float speed;

    public void MoveTarget(Vector3 target)
    {
        target.z = -10;
        transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * speed);
    }
}
