using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //Canvas is disabled - won't be able to see the pause menu unless you disable it.

    public static bool isPaused;
    Canvas pauseOverlay;
    public GameObject btn1;
    public GameObject btn2; //get these by script in start later

    TutorialSection tutorialCanvas;
    
    void Start()
    {
        pauseOverlay = this.gameObject.GetComponent<Canvas>();
        tutorialCanvas = GameObject.FindObjectOfType<TutorialSection>();
        Disable();
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause")) {
            ToggleOverlay();
        } 
    }

    public void ToggleOverlay()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Enable();
        }
        else
        {
            Disable();
        }
    }

    public virtual void Enable()
    {
        pauseOverlay.enabled = true;
        btn1.SetActive(true);
        btn2.SetActive(true);
        Time.timeScale = 0f;
        //tutorialCanvas.HideAll();
        //pauses all time. Should stop the game and enable overlays.
    }

    public virtual void Disable()
    {
        //tutorialCanvas.ShowAll();
        btn1.SetActive(false);
        btn2.SetActive(false);
        pauseOverlay.enabled = false;
        Time.timeScale = 1.0f;
    }
}
