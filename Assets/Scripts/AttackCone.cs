using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCone : MonoBehaviour {
	public TurretAI turretAI;

	public bool isLeft = false;


	void Awake(){
		turretAI = gameObject.GetComponentInParent<TurretAI> ();
	}

	void OnTriggerStay2D(Collider2D col){
		if (col.CompareTag ("Player")) {
			turretAI.Attack (false);
		} else if(isLeft){
			turretAI.Attack (true);
		} else {
			turretAI.Attack (true);
		}
	}
}