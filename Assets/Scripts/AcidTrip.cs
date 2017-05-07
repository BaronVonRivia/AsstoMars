using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidTrip : MonoBehaviour {
	public static GameMaster gm;
	public int damage = 999999;
	// Use this for initialization
	void Start () {
		gm = FindObjectOfType<GameMaster> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D _colInfo) {
		Player _player = _colInfo.collider.GetComponent<Player> ();
		if(_player != null) {
			_player.DamagePlayer (damage);
		}
	}
}
