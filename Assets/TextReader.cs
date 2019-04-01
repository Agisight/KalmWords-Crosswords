using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class TextReader : MonoBehaviour {
	public oneCrossWord[] allCW;
	public oneCrossWord oneCW;
	public SetTyvyzyktar setTyv;
	public Text progress, naryny, current;
	public string txt;
	public Transform grid;
	public Inputer inputer;
	public int yzhykter, sany, amgyCrossWord;
	string[] w;
	int gridx, gridy;
	float gridHeight = 0;
	float gridWidth = 0;
	public GameObject fade, success, miss;
	List <Transform> numberSquare;
	public GameObject orig;
	public Transform CrossWords;
	public GameObject Menu;
	UnityEngine.Object[] texts;
	// Use this for initialization

	void Awake () {
		QualitySettings.vSyncCount = 0; // VSync must be disabled
		//Application.targetFrameRate = 26;
	}

	void Start () {		
//		 for (int i = 0; i < allCW.Length; i++) { // clear history
//			PlayerPrefs.SetInt (allCW [i].GetComponent<oneCrossWord> ().sos.name, 0);
//		}
		texts = Resources.LoadAll("tyvyzyktar/");
		allCW = new oneCrossWord[texts.Length];
		int i = 0;
		foreach (UnityEngine.Object one in texts) {
			if (one.name.StartsWith("tyvyzyk_")) {
				oneCrossWord CW = Instantiate (oneCW, CrossWords);
				string tyvyzyk = (one as TextAsset).text;
				int k1 = tyvyzyk.IndexOf ("|");
				CW.name = one.name;
				CW.title = tyvyzyk.Remove (k1); // title
				int k2 = tyvyzyk.IndexOf ("\n");
				CW.naryny = tyvyzyk.Substring (k1+1, k2 - k1-1); 
				string text = tyvyzyk.Substring (k2);

				string sep = "===";
				int j = text.IndexOf (sep);
				CW.tyvyzyk = text.Remove (j).Trim();

				CW.soster = text.Substring (j).Replace(sep,"").Trim();
				allCW [i] = CW;
				CW.id = i;
				CW.CWlevel.text = "Ур. " + CW.naryny;
				CW.CWname.text = CW.title;
				CW.refresher.onClick.AddListener (() => toReset(CW.id));
				CW.GetComponent<Button>().onClick.AddListener (() => {
					Menu.SetActive(false); 
					grid.gameObject.SetActive(true);
				});
				CW.GetComponent<Button>().onClick.AddListener (() => {					
					amgyCrossWord = CW.id;
					Reload(CW.id);
				});

				isSolved (CW);
				i++;
				}
		}
		amgyCrossWord = PlayerPrefs.GetInt("current", 0);
		//Reload (amgyCrossWord);
	}

	public void isSolved (oneCrossWord oneCW) {		
		if (PlayerPrefs.GetInt (oneCW.name, 0)==1)
			oneCW.toSolve ();
	}

	void Init() {
		Load ();
		AllLetters ();
		Sanaar ();
		fade.SetActive (false);
	}

	public void goReset () {
		foreach (Transform c in numberSquare) {
			Letter l = c.GetComponent<Letter> ();
			string truer = allCW[amgyCrossWord].name+"_0_"+l;
			PlayerPrefs.SetInt (truer, 0);
			truer = allCW[amgyCrossWord].name+"_1_"+l;
			PlayerPrefs.SetInt (truer, 0);
		}
		
		PlayerPrefs.SetInt (allCW [amgyCrossWord].GetComponent<oneCrossWord> ().name, 0);
		Reload (amgyCrossWord);
	}

	public void toReset (int id) {
		if (id == amgyCrossWord)
			goReset ();
		else
			PlayerPrefs.SetInt (allCW [id].GetComponent<oneCrossWord> ().name, 0);
	}

	void Load() {
		gridHeight = 0;
		gridWidth = 0;
		w = txt.Split ('\n');
		numberSquare = new List<Transform>();

		int delta = 0;
		string word;
		foreach (string s in w) {
			if (s.Length > 1) {
				int indx = int.Parse (s.Split ('_') [2]);
				int indy = int.Parse (s.Split ('_') [3]);
				delta =  int.Parse(s.Split ('_') [4]);
				word = s.Split ('_') [1];
				if (gridHeight < 40 * (delta * word.Length + indy)) gridHeight = 40 * (delta * word.Length + indy);
				if (gridWidth < 40 * ((1-delta) * word.Length + indx)) gridWidth = 40 * ((1-delta) * word.Length + indx);
				
				Transform clone;
				if (grid.Find (indx + "_" + indy))
					clone = grid.Find (indx + "_" + indy);
				else clone = Instantiate (orig, grid).transform;
				clone.GetComponent<Letter> ().inputLine = inputer;
				numberSquare.Add (clone);
				clone.GetComponent<RectTransform> ().sizeDelta = new Vector2 (40, 40);
				clone.GetComponent<RectTransform> ().localPosition = new Vector2 (indx*40,  -indy*40);
				clone.gameObject.SetActive (true);
				clone.Find ("Placeholder").gameObject.SetActive (true);
				clone.Find ("Text").gameObject.SetActive (true);
				clone.Find("Placeholder").GetComponent<Text>().enabled = true;
				clone.Find("Placeholder").GetComponent<Text>().text = s.Split('_')[0];
				Letter letter = clone.GetComponent<Letter> ();
				clone.name = indx+"_"+indy;
				if (delta == 0) {
					letter.parentX = letter;
					letter.wordCharsX = new Letter[word.Length];
				} else {					
					letter.parentY = letter;
					letter.wordCharsY = new Letter[word.Length];
				}
				Transform mclone;
				for (int i = 0; i < word.Length; i++) {
					string mname = (indx + i) + "_" + indy;
					if (delta == 1)
						mname = indx+"_"+(indy+i);
						
					if (grid.Find (mname))
						mclone = grid.Find (mname);
					else mclone = Instantiate (orig, grid).transform;

					mclone.GetComponent<Letter> ().inputLine = inputer;
					mclone.GetComponent<RectTransform> ().sizeDelta = new Vector2 (40, 40);
					Letter g = mclone.GetComponent<Letter>();
					if (delta == 0) {
						letter.wordCharsX [i] = g;
						g.parentX = letter;
						mclone.GetComponent<RectTransform> ().localPosition = new Vector2 ((indx + i) * 40,  - indy * 40);
						mclone.name = (indx + i)+"_"+indy;
					} else {
						letter.wordCharsY [i] = g;
						g.parentY = letter;
						mclone.GetComponent<RectTransform> ().localPosition = new Vector2 (indx * 40,  - (indy + i) * 40);
						mclone.name = indx+"_"+(indy+i);
					}
					mclone.gameObject.SetActive (true);
					mclone.Find ("Placeholder").gameObject.SetActive (true);
					mclone.Find ("Text").gameObject.SetActive (true);


					g.GetComponent<Button> ().enabled = true;
					g.letter = word.Substring (i, 1);
					g.tag = "sos";
					g.GetComponent<Image> ().color = Color.white;
					g.GetComponent<Image> ().enabled = true;
				}	
				InitAnalyze (clone.GetComponent<Letter> (), delta);
			}

			foreach (Transform t in numberSquare) { // reset parent
				t.SetParent (grid.parent);
				t.SetParent (grid);
			}
		}
	}

	void InitAnalyze (Letter l, int isHorisontal) {		
		string truer = allCW[amgyCrossWord].name+"_"+isHorisontal+"_"+l;
		//if (amgyCrossWord ==0) Debug.Log (PlayerPrefs.GetInt (truer, 0) + " = init Верное слово: " + truer);
		if (PlayerPrefs.GetInt (truer, 0) == 1)
			setAndCorrect (isHorisontal==0 ? l.wordCharsX : l.wordCharsY);
	}

	void setAndCorrect (Letter[] list) {
		string s = "";
		foreach (Letter ll in list) {
			ll.transform.Find ("Text").GetComponent<Text> ().text = ll.letter.ToUpper();
			ll.correct = true;
			s += ll.letter;
		}
	}

	void AllLetters () {
		yzhykter = GameObject.FindGameObjectsWithTag ("sos").Length;
	}

	public void Sanaar () {
		sany = 0;
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("sos"))
			if (g.GetComponent<Letter> ().correct) sany++;
		
		progress.text = sany + "/" + yzhykter;
		CalculateResult ();
	}

	public void Prev () {
		CalculateResult ();
		if (amgyCrossWord == 0) amgyCrossWord = allCW.Length;
		amgyCrossWord = (amgyCrossWord-1) % allCW.Length;
		Reload (amgyCrossWord);
	}

	public void Next () {
		CalculateResult ();
		amgyCrossWord = (amgyCrossWord+1) % allCW.Length;	
		Reload (amgyCrossWord);
	}

	public void CalculateResult () {
		if (sany == yzhykter) {
			PlayerPrefs.SetInt (allCW [amgyCrossWord].GetComponent<oneCrossWord> ().name, 1);
			allCW [amgyCrossWord].GetComponent<oneCrossWord> ().toSolve ();
		} else {
			PlayerPrefs.SetInt (allCW [amgyCrossWord].GetComponent<oneCrossWord> ().name, 0);
		}
	}

	public void Reload (int i) {
		PlayerPrefs.SetInt("current", i);
		txt = allCW [i].soster;
		setTyv.ta = allCW [i].tyvyzyk;
		allCW [i].Starter ();
		naryny.text = allCW [i].naryny;
		current.text = allCW[i].title;

		setTyv.Starter ();
		foreach (Transform s in grid)
			Destroy (s.gameObject);
		fade.SetActive (true);
		Invoke("Init", 0.75f);

		if (((int)Time.time) % 30 > 20f) {
			Camera.main.GetComponent<AliAds> ().goShowInters ();
		}
		Debug.Log (i);
	}

	public void isCorrect () {
		GameObject succ = Instantiate (success, transform.parent);
		succ.GetComponent<Animator> ().PlayInFixedTime("Normal");
		Destroy (succ, succ.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length); 
	}

	public void isMiss () {
		GameObject mis = Instantiate (miss, transform.parent);
		mis.GetComponent<Animator> ().PlayInFixedTime("Normal");
		Destroy (mis, mis.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length); 
	}
}