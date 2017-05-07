using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[System.Serializable]
	public class PlayerStats {
		public int maxHealth = 100;

		private int _curHealth;
		public int curHealth
		{
			get { return _curHealth; }
			set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
		}

		public void Init() {
			curHealth = maxHealth;
		}
	}

	private LifeManager lifesystem;

	public PlayerStats stats = new PlayerStats ();
	public Transform deathParticles;
	public int fallBoundary = -20;

	[SerializeField]
	private StatusIndicator statusIndicator; 	

	void Start() {

		lifesystem = FindObjectOfType<LifeManager> ();

		stats.Init ();

		if(statusIndicator == null) {
			Debug.LogError ("No status indicator referenced on Player");
		} 
		else {
			statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
		}
	}

	void Update() {
		if (transform.position.y <= fallBoundary)
			DamagePlayer (999999);

	}

	public void DamagePlayer (int damage) {
		stats.curHealth -= damage;
		if (stats.curHealth <= 0) {
			GameMaster.KillPlayer (this);
			//lifesystem.TakeLife ();
		}

		statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
	}
}
