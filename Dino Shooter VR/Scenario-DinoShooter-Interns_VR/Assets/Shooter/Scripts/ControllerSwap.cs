using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerSwap : MonoBehaviour
{

    public GameObject leftController;
    public GameObject rightController;
    public GameObject tree;
    public GameObject rock;

    public OVRInput.Controller controller;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //primary right hand fire
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.GetActiveController()) > 0.5)

        // if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.GetActiveController()))
        {
            // if (OVRInput.GetActiveController() == OVRInput.Controller.RTouch)
            {
                //sets the right hand controller to show and hide the left 
                rightController.SetActive(true);
                leftController.SetActive(false);
               // tree.SetActive(false);

            }
        }
        // if (controller == OVRInput.Controller.LTouch)
        // if (OVRInput.GetActiveController() == OVRInput.Controller.LTouch)

        //secondary left hand fire
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger, OVRInput.GetActiveController()) > 0.5)
            {
            //sets the left hand controller to show and hide the right
            leftController.SetActive(true);
                rightController.SetActive(false);
               // rock.SetActive(false);

            }
        }

    
}
