using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelStart : MonoBehaviour
{
    public static bool isActive = false;
    public GameObject panel;
    public Text countdown;
    public Text impulse;
    public Text Level;
    float timer;
    public bool isMenuScreen = false;

    // Start is called before the first frame update
    void Start()
    {
        Level.text = "Level "+GameManager.MaxLevel.ToString();
        impulse.text = "";
        timer = 0;
        if (isMenuScreen) isActive = true; // allows you to shoot without having to wait for the countdown
        else isActive = false;
    }
    void Update()
    {
        if (!isActive)
        {
            timer += Time.deltaTime;
            if (timer < 1)
            {
                countdown.text = "3";
            }
            if (timer > 1 && timer < 2)
            {
                countdown.text = "2";
            }
            if (timer > 2 && timer < 3)
            {
                countdown.text = "1";
            }
            if (timer > 3 && timer < 4)
            {
                countdown.text = "";
                Level.text = "";
                impulse.text = "Shoot all the dinosaurs!";
            }
            if (timer > 4)
            {
                panel.SetActive(false);
                isActive = true;
            }
        }
    }

    public bool returnIsActive() //allows sounds.cs to access the isActive value
    {
        return isActive;
    }
}
