using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void Updater () {
		int i = 0;
		foreach (Transform c in transform) {
			bool b = GetComponent<RectTransform> ().localPosition.y < i * 88 + 200 && GetComponent<RectTransform> ().localPosition.y > (i * 88 - 200) - Camera.main.pixelHeight;
		
				foreach (Transform cc in c)
					cc.gameObject.SetActive (b);
			
				i++;
		}
	}
}
