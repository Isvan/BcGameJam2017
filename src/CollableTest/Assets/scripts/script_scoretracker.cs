using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class script_scoretracker : MonoBehaviour {

    // Use this for initialization
    int score = 0;
    float time = 0;
	void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
        if(SceneManager.GetActiveScene().name == "scene_play")
        {
            this.time += Time.deltaTime;
            int curscore = GameObject.FindObjectOfType<sceneManager>().getScore();
            this.score = (this.score > curscore) ? this.score : curscore;

        }
        else if (SceneManager.GetActiveScene().name == "scene_yourscore")
        {
            GameObject score = GameObject.Find("scoreHolder");
            GameObject text = GameObject.Find("text_score");
            text.GetComponent<Text>().text = this.score.ToString();
          //  GameObject timer = GameObject.Find("text_time_1");
            GameObject.Find("text_time_1").GetComponent<Text>().text = this.time.ToString();
        }
        else if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            this.time = 0;
            this.score = 0;
        }
    }
}
