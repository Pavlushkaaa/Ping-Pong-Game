using System;
using UnityEngine;
using GoogleMobileAds.Api;
using System.Collections;

namespace Core
{
    public class RewardedAdController : MonoBehaviour
    {
        public event Action ShowedAd;
        public event Action ClosedAd;

        #if UNITY_EDITOR
        private const string _adUnitId = "ca-app-pub-3940256099942544/5224354917";
        [SerializeField] private bool _skipAd;
        #else
        private const string _adUnitId = "ca-app-pub-1652184728384639/2457107251";
        #endif

        private bool _canShowAd => _rewardedAd != null && _rewardedAd.CanShowAd();

        private RewardedAd _rewardedAd;
        private bool _waitLoadAd = false;

        public void ShowAd(Action playerReward)
        {
            #if UNITY_EDITOR
            if (_skipAd)
            {
                playerReward?.Invoke();
                return;
            }
            #endif

            if (_canShowAd)
            {
                _rewardedAd.Show((Reward reward) => playerReward?.Invoke());
                ShowedAd?.Invoke();
            }
            else
            {
                if (!_waitLoadAd)
                    StartCoroutine(TryLoadAd());

                Debug.LogError("Rewarded ad is not ready yet.");
            }
        }

        public void LoadAd()
        {
            if (_rewardedAd != null)
                DestroyAd();

            var adRequest = new AdRequest();

            RewardedAd.Load(_adUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
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

                _rewardedAd = ad;

                ad.OnAdFullScreenContentClosed += LoadAd;
                ad.OnAdFullScreenContentClosed += () => ClosedAd?.Invoke();
                ad.OnAdFullScreenContentFailed += (AdError error) => LoadAd();
                ad.OnAdFullScreenContentFailed += (AdError error) => ClosedAd?.Invoke();
            });
        }

        private void DestroyAd()
        {
            if (_rewardedAd != null)
            {
                _rewardedAd.Destroy();
                _rewardedAd = null;
            }
        }

        private IEnumerator TryLoadAd()
        {
            _waitLoadAd = true;
            yield return new WaitForSeconds(10);

            if (!_canShowAd) LoadAd();

            _waitLoadAd = false;
        }
    }
}