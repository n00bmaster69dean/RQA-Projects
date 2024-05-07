using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrentStatsController : MonoBehaviour
{
    private TextMeshProUGUI pointsText;
    private TextMeshProUGUI timeText;
    private int points;
    private float timer = 0f;
    
    void Start()
    {
        //This script is attached to the canvas holding "PointsIndicator" and "TimeIndicator"
        //Please do not edit the order of the canvas children.

        points = 0;
        pointsText = this.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        timeText = this.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

    }

    void Update()
    {
        UpdateTime();
    }

    private void UpdateTime()
    {
        timer += Time.deltaTime;
        timeText.text = "Time: " + timer.ToString("N0"); //confines timer to seconds (0 decimal places)
    }

    public int GetPoints() //for other functions to access the player points
    {
        return points;
    }

    public int GetTime() //for other functions to access the player points
    {
        return (int)timer;
    }



    // // // COLLECTABLES // // // // // /////////////////////////////////////////
    public void AddPoints(int pointsToAdd) //for 'coin' collectables
    {
        points += pointsToAdd;
        pointsText.text = "Points: " + points.ToString();
    }

    public void PointPenalty(int pointsToRemove) //negative collectable
    {
        points -= pointsToRemove;
        pointsText.text = "Points: " + points.ToString();

    }

    // // // //////////// // // // // // /////////////////////////////////////////

}
