using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
<<<<<<< HEAD
	public int damage;

	void OnCollisionEnter2D(Collision2D col){
				Player _player = col.collider.GetComponent<Player> ();
				if (_player != null) {
					_player.DamagePlayer (damage);
				}
			Destroy (gameObject);
=======

	void OnTriggerEnter2D(Collider2D col){
		if (col.isTrigger != true) {
			Destroy (gameObject);
		}
>>>>>>> 6d30b5710e6d15d7dc58a8f50f715f55c13b302a
	}
}
