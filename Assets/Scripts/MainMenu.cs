using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
	
	public string startLevel;
	public string levelSelect;

	public void NewGame() {
		Application.LoadLevel (startLevel); 	//Call's for the first level.
	}

	public void LevelSelect() {
		Application.LoadLevel (levelSelect);	//Load's selected level.
	}

	public void QuitGame() {
		Debug.Log ("Game Exit");
		Application.Quit ();
	}

}
