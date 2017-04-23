using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public AudioClip respawnAudio;
	public static GameMaster gm;

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

		Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
		Transform clone = Instantiate (spawnPrefab, spawnPoint.position, spawnPoint.rotation);
		Destroy (clone.gameObject, 1f);
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
		Instantiate (_enemy.deathParticles, _enemy.transform.position, Quaternion.identity);
		Destroy (_enemy.gameObject);
	}

	public void _KillSEnemy(SEnemy _senemy) {
		Transform _clone = Instantiate (_senemy.deathParticles, _senemy.transform.position, Quaternion.identity);
		Destroy (_clone.gameObject, 5f);
		Destroy (_senemy.gameObject);
	}
}
