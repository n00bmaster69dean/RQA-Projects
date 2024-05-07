using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int MaxLevel= 1;
    public static int dinoScore = 0;
   public static int levelUP = 5;
   public static int dinosShot = 0;
    //public bool check = true;
    // Start is called before the first frame update
    void Start()
    {
        //check = true;
        dinosShot = 0;
    } 

    // Update is called once per frame
    void Update()
    {
        if(MyPlayer.Health>0){
            CheckNextLevel();
        }else{
             DisplayEndScene();
            MyPlayer.Health = 10; //stops the final screen constantly trying to load itself.
        }

    }

    void CheckNextLevel(){
        if(dinosShot>= levelUP){
            levelUP = levelUP+5;
            dinoScore= dinoScore+dinosShot;
            MaxLevel++;
            int scene = SceneManager.GetActiveScene().buildIndex;
            int nextscene = 0;
            if (scene<=4)
            {
                nextscene = scene+1;
            }
            SceneManager.LoadSceneAsync(nextscene);
        }
    }

    void DisplayEndScene(){
        // SceneManager.LoadSceneAsync(5);
        SceneManager.LoadScene("DeathScreen"); //just because the death scene is currently not the 5th in build!
    }
}
