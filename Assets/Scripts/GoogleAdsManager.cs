using GoogleMobileAds.Api;
using System.Collections.Generic;
using UnityEngine;
public class GoogleAdsManager : MonoBehaviour
{
    private BannerView bannerView;
    void Start()
    {
        /*
        List<string> test = new List<string>();
        test.Add("DD5E2B8B1222F4DDAE6FDF8F060D3EFD");
        RequestConfiguration configuration = new RequestConfiguration.Builder().SetTestDeviceIds(test).build();

        MobileAds.SetRequestConfiguration(configuration);
        */
        MobileAds.Initialize(initStatus => { RequestBanner(); });
    }

    private void RequestBanner()
    {
#if UNITY_ANDROID
        string appId = "ca-app-pub-3940256099942544/6300978111";
        //string appId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
        string appId = "ca-app-pub-3940256099942544/6300978111";
#else
        string appId = "UNEXPECTED_PLATFORM";
#endif
        AdSize adSize = new AdSize(320, 100);
        bannerView = new BannerView(appId, adSize, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);
    }
}
