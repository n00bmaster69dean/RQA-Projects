using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupMessage : MonoBehaviour
{
    public string messageToDisplay;
    PickupIndicatorHandler pickupIndicatorHandler;
    public AudioClip soundEffect;

    // Place this script on pickup prefabs. Add the message to display in the inspector.
    void Start()
    {
        pickupIndicatorHandler = GameObject.FindObjectOfType<PickupIndicatorHandler>();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.clip = soundEffect;
            audio.Play();
            //audio.PlayOneShot(soundEffect, 1f);
            pickupIndicatorHandler.UpdateMessage(messageToDisplay);
        }
    }

}
