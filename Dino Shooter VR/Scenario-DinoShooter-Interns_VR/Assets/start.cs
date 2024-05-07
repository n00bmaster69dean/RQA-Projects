using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{

    string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of this scene.
        sceneName = currentScene.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.GetActiveController()) > 0.5)
        {
            if (sceneName == "Menu")
            {
                SceneManager.LoadScene("Level1");
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }
        }

    }
}
