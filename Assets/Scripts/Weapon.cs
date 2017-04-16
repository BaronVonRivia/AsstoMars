using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
	public float fireRate = 0;
	public int Damage = 10;
	public LayerMask whatToHit;
	public AudioClip gunAudio;

	public Transform BulletTrailPrefab;
	public Transform MuzzleFlashPrefab;
	float timeToSpawnEffect = 0;
	public float effectSpawnRate = 10;
	private float timeToFire = 0;
	Transform firePoint;
	public static GameMaster gm;
	public Transform gunPrefab;

	// Use this for initialization
	void Awake () {
		firePoint = transform.FindChild ("firePoint");
		if (firePoint == null) {
			Debug.LogError ("No firepoint? WHHAAATT?");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (fireRate == 0) {
			if (Input.GetButtonDown ("Fire1")) {
				Shoot ();
				AudioSource.PlayClipAtPoint (gunAudio, new Vector3 (gunPrefab.position.x, gunPrefab.position.y, gunPrefab.position.z), 1f);
			}
		}
		else {
			if(Input.GetButton("Fire1") && Time.time > timeToFire){
				timeToFire = Time.time + 1 / fireRate;
				Shoot ();
				AudioSource.PlayClipAtPoint (gunAudio, new Vector3 (gunPrefab.position.x, gunPrefab.position.y, gunPrefab.position.z), 1f);
			}
		}
	}

	void Shoot(){
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
		if (Time.time >= timeToSpawnEffect) {
			Effect ();
			timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
		}
		Debug.DrawLine (firePointPosition, (mousePosition-firePointPosition)*100, Color.cyan);
		if (hit.collider != null) {
			Debug.DrawLine (firePointPosition, hit.point, Color.red);
			Debug.Log ("We hit " + hit.collider.name + " and did " + Damage + " damage.");
			SEnemy senemy = hit.collider.GetComponent<SEnemy>();
			Enemy enemy = hit.collider.GetComponent<Enemy>();
			if (senemy != null) {
				senemy.DamageSEnemy(Damage);
				Debug.Log ("We hit " + hit.collider.name + " and did " + Damage + " damage.");
			} else if (enemy != null) {
				enemy.DamageEnemy(Damage);
				Debug.Log ("We hit " + hit.collider.name + " and did " + Damage + " damage.");
			}
		}
	}

	void Effect () {
		Instantiate (BulletTrailPrefab, firePoint.position, firePoint.rotation);
		Transform clone = Instantiate (MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
		clone.parent = firePoint;
		float size = Random.Range (0.6f, 0.9f);
		clone.localScale = new Vector3 (size, size, size);
		Destroy (clone.gameObject, 0.02f);
	}

}