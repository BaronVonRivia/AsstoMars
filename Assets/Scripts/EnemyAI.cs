using UnityEngine;
using System.Collections;
using Pathfinding;

[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof (Seeker))]
public class EnemyAI : MonoBehaviour {
	public static GameMaster gm;
	//What to chace?
	public Transform target;

	//How many times each second we will update our path
	public float updateRate = 2f;
	//Caching
	private Seeker seeker;
	private Rigidbody2D rb;

	//The calculated path
	public Path path;

	//The AI's speed per sec
	public float speed = 300f;
	public ForceMode2D fMode;
	[HideInInspector]
	public bool pathIsEnded = false;
	//The max dist from the AI to a waypoiunt for it to continue to the next waypoint
	public float nextWaypointDistance = 3;
	// The waypoint we are currntly moving towards
	private int currentWayPoint = 0;

	private bool searchingForPlayer = false;

	void Start(){
		gm = FindObjectOfType<GameMaster> ();
		//GameObject go = GameObject.FindGameObjectWithTag("Player");
		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody2D> ();

		if (target == null) {
			if(!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer());
			}
			return;
		}
		// Start a new path to the target position, return the result to the OnPathComplete method
		seeker.StartPath (transform.position, target.position, OnPathComplete);

		StartCoroutine (UpdatePath ());
	}

	IEnumerator SearchForPlayer() {
		GameObject sResult = GameObject.FindWithTag ("Player"); {
		if(sResult == null) {
			yield return new WaitForSeconds (0.5f);
			StartCoroutine (SearchForPlayer ());
		} else {
			target = sResult.transform;
			searchingForPlayer = false;
			StartCoroutine (UpdatePath ());
			//return false;
			}
		}
	}


	IEnumerator UpdatePath() {
		if (target == null) {
			if(!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer());
			}
			//return false;
		}

		seeker.StartPath (transform.position, target.position, OnPathComplete);

		yield return new WaitForSeconds (1f / updateRate);
		StartCoroutine (UpdatePath ());
	}

	public void OnPathComplete(Path p) {
		Debug.Log ("We got a path. Did it have an error?" + p.error);
		if (!p.error) {
			path = p;
			currentWayPoint = 0;
		}
	}

	void FixedUpdate () {
		if (target == null) {
			if(!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer());
			}
			return;
		}

		//TODO: Always look at player

		if (path == null)
			return;

		if (currentWayPoint >= path.vectorPath.Count) {
			if(pathIsEnded)
				return;
			Debug.Log ("End of path reached.");
			pathIsEnded = true;
			return;

		}
		pathIsEnded = false;

		//Direction to next waypoint
		Vector3 dir = (path.vectorPath[currentWayPoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;

		//Move the AI
		rb.AddForce(dir, fMode);

		float dist = Vector3.Distance (transform.position, path.vectorPath [currentWayPoint]);
		if (dist < nextWaypointDistance) {
			currentWayPoint++;
			return;
		}

	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			Destroy (other.gameObject);
			gm.StartCoroutine (gm.RespawnPlayer ());

		}
	}

}
