using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Localization.Settings;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown graphicsDropdown;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown languageDropdown;

    Resolution[] resolutions;


    // pobranie rozdzielczosci komputera i dodanie ich do opcji dropdownu
    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> resOptions = new List<string>();
        List<Resolution> uniqueResolutions = new List<Resolution>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            bool alreadyExists = false;

            foreach (Resolution res in uniqueResolutions)
            {
                if (res.width == resolutions[i].width &&
                    res.height == resolutions[i].height)
                {
                    alreadyExists = true;
                    break;
                }
            }

            if (!alreadyExists)
            {
                uniqueResolutions.Add(resolutions[i]);

                string option = resolutions[i].width + "x" + resolutions[i].height;
                resOptions.Add(option);

                if (resolutions[i].width == Screen.width &&
                    resolutions[i].height == Screen.height)
                {
                    currentResolutionIndex = uniqueResolutions.Count - 1;
                }
            }
        }

        resolutions = uniqueResolutions.ToArray();

        resolutionDropdown.AddOptions(resOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        
        // Pobranie języka systemowego komputera
        int defaultLanguage = Application.systemLanguage == SystemLanguage.Polish ? 0 : 1;

        // Ustawienie języka w dropdownie i lokalizacji gry
        languageDropdown.value = defaultLanguage;
        UpdateDropdownLanguage(defaultLanguage);
        StartCoroutine(ChangeLanguage(defaultLanguage));
        // mozliwe ze trzeba bedzie to zmieniac zas XD

    }

    // ustawienie jakosci grafiki
    public void SetGraphics()
    {
        QualitySettings.SetQualityLevel(graphicsDropdown.value);
    }

    // ustawienie glosnosci: muzyki, sfx, master(ogólne)
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
    }

    // Ustawienie pelnego ekranu
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        // Debug.Log("fsc " + isFullscreen);
    }
    // Ustawienie rozdzielczosci
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    // ustawienie jezykow

    public void SetLanguage(int languageIndex)
    {
        StartCoroutine(ChangeLanguage(languageIndex));
    }

    private IEnumerator ChangeLanguage(int languageIndex)
    {
        yield return LocalizationSettings.InitializationOperation;

        switch (languageIndex)
        {
            case 0: // polski
                LocalizationSettings.SelectedLocale =
                    LocalizationSettings.AvailableLocales.GetLocale("pl");
                Debug.Log("polski");

                UpdateDropdownLanguage(0);

                break;

            case 1: // angielski
                LocalizationSettings.SelectedLocale =
                    LocalizationSettings.AvailableLocales.GetLocale("en");
                Debug.Log("angielski");

                UpdateDropdownLanguage(1);
                break;

            default:
                break;
        }
    }

    // aktualizacja jezyku w dropdownach
    public void UpdateDropdownLanguage(int languageIndex)
    {
        if (languageIndex == 0)
        {
            graphicsDropdown.options[0].text = "Ultra";
            graphicsDropdown.options[1].text = "Bardzo wysoka";
            graphicsDropdown.options[2].text = "Wysoka";
            graphicsDropdown.options[3].text = "Średnia";
            graphicsDropdown.options[4].text = "Niska";
            graphicsDropdown.options[5].text = "Ziemniak";

            languageDropdown.options[0].text = "polski";
            languageDropdown.options[1].text = "angielski";
        }
        else if (languageIndex == 1)
        {
            graphicsDropdown.options[0].text = "Ultra";
            graphicsDropdown.options[1].text = "Very High";
            graphicsDropdown.options[2].text = "High";
            graphicsDropdown.options[3].text = "Medium";
            graphicsDropdown.options[4].text = "Low";
            graphicsDropdown.options[5].text = "Very Low";

            languageDropdown.options[0].text = "Polish";
            languageDropdown.options[1].text = "English";
        }

        graphicsDropdown.RefreshShownValue();
        languageDropdown.RefreshShownValue();
    }

}
