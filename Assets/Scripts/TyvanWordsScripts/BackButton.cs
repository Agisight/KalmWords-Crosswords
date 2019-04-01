using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackButton : MonoBehaviour {

	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}
	}
}
