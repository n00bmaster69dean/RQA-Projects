using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScoreManager : MonoBehaviour
{
    //Handles saving the score at the end of a game.
    bool crashed = false; //Prevents the second item tagged as "Player" in the scene from logging it's own high score.

    CurrentStatsController currentStatsController;
    SceneHandler sceneHandler;

    int thisScore;
    int firstPl;
    int secondPl;
    int thirdPl;

    void Start()
    {
        LoadScores(); //Retrieves the scores from playerPrefs
        currentStatsController = GameObject.FindObjectOfType<CurrentStatsController>();
        sceneHandler = FindObjectOfType<SceneHandler>();
    }

    void LoadScores()
    {
        //If no score exists, set score to 0
        firstPl = PlayerPrefs.GetInt("firstPlace", 0);
        secondPl = PlayerPrefs.GetInt("secondPlace", 0);
        thirdPl = PlayerPrefs.GetInt("thirdPlace", 0);
    }

    void SaveScores()
    {
        PlayerPrefs.SetInt("firstPlace", firstPl);
        PlayerPrefs.SetInt("secondPlace", secondPl);
        PlayerPrefs.SetInt("thirdPlace", thirdPl);
        PlayerPrefs.SetInt("playerScore", thisScore);
    }

    public void Crash()
    {
        if (!crashed)
        {
            thisScore = currentStatsController.GetPoints();
            thisScore += currentStatsController.GetTime();
            if (thisScore > firstPl)
            {
                int tempValue = firstPl;
                int tempValue2 = secondPl;
                firstPl = thisScore;
                secondPl = tempValue;
                thirdPl = tempValue2;

            }
            else if (thisScore > secondPl)
            {
                int tempValue = secondPl;
                secondPl = thisScore;
                thirdPl = tempValue;
            }
            else if (thisScore > thirdPl)
            {
                thirdPl = thisScore;
            }
            SaveScores();
            sceneHandler.LoadSceneName("GameOver");

            crashed = true;
        }
    }

}
