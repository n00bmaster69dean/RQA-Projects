using UnityEngine;
using System.Collections;

public class trail : MonoBehaviour
{
    //Variables


    public Rigidbody rb;
    public float random;
    public float max;
   
    public Movement player;

    private void Start()
    {


        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, 0, GameObject.Find("gamemanager").GetComponent<spawnobsticals>().speed), ForceMode.Impulse);

    }
    void Update()
    {
        rb.velocity = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        Vector3 movementVector = new Vector3(0, 0, GameObject.Find("gamemanager").GetComponent<spawnobsticals>().speed * Time.deltaTime);
        transform.Translate(movementVector);
        //rb.AddForce(new Vector3(0, 0, GameObject.Find("gamemanager").GetComponent<spawnobsticals>().speed), ForceMode.Impulse);


        

    }



}