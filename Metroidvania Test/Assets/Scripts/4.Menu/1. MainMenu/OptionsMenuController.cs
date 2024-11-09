using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuController : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField] Slider _volumeSlider;
    [SerializeField] Toggle _muteSound;
    float _volumeValue;

    [Header("Fullscreen Settings")]
    [SerializeField] Toggle _fullscreenToggle;

    [SerializeField] GameObject _optionsCanvas;
    [SerializeField] GameObject _previousMenu;

    private void Start()
    {
        #region Volume Controller
        _volumeSlider.value = PlayerPrefs.GetFloat("audioVolume", 0.5f);
        AudioListener.volume = _volumeSlider.value;
        _volumeSlider.onValueChanged.AddListener(delegate { SetVolume(_volumeSlider.value); });
        #endregion

        #region Volume Mute
        _muteSound.isOn = AudioListener.volume == 0;
        _muteSound.onValueChanged.AddListener(delegate { ToggleSound(_muteSound.isOn); });
        #endregion

        #region Fullscreen Controller
        if (Screen.fullScreen)
        {
            _fullscreenToggle.isOn = true;
        }
        else
        {
            _fullscreenToggle.isOn = false;
        }
        #endregion

    }

    #region Volume Controller
    public void SetVolume (float volume)
    {
        AudioListener.volume = volume;
    }
    #endregion

    #region Volume Mute
    
    public void ToggleSound(bool isMuted)
    {
        AudioListener.volume = isMuted ? 0 : 1;
    }

    #endregion

    #region Fullscreen Controller
    public void ActivateFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    #endregion

    public void BackButton()
    {
        _optionsCanvas.SetActive(false);
        _previousMenu.SetActive(true);
    }

}
