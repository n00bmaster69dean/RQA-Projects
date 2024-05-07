using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothUI : MonoBehaviour
{
    public Transform target;
    public float smoothspeed;
    public Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 desiredposition = target.position + offset;
        Vector3 smoothedposition = Vector3.Lerp(transform.position, desiredposition, smoothspeed);
        transform.position = smoothedposition;
    }
}