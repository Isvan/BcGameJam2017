using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShader : MonoBehaviour {

    public float intensity;
    private Material material;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(Shader.Find("Hidden/shader1"));
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (intensity == 0)
        {
            Graphics.Blit(source, destination);
            return;
        }

        material.SetFloat("_bwBlend", intensity);
        Graphics.Blit(source, destination, material);
    }

}
