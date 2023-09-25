using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    [Header("Elements")]
    [SerializeField] private InterstitialAd interstitialAd;
    [SerializeField] private RewardedAdsButton rewardedAdsButton;

    [Header("Setting")]
    private bool isLoaded;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_IOS
            _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }


    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        interstitialAd.LoadAd();
        rewardedAdsButton.LoadAd();

        isLoaded = true;
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        InitializeAds();
    }
    public bool IsLoaded()
    {
        return isLoaded;
    }
}
