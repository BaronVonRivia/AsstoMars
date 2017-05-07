using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour {
	
	public string startLevel;
	public string levelSelect;

	public void NewGame() {
		SceneManager.LoadScene (startLevel); 	//Call's for the first level.
	}

	public void LevelSelect() {
		SceneManager.LoadScene (levelSelect);	//Load's selected level.
	}

	public void QuitGame() {
		Debug.Log ("Game Exit");
		Application.Quit ();
	}

}
