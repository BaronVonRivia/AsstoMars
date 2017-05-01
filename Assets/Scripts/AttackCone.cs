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
<<<<<<< HEAD
		if (col.CompareTag ("Player")) {
			turretAI.Attack (false);
		} else if(isLeft){
			turretAI.Attack (true);
		} else {
			turretAI.Attack (true);
		}
=======
		if (col.CompareTag ("Player"))
			turretAI.Attack (false);
		else
			turretAI.Attack (true);
>>>>>>> 6d30b5710e6d15d7dc58a8f50f715f55c13b302a
	}
}