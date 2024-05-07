using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //Hold all the weapons in the scene inside the WeaponManager, and drag them into this list into the inspector.

    public GameObject[] weapons;
    GameObject currentWeapon;
    int i;


    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        WeaponSwitch(i);
    }

    // Update is called once per frame
    void Update()
    {
        TakeUserWeaponSwitch();
    }

    void TakeUserWeaponSwitch()
    {
        OVRInput.GetActiveController();
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.GetActiveController()))
        {
            Debug.Log("E pressed");
            if (i < weapons.Length - 1)
            {
                i++;
            }
            else
            {
                i = 0;
            }
            WeaponSwitch(i);
        }

        if (OVRInput.GetDown(OVRInput.Button.Three, OVRInput.GetActiveController()))
        {
            Debug.Log("Q pressed");
            if (i > 0)
            {
                i--;
            }
            else
            {
                i = weapons.Length - 1;
            }
            WeaponSwitch(i);
        }

        //if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.GetActiveController()) > 0.5)
        //{
        //    shoot.bulletcount = 30;
        //}

        //if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.GetActiveController()) > 0.5)
        //{
        //    shoot.bulletcount = 30;
        //}
    }

    void WeaponSwitch(int i)
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
        weapons[i].SetActive(true);
    }
}
