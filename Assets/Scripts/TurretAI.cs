using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour {
	public static GameMaster gm;
	public float distance;
	public float wakeRange;
	public float shootInterval;
	public float bulletSpeed = 100;
	public float bulletTimer;
	public float updateRate = 2f;

	private bool searchingForPlayer = false;
	public bool awake = false;
	public bool lookingRight = true;

	public GameObject bullet; 
	public Transform target;
	public Animator anim;
	public Transform shootPointLeft, shootPointRight;

	void Awake(){
		anim = gameObject.GetComponent<Animator> ();
	}

	void Start(){
		gm = FindObjectOfType<GameMaster> ();
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		target = go.transform;
		if (target == null) {
			if(!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer());
			}
			return;
		}
	}

	void Update(){
		if (target == null) {
			if(!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer());
			}
			return;
		}

		anim.SetBool ("Idle", awake);
		anim.SetBool ("LookingRight", lookingRight);

		RangeCheck ();


		if (target.transform.position.x < transform.position.x) {
			lookingRight = false;
		} else {
			lookingRight = true;
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



	void RangeCheck(){
		distance = Vector2.Distance (transform.position, target.transform.position);

		if (distance < wakeRange) {
			awake = true;
		}

		if (distance > wakeRange) {
			awake = false;
		}
	}

	public void Attack(bool attackingRight){
		bulletTimer += Time.deltaTime;

		if (bulletTimer >= shootInterval) {
			Vector2 direction = target.transform.position - transform.position;
			direction.Normalize ();

			if (!attackingRight) {
				GameObject bulletClone;
				bulletClone = Instantiate(bullet, shootPointLeft.transform.position, shootPointLeft.transform.rotation) as GameObject;
				bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

				bulletTimer = 0;
			}
			else {
				GameObject bulletClone;
				bulletClone = Instantiate(bullet, shootPointRight.transform.position, shootPointRight.transform.rotation) as GameObject;
				bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

				bulletTimer = 0;
			}
		}
	}
}


