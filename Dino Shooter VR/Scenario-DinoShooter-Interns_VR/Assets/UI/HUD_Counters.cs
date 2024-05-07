using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD_Counters : MonoBehaviour
{
    GameObject Player;
    GameManager gm;
    int health;
    public Slider slider;
    public Text dinoText;

    public Text LevelNO;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
      //  Player = GameObject.FindGameObjectWithTag("Player");
        LevelNO.text = GameManager.MaxLevel.ToString();
        dinoText.text = GameManager.dinosShot.ToString();
        slider.maxValue = 10;
        slider.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        dinoText.text = "Level "+GameManager.dinosShot.ToString();
        slider.value = MyPlayer.Health;
    }
}
