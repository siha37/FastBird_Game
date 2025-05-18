using System;
using GoogleMobileAds.Api;
using MyFolder._01.Script._1000.GPGS;
using UnityEngine;

public class GoogleRewardADManager : MonoBehaviour
{
    [Header("Cache")]
    private readonly bool enableTestAD = true; //테스트 광고 활성화 여부
    private readonly string rewardADID = ""; //보상형 광고 ID
    private readonly string rewardTestADID = "ca-app-pub-3940256099942544/5354046379"; //보상형 테스트 광고 ID
    
    private RewardedInterstitialAd rewardedInterstitialAd;

    public Action onUserEarned;
    
    
    void Start()
    {
        LoadRewardedInterstitialAd();
    }
    
    public void LoadRewardedInterstitialAd()
    {
        if (rewardedInterstitialAd != null)
        {
            rewardedInterstitialAd.Destroy();
            rewardedInterstitialAd = null;
        }
        
        Debug.Log("LoadRewardedInterstitialAd");

        var adRequest = new AdRequest();
        adRequest.Keywords.Add("Retry_Admob");
        
        RewardedInterstitialAd.Load(rewardTestADID, adRequest,
            (RewardedInterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("rewarded interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedInterstitialAd = ad;
                RegisterEventHandlers(ad);
                RegisterReloadHandlers(ad);
            }
        );
    }

    public void ShowRewardedInterstitialAd()
    {
        const string rewardMsg = "Rewarded interstitial ad loaded successfully";
        if (rewardedInterstitialAd != null && rewardedInterstitialAd.CanShowAd())
        {
            rewardedInterstitialAd.Show((Reward reward) =>
            { 
                //TODO: Reward the user.
                UnityMainThreadDispatcher.Instance().Enqueue(onUserEarned);
                onUserEarned = null;
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }
    }
    
    private void RegisterEventHandlers(RewardedInterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded interstitial ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded interstitial ad failed to open " +
                           "full screen content with error : " + error);
        };
    }

    private void RegisterReloadHandlers(RewardedInterstitialAd ad)
    {
        ad.OnAdFullScreenContentClosed+= () =>
        {
            Debug.Log("Rewarded interstitial ad full screen content closed.");
            LoadRewardedInterstitialAd();
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded interstitial ad failed to open " +
                           "full screen content with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedInterstitialAd();
        };
    }
    
    public void OnUserPressedRevive(Action action)
    {
        if (rewardedInterstitialAd != null && rewardedInterstitialAd.CanShowAd())
        {
            onUserEarned = action;
            ShowRewardedInterstitialAd();
        }
        else
        {
            Debug.Log("광고가 아직 로드되지 않음");
            LoadRewardedInterstitialAd(); // 재로드
        }
    }
        
}
