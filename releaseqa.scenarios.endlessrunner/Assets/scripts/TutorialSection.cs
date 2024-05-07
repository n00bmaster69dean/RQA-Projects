using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSection : MonoBehaviour
{
    GameObject tutorialCanvas;
    GameObject moveTutorial;
    GameObject jumpTutorial;
    GameObject pickupsTutorial;

    //Tutorial canvas

    void Start()
    {
        tutorialCanvas = gameObject;
        moveTutorial = tutorialCanvas.transform.GetChild(0).gameObject;
        jumpTutorial = tutorialCanvas.transform.GetChild(1).gameObject;
        pickupsTutorial = tutorialCanvas.transform.GetChild(2).gameObject;
        //allocating the tutorial canvases. Please don't change the order of the children!

        HideAll();
        StartCoroutine(TutorialTiming());
    }

    public void HideAll()
    {
        moveTutorial.SetActive(false);
        jumpTutorial.SetActive(false);
        pickupsTutorial.SetActive(false);
    }

    public void ShowAll()
    {
        moveTutorial.SetActive(true);
        jumpTutorial.SetActive(true);
        pickupsTutorial.SetActive(true);
    }

    IEnumerator TutorialTiming()
    {
        //enabling and disabling the gameobjects for a 'tutorial' kind of display
        moveTutorial.SetActive(true);
        yield return new WaitForSeconds(5);
        moveTutorial.SetActive(false);
        jumpTutorial.SetActive(true);
        yield return new WaitForSeconds(5);
        jumpTutorial.SetActive(false);
        pickupsTutorial.SetActive(true);
        yield return new WaitForSeconds(10);
        pickupsTutorial.SetActive(false);
    }
}
