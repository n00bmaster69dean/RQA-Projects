using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChunk : MonoBehaviour
{
    //Move the floor chunk backwards at a set speed
    private void FixedUpdate()
    {
        Vector3 movementVector = new Vector3(0, 0, GameObject.Find("gamemanager").GetComponent<spawnobsticals>().speed * Time.deltaTime);
        transform.Translate(movementVector);
    }

}
