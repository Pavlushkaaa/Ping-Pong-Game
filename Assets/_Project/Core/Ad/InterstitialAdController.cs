using System;
using UnityEngine;
using GoogleMobileAds.Api;
using System.Collections;

namespace Core
{
    public class InterstitialAdController : MonoBehaviour
    {
        public event Action ShowedAd;
        public event Action ClosedAd;

        public bool CanShowAd { get => _interstitialAd != null && _interstitialAd.CanShowAd(); }

        #if UNITY_EDITOR
        private const string _adUnitId = "ca-app-pub-3940256099942544/1033173712";
        [SerializeField] private bool _skipAd;
        #else
        private const string _adUnitId = "ca-app-pub-1652184728384639/9275411447";
        #endif

        private InterstitialAd _interstitialAd;
        private bool _waitLoadAd = false;

        public void ShowAd()
        {
            #if UNITY_EDITOR
            if (_skipAd) return;
            #endif

            if (CanShowAd)
            {
                _interstitialAd.Show();
                ShowedAd?.Invoke();
            }
            else
            {
                if (!_waitLoadAd)
                    StartCoroutine(TryLoadAd());

                Debug.LogError("Interstitial ad is not ready yet.");
            }
        }

        public void LoadAd()
        {
            if (_interstitialAd != null)
                DestroyAd();

            var adRequest = new AdRequest();

            InterstitialAd.Load(_adUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null)
                {
                    Debug.LogError("Rewarded ad FAILED TO LOAD: " + error);
                    return;
                }

                if (ad == null)
                {
                    Debug.LogError("Unexpected error: Rewarded load event fired with null ad and null error.");
                    return;
                }

                _interstitialAd = ad;

                ad.OnAdFullScreenContentClosed += LoadAd;
                ad.OnAdFullScreenContentClosed += () => ClosedAd?.Invoke();
                ad.OnAdFullScreenContentFailed += (AdError error) => LoadAd();
                ad.OnAdFullScreenContentFailed += (AdError error) => ClosedAd?.Invoke();
            });
        }

        private void DestroyAd()
        {
            if (_interstitialAd != null)
            {
                _interstitialAd.Destroy();
                _interstitialAd = null;
            }
        }

        private IEnumerator TryLoadAd()
        {
            _waitLoadAd = true;
            yield return new WaitForSeconds(10);

            if (!CanShowAd) LoadAd();

            _waitLoadAd = false;
        }
    }
}