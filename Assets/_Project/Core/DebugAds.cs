using GoogleMobileAds.Api;
using UnityEngine;

public class DebugAds : MonoBehaviour
{
    public void Start()
    {
        MobileAds.Initialize(initStatus => { });
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
    }
}