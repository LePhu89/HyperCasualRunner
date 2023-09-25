using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [Header(" Coin Texts ")]
    [SerializeField] private TMP_Text[] coinsTexts;
    private int coins;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        coins = PlayerPrefs.GetInt("coins", 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        AddCoins(5);
        UpdateCoinsTexts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void UpdateCoinsTexts()
    {
        foreach(TMP_Text coinText in coinsTexts)
        {
            coinText.text = coins.ToString();
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;

        UpdateCoinsTexts();

        PlayerPrefs.SetInt("coins", coins);
    }
    public void UseCoin(int amount)
    {
        coins -= amount;
        UpdateCoinsTexts();
        PlayerPrefs.SetInt("coins", coins);
    }
    public int GetCoin()
    {
        return coins;
    }
}
