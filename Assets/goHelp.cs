using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class goHelp : MonoBehaviour {
	public GameObject helpPanel;
	// Use this for initialization
	void Start () {
		helpPanel.SetActive (false);
	}
	
	// Update is called once per frame
	public void open () {
		helpPanel.SetActive (true);
	}

	public void close () {
		helpPanel.SetActive (false);
	}
}
