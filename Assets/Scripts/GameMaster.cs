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

	public IEnumerator RespawnPlayer () {
		AudioSource.PlayClipAtPoint (respawnAudio, new Vector3 (spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z), 0.5f);
		yield return new WaitForSeconds (spawnDelay);

		Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
		Transform clone = Instantiate (spawnPrefab, spawnPoint.position, spawnPoint.rotation);
		Destroy (clone.gameObject, 3f);
	}
		

	public static void KillPlayer (Player player) {
		Destroy (player.gameObject);
		gm.StartCoroutine (gm.RespawnPlayer());

	}

	public static void KillEnemy (SEnemy enemy) {
		Destroy (enemy.gameObject);
	}	

	public static void KillEnemy (Enemy enemy) {
		Destroy (enemy.gameObject);
	}	

}
