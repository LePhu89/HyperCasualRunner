using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class ShopManager : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private Button purchaseButton;
    [SerializeField] private SkinButton[] skinbuttons;

    [Header("Skin")]
    [SerializeField] private Sprite[] skins;

    [Header("Pricing")]
    [SerializeField] private int skinPrice;
    [SerializeField] private TMP_Text priceText;

    [Header("Events")]
    [SerializeField] public static Action<int> onSkinSelected;

    private void Awake()
    {
        UnlockSkin(0);
        priceText.text = skinPrice.ToString();       
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {
        RewardedAdsButton.onRewaredAdsRewarded += RewardPlayer;
        ConfiguireButtons();
        UpdatePurchaseButton();
        yield return null;
        SelectSkin(GetLastSelectSkin());
    }
    private void OnDestroy()
    {
        RewardedAdsButton.onRewaredAdsRewarded -= RewardPlayer;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UnlockSkin(Random.Range(0, skinbuttons.Length));
        }
        if (Input.GetKeyDown(KeyCode.D))
            PlayerPrefs.DeleteAll();
    }
    private void RewardPlayer()
    {
        DataManager.instance.AddCoins(200);
        UpdatePurchaseButton();
    }

    private void ConfiguireButtons()
    {
        for (int i = 0; i < skinbuttons.Length; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("skinButton" + i) == 1;
            skinbuttons[i].Configure(skins[i], unlocked);
            int skinIndex = i;
            skinbuttons[i].GetButton().onClick.AddListener(() => SelectSkin(skinIndex));
        }
    }
    public void UnlockSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("skinButton" + skinIndex, 1);
        skinbuttons[skinIndex].Unlocked();
    }
    public void UnlockSkin(SkinButton skinButton)
    {
        int skinIndex = skinButton.transform.GetSiblingIndex();
        UnlockSkin(skinIndex);
    }
    private void SelectSkin(int skinIndex)
    {
        for(int i = 0; i < skinbuttons.Length;  i++)
        {
            if (skinIndex == i)
                skinbuttons[i].Select();
            else
                skinbuttons[i].DeSelect();
        }

        onSkinSelected?.Invoke(skinIndex);

        SaveLastSelectSkin(skinIndex);
    }
    public void PurcharseSkin()
    {
        List<SkinButton> skinButtonsList = new List<SkinButton>();
        for (int i = 0; i < skinbuttons.Length; i++)
        {
            if (!skinbuttons[i].IsUnlock())
                skinButtonsList.Add(skinbuttons[i]);
        }
        if (skinButtonsList.Count <= 0) return;
        SkinButton randomSkinButton = skinButtonsList[Random.Range(0, skinButtonsList.Count)];
        UnlockSkin(randomSkinButton);
        SelectSkin(randomSkinButton.transform.GetSiblingIndex());

        DataManager.instance.UseCoin(skinPrice);

        UpdatePurchaseButton();
    }
    public void UpdatePurchaseButton()
    {
        if(DataManager.instance.GetCoin() < skinPrice)
            purchaseButton.interactable = false;
        else purchaseButton.interactable = true;
    }
    private int GetLastSelectSkin()
    {
        return PlayerPrefs.GetInt("lastSelectSkin", 0);
    }
    private void SaveLastSelectSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("lastSelectSkin", skinIndex);
    }
}
