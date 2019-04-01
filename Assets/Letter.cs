using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Letter : MonoBehaviour {
	public Letter parentX, parentY;
	public Letter[] wordCharsX;
	public Letter[] wordCharsY;
	public string letter;
	public Inputer inputLine;
	public bool correct=false;
	bool direction;
	Text t;
	// Use this for initialization
	void Start () {
		direction = true;
		t = transform.GetChild (0).GetComponent<Text> ();
	}

	void OnMouseUp () {
		if (GetComponent<Image> ().enabled) {
			t.enabled = true;

			Invoke ("Delayed", 0.03f);
		}
	}

	void Delayed () {
		bool b = whichDirection ();
		if (b) 
			inputLine.wordChars = parentX.wordCharsX;
		else {
			if (parentY != null)
				inputLine.wordChars = parentY.wordCharsY;
			else inputLine.wordChars = parentX.wordCharsX;
		}

		Inputer.isHorizontal = b;
		EnableChars ();
	}

	bool whichDirection () {
		if (parentX!=null && parentY!=null) {
			direction = !direction;
			return direction;
		}

		if (parentX != null && parentY == null)
			return true;
		else
			return false;		
	}

	void EnableChars () {
		inputLine.GetComponent<InputField> ().characterLimit = inputLine.wordChars.Length;
		foreach (Letter t in inputLine.wordChars) {
				t.GetComponent<Image> ().enabled = true;
			t.GetComponent<Image> ().color = Color.yellow;// new Color(1f, 0.3f, 0.6f);
			}

		inputLine.gameObject.SetActive (true);
		inputLine.GetComponent<InputField> ().Select ();
		//TouchScreenKeyboard.hideInput = true;
		//inputLine.GetComponent<InputField> ().shouldHideMobileInput = true;
	}

}
