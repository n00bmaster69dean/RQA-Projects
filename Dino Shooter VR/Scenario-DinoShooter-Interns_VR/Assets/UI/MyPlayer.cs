using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : MonoBehaviour
{

    public static int Health=10;
    // Start is called before the first frame update
    void Start()
    {


    }

    void Update()

    {
        //if (Input.GetKeyDown("left")|| Input.GetKeyDown("a"))
        //{
        //    this.transform.rotation *= Quaternion.Euler(0, -90, 0);
        //}


        //if (Input.GetKeyDown("right")|| Input.GetKeyDown("d"))
        //{
        //    this.transform.rotation *= Quaternion.Euler(0, 90, 0);
        //}
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy"){

            Health=Health-1;
        }
    }
}
