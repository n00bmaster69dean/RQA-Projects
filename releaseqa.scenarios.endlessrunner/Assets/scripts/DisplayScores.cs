using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScores : MonoBehaviour
{
    //scores are set on crashing the player in CurrentStatsController.cs

    GameObject highScoreCanvas;
    TextMeshProUGUI firstPl;
    TextMeshProUGUI secondPl;
    TextMeshProUGUI thirdPl;
    TextMeshProUGUI playerRecentScore;

    int first;
    int second;
    int third;
    int recent;

    void Start()
    {
        highScoreCanvas = this.gameObject;
        firstPl = this.gameObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        secondPl = this.gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        thirdPl = this.gameObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        playerRecentScore = this.gameObject.transform.GetChild(5).GetComponent<TextMeshProUGUI>();
        //Please ensure the script is attached to HighscoreCanvas
        //Don't move the order of any of the children!!!
        LoadScores();
        UpdateLeaderboard();
        CheckMatchingScores();

    }

    void LoadScores()
    {
        first = PlayerPrefs.GetInt("firstPlace", 0);
        second = PlayerPrefs.GetInt("secondPlace", 0);
        third = PlayerPrefs.GetInt("thirdPlace", 0);
        recent = PlayerPrefs.GetInt("playerScore", 0);
        //loads all the saved scores from playerPrefs
    }

    void UpdateLeaderboard()
    {
        firstPl.text = "1st: " + first.ToString();
        secondPl.text = "2nd: " + second.ToString();
        thirdPl.text = "3rd: " + third.ToString();
        playerRecentScore.text = "YOUR SCORE: " + recent.ToString();
    }

    public void ResetAllScores()
    {
        PlayerPrefs.SetInt("firstPlace", 0);
        PlayerPrefs.SetInt("secondPlace", 0);
        PlayerPrefs.SetInt("thirdPlace", 0);
        PlayerPrefs.SetInt("playerScore", 0);
        LoadScores();
        UpdateLeaderboard();
    }

    void CheckMatchingScores()
    {
        //this will indicate if your score earned a place on the leaderboard
        if (recent == first)
        {
            firstPl.color = Color.yellow;
            playerRecentScore.color = Color.yellow;
        }
        else if (recent == second)
        {
            secondPl.color = Color.yellow;
            playerRecentScore.color = Color.yellow;
        }
        else if (recent == third)
        {
            thirdPl.color = Color.yellow;
            playerRecentScore.color = Color.yellow;
        }
    }

}
