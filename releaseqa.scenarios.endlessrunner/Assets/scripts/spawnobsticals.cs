using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnobsticals : MonoBehaviour
{
    public GameObject[] cubes;
    public GameObject[] cubes2;
    public GameObject[] cubes3;
    public GameObject[] cubes4;
    public GameObject[] speedPickups;
    public GameObject[] badPickups;
    public GameObject player;
    public float timeleft = 1;
   public float speed;
    public float max;
    public float repeated=0;
    public float speedmax=300;
    public int pizza;


    // Start is called before the first frame update
    void Start()
    {
        max += 10.5f;

    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position;
        if (speed < speedmax)
        max += Time.deltaTime;
        speed = -max;
      //  speed = Random.Range(-10, -max);
  

        timeleft -= Time.deltaTime;
        if (timeleft < 0)
        {
            
            //walls
            if (Random.Range(0, 5) == 1)
            {
                //random
                pizza = Random.Range(0, cubes4.Length);
                    //for (int i = 0; i < cubes4.Length; i++)
                {
                    position = new Vector3(Random.Range(-100.0f, 100.0f), 2.5f, Random.Range(-10.0f, 10.0f));
                    position = position + this.transform.position;
                    position = player.transform.position + position;
                    position = new Vector3(position.x, 2.5f, position.z);
                    Instantiate(cubes4[pizza], position, Quaternion.identity);
                }
            }

            //random cubes
            
            for (int i = 0; i < cubes.Length; i++)
            {
                position = new Vector3(Random.Range(-100.0f, 100.0f), 2.5f, Random.Range(-10.0f, 10.0f));
                position = position + this.transform.position;
                position = player.transform.position + position;
                position = new Vector3(position.x, 2.5f, position.z);
                Instantiate(cubes[i], position, Quaternion.identity);
            }

            //coin
            if (Random.Range(0, 10) == 1)
            {
                for (int i = 0; i < cubes2.Length; i++)
                {
                    position = new Vector3(Random.Range(-100.0f, 100.0f), 2.5f, Random.Range(-10.0f, 10.0f));
                    position = position + this.transform.position;
                    position = player.transform.position + position;
                    position = new Vector3(position.x, 2.5f, position.z);
                    Instantiate(cubes2[i], position, Quaternion.identity);

                }
            }
            if (Random.Range(0, 10) == 1)
            {
                //sheild
                for (int i = 0; i < cubes3.Length; i++)
                {
                    position = new Vector3(Random.Range(-100.0f, 100.0f), 2.5f, Random.Range(-10.0f, 10.0f));
                    position = position + this.transform.position;
                    position = player.transform.position + position;
                    position = new Vector3(position.x, 2.5f, position.z);
                    Instantiate(cubes3[i], position, Quaternion.identity);
                }

                for (int i = 0; i < speedPickups.Length; i++)
                {
                    position = new Vector3(Random.Range(-100.0f, 100.0f), 2.5f, Random.Range(-10.0f, 10.0f));
                    position = position + this.transform.position;
                    position = player.transform.position + position;
                    position = new Vector3(position.x, 2.5f, position.z);
                    Instantiate(speedPickups[i], position, Quaternion.identity);
                }

                for (int i = 0; i < badPickups.Length; i++)
                {
                    position = new Vector3(Random.Range(-100.0f, 100.0f), 2.5f, Random.Range(-10.0f, 10.0f));
                    position = position + this.transform.position;
                    position = player.transform.position + position;
                    position = new Vector3(position.x, 2.5f, position.z);
                    Instantiate(badPickups[i], position, Quaternion.identity);
                }
            }

            if (repeated < 10)
                timeleft = 1;

            repeated += 1;
            if (repeated == 10)
            {
                timeleft = 0.8f;
            }

            if (repeated > 10)
            {
                timeleft = 0.6f;
            }
        }


    }

    private void FixedUpdate()
    {

        //for (int i = 0; i < cubes3.Length; i++)
        //{
        //    Vector3 position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
        //    position = position + this.transform.position;
        //    Instantiate(cubes[i], player.transform.position + position, transform.rotation);
        //}
       
    }
}
