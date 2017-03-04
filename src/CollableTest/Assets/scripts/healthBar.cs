using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updateHealth(float size)
    {
        //Vector3 dim = gameObject.GetComponent<SpriteRenderer>().bounds.size;

       // float moveAmount = (dim.x/2) * (1 -  size);
        transform.localScale = new Vector3(size,1,1);
        Vector3 pos = transform.localPosition;
        transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
    }

}
