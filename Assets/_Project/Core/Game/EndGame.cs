﻿using System;
using System.Collections;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(SoundPlayer))]
    public class EndGame : MonoBehaviour
    {
        public event Action OnEndFail;
        public event Action OnEndSuccess;

        [SerializeField] private AdController _adController;
        [SerializeField] private EndGameView _view;

        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private GameLoop _gameLoop;
        [SerializeField] private LevelManager _levelsManager;

        [Space]
        [SerializeField] private AudioClip _doneClip;
        [SerializeField] private AudioClip _failClip;

        private SoundPlayer _soundPlayer;

        private int _failEndNumber;
        private bool _canShowInterstitialAd;
        private bool _canFreeContinue;
        private Coroutine _waitCoroutine;

        private const int _minFailsNumberToShowAd = 4;
        private const int _pauseBetweenAdsAtSeconds = 60;

        public void ForceEndFail()
        {
            _gameLoop.EndLoop();
            _mainMenu.Show();
            OnEndFail?.Invoke();
        }

        public void EndFail()
        {
            ShowInterstitialAd();

            _soundPlayer.Play(_failClip);

            _gameLoop.StopLoop();
            _view.ShowFailPanel();
            OnEndFail?.Invoke();
        }

        public void EndSuccess()
        {
            _levelsManager.SetLevelComplete();
            _soundPlayer.Play(_doneClip);

            _gameLoop.StopLoop();

            _view.ShowDonePanel();
            OnEndSuccess?.Invoke();
        }

        private void WatchAd()
        {
            if(_canFreeContinue)
            {
                ContinueGame();
                ResetWathAdButton();
                return;
            }

            _adController.ShowRewardAd(ContinueGame);
        }

        private void ResetWathAdButton()
        {
            if (_adController.DisableAds) return;

            _canFreeContinue = false;
            _view.SetWatchAdSprite();
        }

        private void ContinueGame()
        {
            _failEndNumber = 0;

            if (_waitCoroutine != null) StopCoroutine(_waitCoroutine);
            _canShowInterstitialAd = true;
            _waitCoroutine = StartCoroutine(StartTimer());

            _gameLoop.ContinueLoop();
            _view.Hide();
        }

        private void ShowInterstitialAd()
        {
            _failEndNumber++;

            if (!_adController.CanShowInterstitialAd) return;

            if (_failEndNumber >= _minFailsNumberToShowAd && _canShowInterstitialAd)
            {
                _failEndNumber = 0;
                _canFreeContinue = true;
                _view.SetFreeContinueSprite();

                _adController.ShowInterstitialAd();

                _waitCoroutine = StartCoroutine(StartTimer());
            }
        }

        private void Start()
        {
            _view.WatchedAd += WatchAd;

            _view.PlayedAgain += _gameLoop.Restart;
            _view.PlayedNext += _gameLoop.StartLoop;
            _view.ReturnedToMainMenu += ForceEndFail;

            _view.PlayedAgain += ResetWathAdButton;
            _view.PlayedNext += ResetWathAdButton;
            _view.ReturnedToMainMenu += ResetWathAdButton;

            _soundPlayer = GetComponent<SoundPlayer>();


            if (_adController.DisableAds)
            {
                _canShowInterstitialAd = false;
                _canFreeContinue = false;
                _view.SetFreeContinueSprite();
            }
            else
            {
                _canShowInterstitialAd = true;
                StartCoroutine(StartTimer());
            }
        }

        private IEnumerator StartTimer()
        {
            if (!_canShowInterstitialAd) yield break;

            _canShowInterstitialAd = false;

            yield return new WaitForSecondsRealtime(_pauseBetweenAdsAtSeconds);

            _canShowInterstitialAd = true;
        }
    }
}
