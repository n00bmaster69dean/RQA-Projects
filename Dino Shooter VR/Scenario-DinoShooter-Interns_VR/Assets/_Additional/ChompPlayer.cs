using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using U4K.BaseScripts;
using U4K.BehaviourTemplate.Attr;

public class ChompPlayer : MonoBehaviour
{
    //Script is used to make the dino stop and start the eating animation when they reach the TriggerDinoEating trigger
    //The trigger is in OVRCameraRig
    //Attached to the T-Rex prefab

    Animator animator;
    U4K.BehaviourTemplate.Move2TargetTemplate m2dt; //different namespace

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        m2dt = gameObject.GetComponent<U4K.BehaviourTemplate.Move2TargetTemplate>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("isEating", true);
            m2dt.ToggleActive(); //this will stop the dino from continuously moving towards the player.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("isEating", false);
            m2dt.ToggleActive();
        }
    }
}
