using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetTyvyzyktar : MonoBehaviour {
	public string ta;
	// Use this for initialization
	public void Starter () {
		string sep = "---";
		int i = ta.IndexOf (sep);

		string g = ta.Remove (i).Trim();
		string s = ta.Substring (i).Replace(sep,"").Trim();
		transform.GetChild(0).GetComponent<Text> ().text = g;
		transform.GetChild(1).GetComponent<Text> ().text = s;
	}
}
