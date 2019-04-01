using UnityEngine;
using System.Collections;

public class OpenUrl : MonoBehaviour {
	public string AndroidString;
	public string iOSString;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public void goOpen () {
		#if UNITY_IOS
		Application.OpenURL(iOSString);
		#endif
		#if !UNITY_IOS
		Application.OpenURL(AndroidString);
		#endif
	}
}
