using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupIndicatorHandler : MonoBehaviour
{
    public float messageDisplayLength = 3f;
    float timer;
    GameObject indicatorMessage;
    bool active = false;

    //This script is used by PickupMessage.cs

    void Start()
    {
        ResetTimer(); //Stops the game message from disappearing if a new message is displayed.
        indicatorMessage = GameObject.Find("DisplayPickupEffect").gameObject;
    }

    void Update()
    {
        if (active) timer -= Time.deltaTime; //"active" indicates that the message is displayed on screen
        if (timer < 0)
        {
            indicatorMessage.GetComponent<TextMeshProUGUI>().text = "";
            active = false;
        }
    }

    public void UpdateMessage(string message) //Used by PickupMessage.cs
    {
        active = true;
        ResetTimer();
        indicatorMessage.GetComponent<TextMeshProUGUI>().text = message;
    }

    void ResetTimer()
    {
        timer = messageDisplayLength;
    }
}
