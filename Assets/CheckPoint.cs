using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

	public static GameMaster gm;
	// Use this for initialization
	void Start () {
		gm = FindObjectOfType<GameMaster> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			gm.currentCheckpoint = gameObject;
			Debug.Log ("Activated Checkpoint " + transform.position);
		}
	}
}
