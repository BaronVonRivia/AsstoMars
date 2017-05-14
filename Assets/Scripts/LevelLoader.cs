using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

	private bool playerInZone;
	public string levelToLoad;

	// Use this for initialization
	void Start () {
		playerInZone = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E) && playerInZone) {	// Load's new level but must press "E"
			SceneManager.LoadScene (levelToLoad);
		}
	}



	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player") {
			playerInZone = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == "Player") {
			playerInZone = false;
		}
	}
}
