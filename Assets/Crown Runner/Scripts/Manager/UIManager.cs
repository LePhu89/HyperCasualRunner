using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] private ShopManager shopManager;
    [Header("Element")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Text levelText;
    // Start is called before the first frame update
    void Start()
    {
        progressBar.value = 0;
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        settingsPanel.SetActive(false);
        HideShop();
        levelText.text = "Level " +  (ChunkManager.instance.GetLevel() + 1);

        GameManager.onGameStateChanged += GameStateChangedCallback;
    }
    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateProgressBar();
    }
    private void GameStateChangedCallback(GameManager.GameState gameState)
    {
        if(gameState == GameManager.GameState.GameOver)        
            ShowGameOver();        
        else if(gameState == GameManager.GameState.LevelComplete)        
            ShowLevelComplete();       
    }
    public void PlayButtonPressed()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Game);   
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }
    public void RetryButtonPressed()
    {
        InterstitialAd.Instance.ShowAd();
        SceneManager.LoadScene(0);       
    }
    public void ShowGameOver()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }
    private void ShowLevelComplete()
    {
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(true);
    }
    public void UpdateProgressBar()
    {
        if (!GameManager.Instance.IsGameState())
            return;
        float progress = PlayerController.instance.transform.position.z / ChunkManager.instance.GetFinishZ();
        progressBar.value = progress;   
    }
    public void ShowSettingPanel()
    {
        settingsPanel.SetActive(true);
    }
    public void HideSettingPanel()
    {
        settingsPanel.SetActive(false);
    }
    public void ShowShop()
    {
        shopPanel.SetActive(true);
        shopManager.UpdatePurchaseButton();
    }
    public void HideShop()
    {
        shopPanel.SetActive(false);
    }
}
