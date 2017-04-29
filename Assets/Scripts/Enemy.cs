using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[System.Serializable]
	public class EnemyStats {
		public int Health = 100;
	}

	public EnemyStats stats = new EnemyStats();
	public Transform deathParticles;

	void Start() {
		if(deathParticles == null) {
			Debug.LogError ("No death particles reffernce on enemy!");
		}
	}

	public int damage = 40;

	public void DamageEnemy (int damage) {
		stats.Health -= damage;
		if (stats.Health <= 0) {
			GameMaster.KillEnemy (this);
		}
	}

	void OnCollisionEnter2D(Collision2D _colInfo) {
		Player _player = _colInfo.collider.GetComponent<Player> ();
		if(_player != null) {
			_player.DamagePlayer (damage);
			DamageEnemy (999999);
		}
	}
}

