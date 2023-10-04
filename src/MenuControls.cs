using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuControls : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField] private Text VolumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private float defaultVolume = 1.0f;

    [Header("Gameplay Settings")]
    [SerializeField] private Text controllerSenTextValue = null;
    [SerializeField] private Slider controllerSenSlider = null;
    [SerializeField] private int defaultSen = 4;
    public int mainControllerSen = 4;

    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private Toggle fullScreenToggle;

    [Header("Toggle Settings")]
    [SerializeField] private Toggle invertYToggle = null;

    [Header("Graphics Settings")]
    [SerializeField] private Slider brightnessSlider = null;
    [SerializeField] private Text brightnessTextValue = null;
    [SerializeField] private float defaultBrightness = 1;

    private int _qualityLevel;
    private bool _isFullScreen;
    private float _brightnessLevel;



    [Header("Confirmation")]
    [SerializeField] private GameObject comfirmationPrompt = null;




    [Header("Levels To Load")]
    public string _newGameLevel;
    private string levelToLoad;

    private string tutorial;
    [SerializeField] private GameObject noSavedFileDialog = null;
    



    [Header("Resolution Dropdowns")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
        

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
            currentResolutionIndex = i;
            }

        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }





    public void NewFileDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);
    }

    public void LoadTutorialDialogYes(){
        SceneManager.LoadScene("newTutorial");
    }

    public void LoadGameDialogYes()
    {
        if(PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }

        else{
            noSavedFileDialog.SetActive(true);

        }
    }

    public void ExitButton() {
        Application.Quit();
    }


    public void SetVolume(float volume){
        AudioListener.volume = volume;
        //VolumeTextValue.text = volume.ToString("0.0");  
    }


    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }

    public void SetControllerSen(float sensitivity){
        mainControllerSen = Mathf.RoundToInt(sensitivity);
        controllerSenTextValue.text = sensitivity.ToString("0");
    }

    public void GameplayApply()
    {
        if (invertYToggle.isOn){
            PlayerPrefs.SetInt("masterInvertY", 1);
            //invert Y
        }
        else
        {
            PlayerPrefs.SetInt("masterInvertY", 0);
            //not invert
        }

        PlayerPrefs.SetFloat("masterSen", mainControllerSen);
        StartCoroutine(ConfirmationBox());
    }



    //public void SetBrightness(float brightness)
    //{
        //_brightnessLevel = brightness;
        //brightnessTextValue.text = brightness.ToString("0.0");
    
    //}

    public void SetFullScreen(bool isFullscreen)
    {
        _isFullScreen = isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
      _qualityLevel = qualityIndex;  
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", _brightnessLevel);
        //code to change brightness

        PlayerPrefs.SetInt("masterQuality", _qualityLevel);
        QualitySettings.SetQualityLevel(_qualityLevel);

        PlayerPrefs.SetInt("masterFullscreen", (_isFullScreen ? 1 : 0));
        Screen.fullScreen = _isFullScreen;

        StartCoroutine(ConfirmationBox());
    }

    



    public void ResetButton(string MenuType)
    {

        if (MenuType == "Graphics")
        {
            //reset brightness
            //brightnessSlider.value = defaultBrightness;
            //brightnessTextValue.text = defaultBrightness.ToString("0.0");

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            fullScreenToggle.isOn = false;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);

            resolutionDropdown.value = resolutions.Length;
            GraphicsApply();
        }



        if (MenuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            VolumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }

        if (MenuType == "Gameplay")
        {
            controllerSenTextValue.text = defaultSen.ToString("0");
            controllerSenSlider.value = defaultSen;
            mainControllerSen = defaultSen;
            invertYToggle.isOn = false;
            GameplayApply();
        }
    }

    public IEnumerator ConfirmationBox()
    {
        comfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        comfirmationPrompt.SetActive(false);
    }


}
