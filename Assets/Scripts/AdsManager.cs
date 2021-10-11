using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
#if UNITY_IOS
    private string gameID = "4397880";
    private string myPlacementID = "Rewarded_iOS";
#elif UNITY_ANDROID
    private string gameID = "4397881";
    private string rewardedPlacementID = "Rewarded_Android";
#endif

    public Button rewardedBtn;

    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, true);

        rewardedBtn.onClick.AddListener(ShowRewardedVideo);
        rewardedBtn.interactable = Advertisement.IsReady(rewardedPlacementID);
    }

    /// <summary>
    /// 展示激励广告
    /// </summary>
    public void ShowRewardedVideo()
    {
        Advertisement.Show(rewardedPlacementID);
    }

    public void OnUnityAdsReady(string placementId)
    {
        rewardedBtn.interactable = Advertisement.IsReady(rewardedPlacementID);
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
