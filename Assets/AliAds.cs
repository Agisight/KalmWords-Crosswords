using System;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AliAds : MonoBehaviour {
	private BannerView bannerView;
	private InterstitialAd interstitial;
	void Start () {
		
		//goShowBanner ();
		//InvokeRepeating ("goShowBanner", 1f, 10f);
		RequestInterstitial ();
		InvokeRepeating ("goShowInters", 10f, 180f);
	}

	void goShowBanner () {
		RequestBanner ();
		if (bannerView != null ) bannerView.Show ();
	}

	public void goShowInters () {	
		//GameObject.Find ("test").GetComponent<Text> ().text = "Inters isLoaded? " + interstitial.IsLoaded() + " | " + Time.time;	
		if (interstitial.IsLoaded()) {
			interstitial.Show();
		//	GameObject.Find ("test").GetComponent<Text> ().text = "Inters Show " + Time.time;
		}

		RequestInterstitial ();
	}

	private void RequestBanner() {
		string s = "ca-app-pub-2222222/3333333";
		#if UNITY_EDITOR
		string adUnitId = s;
		#elif UNITY_ANDROID
		string adUnitId = s;
		#elif UNITY_IPHONE
		string adUnitId = s;
		#else
		string adUnitId = s;
		#endif

		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
		// Create an empty ad request.
		AdRequest request = createAdRequest ();
		// Load the banner with the request.
		bannerView.LoadAd(request);
		}

		//Returns an ad request with custom ad targeting.
		private AdRequest createAdRequest()
		{
		return new AdRequest.Builder()
		.AddTestDevice(AdRequest.TestDeviceSimulator)
		.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
		.AddKeyword("game")
		.SetGender(Gender.Male)
		.SetBirthday(new DateTime(1985, 1, 1))
		.TagForChildDirectedTreatment(false)
		.AddExtra("color_bg", "9B30FF")
		.Build();

		}

		private void RequestInterstitial()
		{
		string s = "ca-app-pub-0000000000/11111111";
		#if UNITY_ANDROID
		string adUnitId = s;
		#elif UNITY_IPHONE
		string adUnitId = s;
		#else
		string adUnitId = s;
		#endif

		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd(adUnitId);
		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		interstitial.LoadAd(request);
		}

		private void GameOver()
		{
			if (interstitial.IsLoaded()) {
				interstitial.Show();
			}
		}
}
