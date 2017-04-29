using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public GameObject currentCheckpoint;
	public AudioClip respawnAudio;
	public static GameMaster gm;
	private Player player;				// player class call

	void Start () {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster> ();
		}
	}

	public Transform playerPrefab;
	public Transform spawnPoint;
	public float spawnDelay = 2f;
	public Transform spawnPrefab;

	public IEnumerator _RespawnPlayer () {
		AudioSource.PlayClipAtPoint (respawnAudio, new Vector3 (spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z), 0.5f);
		yield return new WaitForSeconds (spawnDelay);

		Instantiate (playerPrefab, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
		Transform clone = Instantiate (spawnPrefab, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
		Destroy (clone.gameObject, 1f);
		//player.transform.position = currentCheckpoint.transform.position;	// allows checkpoint system
	}

	public static void KillPlayer (Player player) {
		Destroy (player.gameObject);
		gm.StartCoroutine (gm._RespawnPlayer());

	}

	public static void KillSEnemy (SEnemy senemy) {
		gm._KillSEnemy (senemy);
	}	

	public static void KillEnemy (Enemy enemy) {
		gm._KillEnemy (enemy);
	}	

	public void _KillEnemy(Enemy _enemy) {
		Transform _clone = Instantiate (_enemy.deathParticles, _enemy.transform.position, Quaternion.identity);
		Destroy (_clone.gameObject, 5f);
		Destroy (_enemy.gameObject);
	}

	public void _KillSEnemy(SEnemy _senemy) {
		Transform _clone = Instantiate (_senemy.deathParticles, _senemy.transform.position, Quaternion.identity);
		Destroy (_clone.gameObject, 5f);
		Destroy (_senemy.gameObject);
	}
}
