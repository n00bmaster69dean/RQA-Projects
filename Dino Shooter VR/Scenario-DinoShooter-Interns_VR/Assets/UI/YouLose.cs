using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouLose : MonoBehaviour
{
    public Text level;
    public Text score;
    void Start()
    {
        level.text = GameManager.MaxLevel.ToString();
        score.text = GameManager.dinoScore.ToString();
    }

    public void closeThis(){
        Application.Quit();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }
}
