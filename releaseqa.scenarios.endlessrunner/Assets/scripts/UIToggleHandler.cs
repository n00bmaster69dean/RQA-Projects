using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToggleHandler: MonoBehaviour
{
    Toggle UIToggle;
    Canvas gameUI;

    //Enables/disables the stats canvas that displays in game with a toggle.
    void Start()
    {
        UIToggle = gameObject.GetComponent<Toggle>();
        gameUI = GameObject.Find("CurrentStatsCanvas").GetComponent<Canvas>();
        UIToggle.isOn = true;
        UIToggle.onValueChanged.AddListener(delegate { ToggleUI(UIToggle); }) ; 
    }

    private void ToggleUI(Toggle UIToggle)
    {
        if (UIToggle.isOn)
        {
            gameUI.enabled = true;
        }
        else if (!UIToggle.isOn)
        {
            gameUI.enabled = false;
        }
    } 

}
