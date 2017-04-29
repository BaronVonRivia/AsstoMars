using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidTrip : MonoBehaviour {
	public static GameMaster gm;
	int damage = 999999;
	// Use this for initialization
	void Start () {
		gm = FindObjectOfType<GameMaster> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			Destroy (other.gameObject);
			gm.StartCoroutine (gm._RespawnPlayer ());

		}
	}
}
