using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    public int speedIncreaseValue;
    Movement movement;
    public int pickupValue;
    CurrentStatsController currentStatsController;

    //note that this is just for the player movement from side to side, not the speed of going forwards.
    void Start()
    {
        movement = FindObjectOfType<Movement>();
        currentStatsController = FindObjectOfType<CurrentStatsController>();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" && other.gameObject.name == "player")
        {
            movement.speedUpgrade(speedIncreaseValue);
            currentStatsController.AddPoints(pickupValue);

            StartCoroutine(WaitToPlay());
        }
    }

    IEnumerator WaitToPlay()
    {
        //This will give time for the audioclip to play before destroying the object
        //while we are still able to hide the item from view.
        this.transform.GetChild(0).gameObject.SetActive(false);
        //this.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
