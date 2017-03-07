using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

	public Transform[] backgrounds;		//Array of back/fore-grounds to be paralaxed
	private float[] parallaxScales; 	   //The proportion of the camera's movement 
	public float smoothing;           //How smooth the paralax is going to be(Above 0)

	private Transform cam;			 //Reference ti the main cameras transform
	private Vector3 previousCamPos; //Position of the camera in the previous frame

	void Awake (){
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () {
		previousCamPos = cam.position;

		parallaxScales = new float[backgrounds.Length];
		//assigning corresponding parallax scales
		for(int i = 0; i < backgrounds.Length; i++){
			parallaxScales[i] = backgrounds[i].position.z*-1;
		}
	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < backgrounds.Length; i++) {
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales [i];

			//set a target x position which is the current position plus the parallax
			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			//create a target poisition 
			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}
		previousCamPos = cam.position;
	}
}
