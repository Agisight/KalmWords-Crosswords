using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Inputer : MonoBehaviour {
	public TextReader TR;
	public Letter[] wordChars;
	public static bool isHorizontal;

	// Update is called once per frame
	public void goClose () {
		foreach (Letter img in wordChars)
			img.GetComponent<Image> ().color = Color.white;

		if (GetComponent<InputField> ().text.Length == wordChars.Length) {
			int h = 0;
			for (int i = 0; i < GetComponent<InputField> ().text.Length; i++) {
				if (wordChars [i].letter.ToUpper() == GetComponent<InputField> ().text.Substring (i, 1).ToUpper())
					h++;
			}
				
			if (h == wordChars.Length) { //слово верно и надо его закрепить
				string truer = TR.allCW[TR.amgyCrossWord].name;
				if (isHorizontal)
					truer += "_0_" + wordChars [0].parentX;
				else truer += "_1_" + wordChars [0].parentY;

				//Debug.Log (isHorizontal + ". Верное слово: " + truer + " | Текущий кроссворд: " + TR.amgyCrossWord);
				PlayerPrefs.SetInt (truer, 1);
				foreach (Letter l in wordChars)
					l.correct = true;

				TR.isCorrect ();
				TR.Sanaar ();
				GetComponent<InputField> ().text = "";
				return;
			} else TR.isMiss ();
		}

		for (int i = 0; i < wordChars.Length; i++)
			if (!wordChars [i].correct) wordChars [i].transform.GetChild (1).GetComponent<Text> ().text = "";
		
		GetComponent<InputField> ().text = "";
	}
		
	public void goEdit () {
		for (int i = 0; i < GetComponent<InputField> ().text.Length; i++)
			if (!wordChars [i].correct)
				wordChars [i].transform.GetChild (1).GetComponent<Text> ().text = GetComponent<InputField> ().text.Substring (i, 1).ToUpper ();

		for (int i = GetComponent<InputField> ().text.Length; i < wordChars.Length; i++)
			if (!wordChars [i].correct) wordChars [i].transform.GetChild(1).GetComponent<Text> ().text = "";
	}
}