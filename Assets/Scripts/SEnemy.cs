using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEnemy : MonoBehaviour {
	public static GameMaster gm;
	public Transform target;
	public float moveSpeed;
	public float rotationSpeed;
	private bool searchingForPlayer = false;
	public float updateRate = 2f;

	[System.Serializable]
	public class SEnemyStats {
		public int Health = 40;
	}

	public SEnemyStats senemyStats = new SEnemyStats ();

	public Transform deathParticles;


	public void DamageSEnemy (int damage) {
		senemyStats.Health -= damage;
		if (senemyStats.Health <= 0) {
			GameMaster.KillSEnemy (this);
		}
	}

	IEnumerator SearchForPlayer() {
		GameObject sResult = GameObject.FindWithTag ("Player"); {
			if(sResult == null) {
				yield return new WaitForSeconds (1f);
				StartCoroutine (SearchForPlayer ());
			} else {
				target = sResult.transform;
				searchingForPlayer = false;
				StartCoroutine (UpdatePath ());
			}
		}
	}

	IEnumerator UpdatePath() {
		if (target == null) {
			if(!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer());
			}
		}

		yield return new WaitForSeconds (0.5f / updateRate);
		StartCoroutine (UpdatePath ());
	}

	private Transform myTransform;
	void Awake() {
		myTransform = transform;
	}
	
	void Start () {
		gm = FindObjectOfType<GameMaster> ();
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
		if(deathParticles == null) {
			Debug.LogError ("No death particles reffernce on enemy!");
		}
		if (target == null) {
			if(!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer());
			}
			return;
		}

	}


	// Update is called once per frame
	void Update () {
		if (target == null) {
			if(!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer());
			}
			return;
		}
		Vector3 dir = target.position - myTransform.position;
		dir.z = 0.0f; 
		if (dir != Vector3.zero) {
			myTransform.rotation = Quaternion.Slerp (myTransform.rotation, 
				Quaternion.FromToRotation(Vector3.right, dir), rotationSpeed * Time.deltaTime);
		}

		//Move Towards Target
		myTransform.position += (target.position - myTransform.position).normalized * moveSpeed * Time.deltaTime;

	}
	void OnTriggerEnter2D (Collider2D other) {
			if (other.tag == "Player") {
				Destroy (other.gameObject);
				gm.StartCoroutine (gm._RespawnPlayer ());

		}
	}
}