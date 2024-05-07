using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{


    public Transform target;

    public float smoothspeed;

    public Vector3 offset;




    void Start()
    {


    }

    private void FixedUpdate()
    {
       

                Vector3 desiredposition = target.position + offset;
                Vector3 smoothedposition = Vector3.Lerp(transform.position, desiredposition, smoothspeed);
                transform.position = smoothedposition;

        //transform.LookAt(target);
        //transform.position = new Vector3(this.transform.position.x, this.transform.position.y, player.transform.position.z);


    }

    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(0.8f);
        {
            //p = true;
        }
    }
}

