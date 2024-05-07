using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class day : MonoBehaviour
{
    // Script to make the directional light rotate to make a day/night cycle
    void Update()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * 10f);
    }
}
