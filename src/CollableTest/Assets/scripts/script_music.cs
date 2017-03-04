using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class script_music : MonoBehaviour {

    static bool AudioBegin = false;

    new GameObject audio;
    AudioSource[] sources;
    Dictionary<string, AudioSource> afx;
    bool scenemenu = false;
    bool sceneplay = false;
    bool scenescore = false;
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);
        
    }

    void Awake()
    {
        audio = GameObject.Find("audio_menu");
        sources = GameObject.FindObjectsOfType<AudioSource>();
        afx = new Dictionary<string, AudioSource>();
        for (int i=0; i < sources.Length; i++)
        {
            //print(sources[i].name);
            DontDestroyOnLoad(sources[i]);
            afx.Add(sources[i].name, sources[i]);
        }


        //print(sources.Length);
        //if(sources.Length > 6)
        //{
        //    Destroy(sources[6]);
        //    Destroy(sources[5]);
        //    Destroy(sources[4]);
        //    Destroy(sources[3]);
        //    Destroy(sources[2]);
        //    Destroy(sources[1]);
        //}

        //if (audio == null)
        //{
        //    print("Audio null");
        //    audio = this.gameObject;
        //    audio.name = "audio_menu";
        //    DontDestroyOnLoad(audio);


        //}
        //else
        //{
        //    print("Audio exists already!!");
        //    if (this.gameObject.name != "audio_menu")
        //    {
        //        print("Destroying this object");
        //        Destroy(this.gameObject);
        //    }
        //    else
        //    {
        //        print("Not destroying this object");
        //        DontDestroyOnLoad(audio);
        //    }
        //}
       

    }
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "scene_play")
        {
            //SqliteDatabase db;
            //AudioSource rawr =    afx["audio_game_0"];
            //  rawr.Play();
            // 
            if (!sceneplay)
            {
                afx["audio_menu"].Stop();
                afx["audio_game_0"].Play();
                sceneplay = true;
                scenemenu = false;
                scenescore = false;
            }

            // this.audio.SetActive(false);
            //Destroy(this.gameObject);
            //  AudioBegin = false;
            //print("music stopping");
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
            {
                SceneManager.LoadScene("scene_yourscore");
            }
        }
        else if (SceneManager.GetActiveScene().name == "scene_yourscore")
        {
            if (!scenescore)
            {
                afx["audio_game_0"].Stop();
                afx["audio_score"].Play();
                scenescore = true;
                sceneplay = false;
                scenemenu = false;

            }

            if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu");
            }

            // print("score");
            // print("keep the music playing");

        }
        else if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            //print("menu");
            //this.audio.SetActive(false);
            if (!scenemenu)
            {
                afx["audio_score"].Stop();
                afx["audio_menu"].Play();
                scenemenu = true;
                scenescore = false;
                sceneplay = false;
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("scene_play");
            }

        } else if (SceneManager.GetActiveScene().name == "scene_credits"|| SceneManager.GetActiveScene().name == "scene_tutorial_and_controls")
        {
            if(Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    public void sfxBad()
    {
        int x = (Random.Range(0, 1) > 0.5) ? 0 : 1;
        afx["audio_sfx_bad_"+x].Play();
        

    }

    public void sfxGood()
    {
        int x = (Random.Range(0, 1) > 0.5) ? 0 : 1;
        afx["audio_sfx_good_" + x].Play();

    }
}
