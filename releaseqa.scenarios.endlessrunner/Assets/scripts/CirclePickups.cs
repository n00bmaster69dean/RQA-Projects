using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePickups : MonoBehaviour
{
    public int value = 0;
    CurrentStatsController currentStatsController;
    public bool negativePickup = false; //ticking this will make it a 'negative' pickup

    void Start()
    {
        //will allow us to affect the score on the canvas.
        currentStatsController = FindObjectOfType<CurrentStatsController>();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" && other.gameObject.name == "player")
        {
            if(!negativePickup) currentStatsController.AddPoints(value);
            if(negativePickup) currentStatsController.PointPenalty(value);
            StartCoroutine(WaitToPlay());
        }
    }

    IEnumerator WaitToPlay()
    {
        //this.GetComponent<MeshRenderer>().enabled = false;
        this.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
