using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O2 : MonoBehaviour {

	public GameObject target;
	[System.Serializable]
	public class O2Timer {

		public float maxTime = 300f;
		private float _curTimer;
		public float curTimer
		{
			get { return _curTimer; }
			set { _curTimer = Mathf.Clamp(value, 0f, maxTime); }
		}

		public void Init() {
			curTimer = maxTime;
		}
	}
	public static GameMaster gm;
	public static Player player;
	public O2Timer timer = new O2Timer ();

	[SerializeField]
	private O2Indicator o2Indicator;

	void Start () {
		timer.Init ();
		gm = FindObjectOfType<GameMaster> ();
	}

	void Update () {
		timer.curTimer -= Time.deltaTime;
		Debug.Log (timer.curTimer);
		if(timer.curTimer <= 0) {
			Destroy (player.gameObject);
			gm.StartCoroutine (gm._RespawnPlayer());
			resetTimer (timer.curTimer);
		}
	}
	void resetTimer(float curTime) {
		timer.curTimer = timer.maxTime;
	}
}
