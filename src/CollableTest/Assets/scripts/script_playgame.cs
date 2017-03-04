using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class script_playgame : MonoBehaviour {

    // Use this for initialization
    void Start() {
        // Application.LoadLevel("dev");

    }

    // Update is called once per frame
    void Update() {

    }

    public void click_playgame()
    {
        SceneManager.LoadScene("scene_play");

    }

    public void click_tutorial()
    {
        SceneManager.LoadScene("Scene_tutorial_and_controls");
    }

    public void click_mainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }

    public void click_exit()
    {

        Application.Quit();
        print("It will quit in production, trust me");
    }

    public void click_credits()
    {
        SceneManager.LoadScene("scene_credits");
    }
}
