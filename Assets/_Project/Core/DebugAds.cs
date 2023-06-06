using GoogleMobileAds.Api;
using UnityEngine;

public class DebugAds : MonoBehaviour
{
    public void Start()
    {
        #if UNITY_EDITOR
        return;
        #endif

        MobileAds.Initialize(initStatus => { });
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
    }
}