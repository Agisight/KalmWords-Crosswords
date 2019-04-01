using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class oneCrossWord : MonoBehaviour {
	public int id;
	public Text CWlevel, CWname;
	public Button refresher;
	public string soster;
	public string tyvyzyk;
	public string naryny;
	public string title;
	public Sprite ok, no;
	// Use this for initialization
	public void Starter () {
		//title = tyvyzyk.Split ('\n')[0].Trim();
	}

	public void toSolve () {
		transform.Find ("isSolved").GetComponent<Image>().color = Color.white;
	}

	public void toRefresh () {
		transform.Find ("isSolved").GetComponent<Image>().color = Color.clear;
	}
}
