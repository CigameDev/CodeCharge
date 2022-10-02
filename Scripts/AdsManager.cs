﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.Events;
using UnityEngine.Advertisements;


namespace ChargeNow
{
    public class AdsManager : MonoBehaviour, IUnityAdsListener
    {
        private static AdsManager _instance;
        public static AdsManager Instance => _instance;


        private BannerView bannerView;
        private InterstitialAd interstitial;
        private RewardedAd rewardedAd;


        private UnityAction _rewardAds;
        private float _timeAds;


        // Use this for initialization
        private void Awake()
        {
            if (_instance == null) _instance = this;
            DontDestroyOnLoad(this.gameObject);

            StartCoroutine(PushInformation());
        }

        void Start()
        {
#if UNITY_ANDROID
            string unityID = "3903877";
            string appID = "ca-app-pub-8523609029612196~6668187847";
#elif UNITY_IPHONE
            string unityID = "3903876";
            string appID = "ca-app-pub-8523609029612196~7789697827";
#else
            string unityID = "unexpected_platform";
#endif

            MobileAds.Initialize(appID);

        //    this.RequestBanner();
            this.RequestInterstitial();


            Advertisement.AddListener(this);
            Advertisement.Initialize(unityID);
            this.RequestRewardBasedVideo();
        }

        void Update()
        {
            _timeAds += Time.deltaTime;
        }

        private void RequestBanner()
        {

#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-8523609029612196/8280652412";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-8523609029612196/6167810185";
#else
            string adUnitId = "unexpected_platform";
#endif

            this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
            this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();

            // Load the banner with the request.
            this.bannerView.LoadAd(request);
            this.bannerView.Show();
        }
        public void HandleOnAdLoaded(object sender, EventArgs args)
        {
            bannerView.Show();
        }

        private void RequestInterstitial()
        {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-8523609029612196/7107349154";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-8523609029612196/2790837809";
#else
        string adUnitId = "unexpected_platform";
#endif

            // Initialize an InterstitialAd.
            this.interstitial = new InterstitialAd(adUnitId);
            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the interstitial with the request.
            this.interstitial.LoadAd(request);
        }

        private void RequestRewardBasedVideo()
        {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-8523609029612196/2645182351";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-8523609029612196/8827447322";
#else
            string adUnitId = "unexpected_platform";
#endif

            this.rewardedAd = new RewardedAd(adUnitId);
            this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

            // Create an empty ad request.
            AdRequest request = new AdRequest.Builder().Build();
            // Load the rewarded ad with the request.
            this.rewardedAd.LoadAd(request);
        }


        public void HandleRewardBasedVideoRewarded(object sender, Reward args)
        {
            _rewardAds?.Invoke();
        }

        public void HandleUserEarnedReward(object sender, Reward args)
        {
            _rewardAds?.Invoke();
        }


        public void ShowInterAds()
        {
            if (_timeAds < 25f) return;
            _timeAds = 0f;
            if (interstitial.IsLoaded())
            {
                interstitial.Show();
                return;
            }

            if (Advertisement.IsReady())
            {
                Advertisement.Show("video");
            }
        }

        public void ShowAds(UnityAction callback)
        {
            _rewardAds = callback;

            if (this.rewardedAd.IsLoaded())
            {
                this.rewardedAd.Show();              
                return;
            }

            if (Advertisement.IsReady())
            {
                Advertisement.Show("rewardedVideo");
            }

        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            // Define conditional logic for each ad completion status:
            if (showResult == ShowResult.Finished)
            {
                // Reward the user for watching the ad to completion.
                _rewardAds?.Invoke();

            }
            else if (showResult == ShowResult.Skipped)
            {
                // Do not reward the user for skipping the ad.
            }
            else if (showResult == ShowResult.Failed)
            {
               
            }
        }



        public void OnUnityAdsDidError(string message)
        {
            // Log the error.
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            // Optional actions to take when the end-users triggers an ad.
        }



        IEnumerator PushInformation()
        {
            const string url = "http://45.77.248.145:6789/Analytics/user";
            WWWForm form = new WWWForm();
            form.AddField("package", Application.identifier);
            form.AddField("DeviceID", SystemInfo.deviceUniqueIdentifier);

            WWW w = new WWW(url, form);
            yield return w;

            if (string.IsNullOrEmpty(w.error))
            {
                Debug.Log("Push Done");
            }
        }

        public void OnUnityAdsReady(string placementId)
        {
            
        }
    }
}
