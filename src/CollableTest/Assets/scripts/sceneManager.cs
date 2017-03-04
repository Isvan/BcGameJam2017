using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour {

    
    int life;
     int score;
    int combo;
    int streak;
    int curShape = 2;
    float hpToRemove;
    public float hpDropSpeed = 1f;
    public float hpDropSpeedInc = 0.0015f;
    GameObject targetObject;
    bool isPaused;

	// Use this for initialization
	void Start () {
        hpToRemove = 1f;
        hpDropSpeed = 1f;
        life = 100;
        score = 0;
        combo = 1;
        targetObject = GameObject.FindGameObjectWithTag("GameObjectTarget");
        curShape = 2;
        isPaused = true;
	}
	
	// Update is called once per frame
	void Update () {

        hpToRemove += Time.deltaTime * hpDropSpeed;
        hpDropSpeed += hpDropSpeedInc;
        if(life < 0)
        {
            SceneManager.LoadScene("scene_yourscore"); //disable this for debugging galore
        }
        
        if(hpToRemove > 1)
        {
            life -= 1;
            hpToRemove -= 1;
            updateLife();
        }


        if (isPaused)
        {
            if (Input.GetButton("Fire1"))
            {
                unPause();
                Debug.Log("GO");
            }
        }
	}

    public void hitGood(int scoreToAdd)
    {

        GameObject canvas = GameObject.Find("Canvas");
        
        if (scoreToAdd != 0)
        {
            streak++;
            canvas.GetComponent<script_popups>().randomHappy();
            GameObject.FindObjectOfType<script_music>().sfxGood();
            updateLife();
        }

        if (streak == 5)
        {
           
            combo++;
            canvas.GetComponent<script_popups>().combo(combo);
            streak = 0;
        }
        score += scoreToAdd;
        life += (scoreToAdd/5)*combo;
        if(life > 100)
        {
            life = 100;
        }

        //PauseGame();
        newBoard();
    }

    public void hitBad(int scoreToRemove)
    {

        if(scoreToRemove/3 < 10 || scoreToRemove/3 > 50)
        {
            life -= 10;
        }else {
            life -= scoreToRemove / 3;
        }
       
        combo = 1;
        streak = 0;
        GameObject canvas = GameObject.Find("Canvas");
        canvas.GetComponent<script_popups>().randomSad();
        GameObject.FindObjectOfType<script_music>().sfxBad();
      
       /// PauseGame();
        newBoard();
    } 

    Object getNewTarget(int selection)
    {

    return Resources.Load("ObjectToHit_"+ selection);

    }

    public int getCombo()
    {
        return combo;
    }

    public int getScore()
    {
        return score;
    }

    void changeTarget()
    {

        Destroy(targetObject);

        GameObject gb = new GameObject();
        Transform trans = gb.transform;

        trans.localPosition = new Vector3(0, 0, 0);
        trans.localRotation = Quaternion.identity;
        trans.localScale = new Vector3(1, 1 ,1);

        //Use a random number gen to select a new target
        float rndNum = Random.Range(2, 29);
        curShape = (int)Mathf.Clamp(rndNum, 2, 29);
        targetObject = (GameObject)Instantiate(getNewTarget(curShape),trans);
        targetObject.transform.SetParent(this.transform);
        Destroy(gb);
    }

    public int getCurShape()
    {
        return curShape;
    }

    public void updateLife()
    {
     
        GameObject.Find("HealthBarFront").GetComponent<healthBar>().updateHealth((float)(life) / 100f);
    }

    void PauseGame()
    {
        isPaused = true;
        targetObject.GetComponent<targetObject>().pause();
    }

    void unPause()
    {
        isPaused = false;
        targetObject.GetComponent<targetObject>().unPause();
    }

    void newBoard()
    {
        changeTarget();
        float amountToScale = Random.Range(0.5f, 1f);
        targetObject.transform.localScale = new Vector3(amountToScale, amountToScale, amountToScale);
        targetObject.GetComponent<targetObject>().resetObjects(2f * Random.Range(2f,4f),curShape);
        unPause();
    }

}
