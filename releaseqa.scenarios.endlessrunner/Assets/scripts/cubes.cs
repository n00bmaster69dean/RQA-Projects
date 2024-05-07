using UnityEngine;
using System.Collections;

public class cubes : MonoBehaviour
{
    //Variables


    public Rigidbody rb;
    public float random;
    public float max;
    public float timeleft = 50f;

  //  public spawnobsticals boss;


    private void Awake()
    {
        timeleft = 50f;

    }

    private void Start()
    {
 

        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, 0, GameObject.Find("gamemanager").GetComponent<spawnobsticals>().speed), ForceMode.Impulse);

    }
    void Update()
    {
        rb.velocity = new Vector3 (0,0,0);
        Vector3 movementVector = new Vector3(0, 0, GameObject.Find("gamemanager").GetComponent<spawnobsticals>().speed * Time.deltaTime);
        transform.Translate(movementVector);
        //rb.AddForce(new Vector3(0, 0, GameObject.Find("gamemanager").GetComponent<spawnobsticals>().speed), ForceMode.Impulse);


        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
           Destroy(this.gameObject);
        }

    }
        
        
    
}