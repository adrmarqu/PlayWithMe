using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OptionsController : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    [SerializeField]
    Toggle toggle;
    [SerializeField]
    TMP_Dropdown resolutionsDropDown;

    float sliderValue;

    Resolution[] resolutions;

    void Start()
    {
        sliderValue = 0;
        slider.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = slider.value;

        if(Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }

        CheckResolutions();
    }

    public void ActivateFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void CheckResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionsDropDown.ClearOptions();
        List<string> options = new List<string>();
        int actualResolution = 0;
        
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                actualResolution = i;
            }
        }

        resolutionsDropDown.AddOptions(options);
        resolutionsDropDown.value = actualResolution;
        resolutionsDropDown.RefreshShownValue();

        resolutionsDropDown.value = PlayerPrefs.GetInt("numeroResolucion", 0);
    }

    public void ChangeResolution(int indexResolution)
    {
        PlayerPrefs.SetInt("numeroResolucion", resolutionsDropDown.value);
        Resolution resolution = resolutions[indexResolution];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void ChangeSlider(float value)
    {
        sliderValue = value;
        PlayerPrefs.SetFloat("volumeAudio", sliderValue);
        AudioListener.volume = slider.value;
    }
}
