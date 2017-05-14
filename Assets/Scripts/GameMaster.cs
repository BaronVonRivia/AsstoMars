using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public GameObject currentCheckpoint;
	public AudioClip respawnAudio;
	public static GameMaster gm;
	private static int _remainingLives = 2;
	public static int RemainingLives
	{
		get{return _remainingLives;}
	}

	void Start () {
		_remainingLives = 2;
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster> ();
		}
	}

	public Transform playerPrefab;
	public Transform spawnPoint;
	public float spawnDelay = 2f;
	public Transform spawnPrefab;

	[SerializeField]
	private GameObject gameOverUI;

	public void EndGame() 
	{
		Debug.Log ("game over");
		gameOverUI.SetActive (true);
	}

	public IEnumerator _RespawnPlayer () {
		yield return new WaitForSeconds (spawnDelay);
		AudioSource.PlayClipAtPoint (respawnAudio, new Vector3 (spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z), 0.5f);
		Instantiate (playerPrefab, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
		Transform clone = Instantiate (spawnPrefab, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
		Destroy (clone.gameObject, 1f);
	}

	public static void KillPlayer (Player player) {
		Transform _clone = Instantiate (player.deathParticles, player.transform.position, Quaternion.identity);
		Destroy (_clone.gameObject, 5f);
		Destroy (player.gameObject);
		_remainingLives--;

		if(_remainingLives <= 0)
		{
			gm.EndGame ();
		} else
		{
			gm.StartCoroutine (gm._RespawnPlayer());
		}
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
