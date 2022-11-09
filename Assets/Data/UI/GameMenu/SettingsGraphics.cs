using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CDG.UI.Settings
{
    public class SettingsGraphics : MonoBehaviour
    {
        [SerializeField] private Dropdown resolutionDropdown;
        [SerializeField] private Dropdown qualityDropdown;

        Resolution[] resolutions;

        private void Start()
        {
            resolutionDropdown.ClearOptions();
            List<string> options = new List<string>();
            resolutions = Screen.resolutions;
            int currentResoulutionIndex = 0;

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
                options.Add(option);
                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                    currentResoulutionIndex = i;
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.RefreshShownValue();
            LoadSettings(currentResoulutionIndex);

        }

        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        public void SaveSettings()
        {
            PlayerPrefs.SetInt("QualitySettingPreference", qualityDropdown.value);
            PlayerPrefs.SetInt("ResolutionPreference", resolutionDropdown.value);
            PlayerPrefs.SetInt("FullscreenPreferernce", System.Convert.ToInt32(Screen.fullScreen));
        }

        public void LoadSettings(int currentResoulutionIndex)
        {
            if (PlayerPrefs.HasKey("QualitySettingPreference"))
                qualityDropdown.value = PlayerPrefs.GetInt("QualitySettingPreference");
            else
                qualityDropdown.value = 3;

            if (PlayerPrefs.HasKey("ResolutionPreference"))
                resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionPreference");
            else
                resolutionDropdown.value = currentResoulutionIndex;

            if (PlayerPrefs.HasKey("FullscreenPreferernce"))
                Screen.fullScreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreferernce"));
            else
                Screen.fullScreen = true;
        }
    }
}

