using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LifeManager : MonoBehaviour {

	private Text theText;

	// Use this for initialization
	void Awake () {
		theText = GetComponent<Text> ();	
	}
	
	// Update is called once per frame
	void Update () {
		theText.text = "x " + GameMaster.RemainingLives;

	}
}
		
