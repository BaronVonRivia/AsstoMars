using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public int damage;

	void OnCollisionEnter2D(Collision2D col){
				Player _player = col.collider.GetComponent<Player> ();
				if (_player != null) {
					_player.DamagePlayer (damage);
				}
			Destroy (gameObject);
	}
}
