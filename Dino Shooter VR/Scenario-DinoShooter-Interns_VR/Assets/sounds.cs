using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using U4K.BaseScripts;
using U4K.BehaviourTemplate.Attr;

public class sounds : MonoBehaviour
{
    public AudioSource snd;
    public AudioClip shoot;
    public AudioClip reload;
    public bool check = false;

    public GameObject weapon;
    public GameObject weapon360;
    public GameObject weapon1;


    public U4K.BaseScripts.ShootBase shootBase;
    public U4K.BaseScripts.ShootBase shootBase360;
    public U4K.BaseScripts.ShootBase shootBase1;

    LevelStart levelStart;

    // public AudioClip ;
    // Start is called before the first frame update
    void Start()
    {
        ///shootBase.bulletcount = 0;
        levelStart = GameObject.FindObjectOfType<LevelStart>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.GetActiveController()) > 0.8)||Input.GetKeyDown("left"))
        {
            if(!snd.isPlaying)
            snd.PlayOneShot(reload);
        }
        //print(check);
        if ((OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.GetActiveController()))|| Input.GetKeyDown("right"))
        {
            Debug.Log(shootBase.bulletcount);
            if (levelStart.returnIsActive()) //this function stops the gun from making shooting sounds during the countdown
            {
                if (shootBase.bulletcount > 0 && weapon.activeSelf)
                {

                    if (!snd.isPlaying)
                        snd.PlayOneShot(shoot);
                }
                if (shootBase360.bulletcount > 0 && weapon360.activeSelf)
                {

                    if (!snd.isPlaying)
                        snd.PlayOneShot(shoot);
                }
                if (shootBase1.bulletcount > 0 && weapon1.activeSelf)
                {

                    if (!snd.isPlaying)
                        snd.PlayOneShot(shoot);
                }
            }
        }
    }
}
