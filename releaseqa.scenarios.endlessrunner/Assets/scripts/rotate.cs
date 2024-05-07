using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    Vector3 newdir;
    float speed = 5f;
    public GameObject player;
    //PauseMenu pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        //pauseMenu = GameObject.FindObjectOfType<PauseMenu>();
    }
    void Update()
    {
        //var step = speed * Time.deltaTime;
        if (Time.timeScale != 0f) {
            //right 
            if (Input.GetAxis("Horizontal") > 0)
            {
                if (transform.rotation.z > -0.4f)
                {
                    transform.Rotate(Vector3.forward * -Input.GetAxis("Horizontal"));
                }
            }

            //left
            if (Input.GetAxis("Horizontal") < 0)
            {
                if (transform.rotation.z < 0.4f)
                {
                    transform.Rotate(Vector3.forward * -Input.GetAxis("Horizontal"));
                }

            }
            //no movement
            if (Input.GetAxis("Horizontal") == 0)
            {
                //transform.rotation = Quaternion.Slerp(Quaternion.LookRotation(transform.forward), Quaternion.LookRotation(new Vector3 (0,0,0)), Time.time * 10);
                //newdir = Vector3.RotateTowards(transform.forward, new Vector3(0, 0, 0), 11, 0);
                //transform.rotation = Quaternion.LookRotation(newdir);

                var desiredRotation = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(desiredRotation), Time.deltaTime * speed);

            }

        }
    }
}
