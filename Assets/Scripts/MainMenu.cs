using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
=======
using UnityEngine.SceneManagement;
>>>>>>> test

public class MainMenu : MonoBehaviour {
	
	public string startLevel;
	public string levelSelect;

	public void NewGame() {
<<<<<<< HEAD
		Application.LoadLevel (startLevel); 	//Call's for the first level.
	}

	public void LevelSelect() {
		Application.LoadLevel (levelSelect);	//Load's selected level.
=======
		SceneManager.LoadScene (startLevel); 	//Call's for the first level.
	}

	public void LevelSelect() {
		SceneManager.LoadScene (levelSelect);	//Load's selected level.
>>>>>>> test
	}

	public void QuitGame() {
		Debug.Log ("Game Exit");
		Application.Quit ();
	}

}
