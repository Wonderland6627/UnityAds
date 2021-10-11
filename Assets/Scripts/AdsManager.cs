using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
#if UNITY_IOS
    private string gameID = "4397880";
    private string rewardedPlacementID = "Rewarded_iOS";
    private string bannerPlacementID = "Banner_iOS";
#elif UNITY_ANDROID
    private string gameID = "4397881";
    private string rewardedPlacementID = "Rewarded_Android";
    private string bannerPlacementID = "Banner_Android";
#endif

    [Header("激励广告按钮")]
    public Button rewardedAdsBtn;
    [Header("全屏广告按钮")]
    public Button fullScreenAdsBtn;

    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, true);

        SetAdsBtnsReady();
    }

    private void SetAdsBtnsReady()
    {
        rewardedAdsBtn.onClick.AddListener(ShowRewardedAdsVideo);
        rewardedAdsBtn.interactable = Advertisement.IsReady(rewardedPlacementID);

        fullScreenAdsBtn.onClick.AddListener(ShowFullScreenAdsVideo);

        StartCoroutine(SetBannerAdsReady());
    }

    /// <summary>
    /// 展示激励广告
    /// </summary>
    private void ShowRewardedAdsVideo()
    {
        Advertisement.Show(rewardedPlacementID);
    }

    /// <summary>
    /// 展示全屏广告
    /// </summary>
    private void ShowFullScreenAdsVideo()
    {
        Advertisement.Show();
    }

    /// <summary>
    /// 展示横幅广告
    /// </summary>
    private void ShowBannerAds()
    {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(bannerPlacementID);
    }

    private IEnumerator SetBannerAdsReady()
    {
        var sec = new WaitForSeconds(0.5f);
        while (!Advertisement.IsReady(bannerPlacementID))
        {
            yield return sec;
        }

        ShowBannerAds();
    }

    public void OnUnityAdsReady(string placementId)
    {
        rewardedAdsBtn.interactable = Advertisement.IsReady(rewardedPlacementID);
        Debug.Log("ad ready");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Failed:
                Debug.Log("ad failed");
                break;
            case ShowResult.Skipped:
                Debug.Log("ad skipped");
                break;
            case ShowResult.Finished:
                Debug.Log("ad finish");
                break;
            default:
                break;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {

    }

    public void OnUnityAdsDidError(string message)
    {

    }
}
