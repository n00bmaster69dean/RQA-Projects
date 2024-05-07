

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrier : MonoBehaviour
{
    public float timeleft = 1;
    public bool now = true;
    Vector3 oldtransform;
    // Start is called before the first frame update
    void Start()
    {
        oldtransform = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //timeleft -= Time.deltaTime;
        //if (timeleft <= 0)
        //{
        //    if (now)
        //    {
        //        transform.position = new Vector3(this.transform.position.x + 0.1f, this.transform.position.y, this.transform.position.z);

        //    }
        //    if (oldtransform.x + 1.5f <= transform.position.x)
        //    {
        //        now = false;

        //    }

        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            transform.position = new Vector3(this.transform.position.x + 0.1f, this.transform.position.y, this.transform.position.z);
        }
    }
}