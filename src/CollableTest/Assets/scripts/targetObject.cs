using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetObject : MonoBehaviour {


    GameObject child;
    GameObject scene;
    public int scoreMod = 100;

    bool isTouching;
    bool isPaused;

	// Use this for initialization
	void Start () {
       
        isTouching = false;     
        isPaused = false;
        scene = GameObject.Find("sceneManager");
        int shape = gameObject.GetComponentInParent<sceneManager>().getCurShape();
        resetObjects(3f,shape);
    }
	
	// Update is called once per frame
	void Update () {

        if (!isPaused)
        {

            if (Input.GetButtonDown("Jump"))
            {

                //If its touched and we tried to lock, that means we were to big/had bad rotation
                if (isTouching)
                {


                    float parentSize = transform.localScale.x;
                    float childSize = child.transform.localScale.x;
                    //Since child size is relative to parent size we need some maths
                    childSize *= parentSize;

                    //Because the if parent hit box is'nt as big as the size of it, its impossible to get the natural 100% score, just use the score mod to adjust
                    int score = (int)(scoreMod * parentSize / childSize);

                    scene.GetComponent<sceneManager>().hitBad(score);

                }
                else
                {
                    //We are good to go

                    //Just sample the x value
                    float parentSize = transform.localScale.x;
                    float childSize = child.transform.localScale.x;
                    //Since child size is relative to parent size we need some maths
                    childSize *= parentSize;

                    //Because the if parent hit box is'nt as big as the size of it, its impossible to get the natural 100% score, just use the score mod to adjust
                    int score = (int)(scoreMod * childSize / parentSize);

                    if(child.transform.localPosition.x > 0.4 || child.transform.localPosition.y > 0.4 || child.transform.localPosition.y < -0.4 || child.transform.localPosition.x < -0.4)
                    {
                        scene.GetComponent<sceneManager>().hitBad(score);

                    }else
                    {
                        scene.GetComponent<sceneManager>().hitGood(score);

                    }


                }

            }
        }
	}

    public void pause()
    {
        child.GetComponent<movingObject>().setIsPaused(true);
        isPaused = true;
    }

    public void unPause()
    {
        child.GetComponent<movingObject>().setIsPaused(false);
        isPaused = false;
    }

    Object getNewChild(int pos)
    {
        return Resources.Load("ObjectTohitWith_"+pos);
    }

    public void createNewChild(int childNum)
    {
        
        if(child != null)
        {
            Destroy(child);
        }
       

           GameObject gb = new GameObject();
           Transform trans = gb.transform;
            
            trans.localPosition = new Vector3(0, 0, 0);
            trans.localRotation = Quaternion.identity;
            trans.localScale = new Vector3(2, 2, 2);
              
     
          Object obj = getNewChild((childNum));
        //  Object obj = getNewChild(2);

       
        child = (GameObject)Instantiate(obj, trans);
        child.transform.SetParent(this.transform);
        child.transform.localPosition = new Vector3(0,0,0);
        Destroy(gb);
    }

    public void resetObjects(float newSize,int objectNum)
    {
        //Possibly move the target here, Since the target is the parrent if we move the child will follow
        createNewChild(objectNum);
        if (Random.Range(1, 2) > 1)
        {
           
            child.GetComponent<movingObject>().setIsGrow();
            child.GetComponent<movingObject>().Reset(0.25f);
        }else{
            child.GetComponent<movingObject>().Reset(newSize);

        }
        //child.GetComponent<movingObject>().setIsPaused(false);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (!isTouching)
        {
            isTouching = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isTouching) {
            isTouching = false;

            } 
    }

}
