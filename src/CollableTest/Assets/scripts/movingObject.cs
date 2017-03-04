using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingObject : MonoBehaviour {

    private bool canRotate = true;
    public float zoomInSpeed = 0.01f;
    public float rotateSpeed = 2.5f;


    private bool grow = false;

    float currentRotation;
    bool isPaused;
    Rigidbody2D body;
    float scale;
    // Use this for initialization
    void Start()
    {
        //On first start we just take the value from the editor
        scale = transform.localScale.y;
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {

        //   int combo = gameObject.GetComponentInParent<GameObject>().GetComponentInParent<sceneManager>().getCombo();
        int combo = FindObjectOfType<sceneManager>().GetComponent<sceneManager>().getCombo();
        if (!isPaused)
        {
            float zoomBoost = 1f;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                zoomBoost = 3f;
            }

            if (scale > 0 && !grow)
            {
                scale -= zoomBoost * zoomInSpeed/2 * (1 + (combo/5));
                transform.localScale = new Vector3(scale, scale, scale);
            }else if (scale < 10 && grow) {

                scale += zoomInSpeed/2 * (1 + (combo / 5));
                transform.localScale = new Vector3(scale, scale, scale);

            }
            else
            {
                //Object is too small, tell the sceneManager that we are done, it should stop everything
                GameObject scene = GameObject.Find("sceneManager");
                scene.GetComponent<sceneManager>().hitBad(100);

            }


            if (canRotate)
            {

                float xValMov = Input.GetAxis("Horizontal");
                float yValMov = Input.GetAxis("Vertical");

                Vector3 movement = transform.localPosition;
                movement.x += xValMov * Time.deltaTime * combo;
                movement.y += yValMov * Time.deltaTime * combo;

                transform.localPosition = movement;

                Quaternion quaternRot = Quaternion.identity;

                float xValRot = 0;

                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    xValRot = -1;
                }else if (Input.GetKey(KeyCode.RightArrow))
                {
                    xValRot = 1;
                }

                quaternRot = transform.localRotation;
                currentRotation = quaternRot.eulerAngles.z;
                Vector3 newRotation = new Vector3(0, 0, currentRotation + xValRot * rotateSpeed);
        
                quaternRot.eulerAngles = newRotation;
                transform.localRotation = quaternRot;
            }

        }
    }

    public void setIsGrow()
    {
        grow = true;
    }
    //After either a good or bad hit, this will be called when the next set of target / objects go
    public void Reset(float newSize)
    {
    
        //On reset we do what the parent tells us
        isPaused = false;
        scale = newSize;
        transform.localRotation = Quaternion.identity;

        Quaternion newRot = Quaternion.identity;

        float rand = Random.Range(0f, 180f);

        newRot.eulerAngles = new Vector3(0, 0, rand);

        //Move to random place one the screen to fuck with you
        float posX = Random.Range(-0.8f,0.8f);
        float posY = Random.Range(-0.8f, 0.8f);

        transform.localPosition = new Vector3(posX, posY, 0);
        transform.localRotation = newRot;


    }

    //Set the pause
    public void setIsPaused(bool isPaused)
    {
        this.isPaused = isPaused;
    }

    //Physics updates, but we arnt using those so what ever
    private void FixedUpdate()
    {
        

       
    }

}
