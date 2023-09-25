using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private SoundManager soundsManager;
    [SerializeField] private VibrationManager vibrationManager;
    [SerializeField] private Sprite optionsOnSprite;
    [SerializeField] private Sprite optionsOffSprite;
    [SerializeField] private Image soundsButtonImage;
    [SerializeField] private Image hapticsButtonImage;

    [Header(" Settings ")]
    private bool soundsState = true;
    private bool hapticsState = true;

    private void Awake()
    {
        soundsState = PlayerPrefs.GetInt("sounds", 1) == 1;
        hapticsState = PlayerPrefs.GetInt("haptics", 1) == 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Setup()
    {
        if (soundsState)
            EnableSounds();
        else
            DisableSounds();


        if (hapticsState)
            EnableHaptics();
        else
            DisableHaptics();
    }

    public void ChangeSoundsState()
    {
        if (soundsState)
            DisableSounds();
        else
            EnableSounds();

        soundsState = !soundsState;

        // Save the value of the sounds State
  

        PlayerPrefs.SetInt("sounds", soundsState ? 1 : 0);
    }

    private void DisableSounds()
    {
        // Tell the sounds manager to set the volume of Alllllll the sounds to 0
        soundsManager.DisableSounds();

        // Change the image of the sounds button
        soundsButtonImage.sprite = optionsOffSprite;
    }

    private void EnableSounds()
    {
        soundsManager.EnableSounds();

        // Change the image of the sounds button
        soundsButtonImage.sprite = optionsOnSprite;
    }

    public void ChangeHapticsState()
    {
        if (hapticsState)
            DisableHaptics();
        else
            EnableHaptics();

        hapticsState = !hapticsState;

        PlayerPrefs.SetInt("haptics", hapticsState ? 1 : 0);
    }

    private void DisableHaptics()
    {
        vibrationManager.DisableVibrations();

        hapticsButtonImage.sprite = optionsOffSprite;
    }

    private void EnableHaptics()
    {
        vibrationManager.EnableVibrations();

        hapticsButtonImage.sprite = optionsOnSprite;
    }
}
