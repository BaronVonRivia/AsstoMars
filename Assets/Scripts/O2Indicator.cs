using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class O2Indicator : MonoBehaviour {
	[SerializeField]
	private RectTransform O2BarRect;
	[SerializeField]
	private Text O2Text;

	void Start() {
		if(O2BarRect == null) {
			Debug.LogError ("Status Indicator: No O2 bar object referenced!");
		}

		if(O2Text == null) {
			Debug.LogError ("Status Indicator: No O2 text object referenced!");
		}
	}

	public void SetO2(int _cur, int _max) {
		float _value = (float)_cur / _max;

		O2BarRect.localScale = new Vector3 (_value, O2BarRect.localScale.y, O2BarRect.localScale.z);
		O2Text.text = _cur + "/" + _max + " O2";
	}
}
