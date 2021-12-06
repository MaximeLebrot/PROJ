using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SettingsMenu : MonoBehaviour
{
    [Tooltip("DONT TOUCH")]
    [SerializeField] public  SettingsData userSettings;


    #region Compnent References
    //Audio
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TextMeshProUGUI musicSliderValueText;
    
    [SerializeField] private Slider voiceSlider;
    [SerializeField] private TextMeshProUGUI voiceSliderText;
    
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TextMeshProUGUI sfxSliderText;
    [SerializeField] private Toggle mute;

    //Ease of Use
    [SerializeField] private Toggle blindMode;
    [SerializeField] private Toggle highContrastMode;

    //Display
    [SerializeField] private Slider fieldOfView;
    [SerializeField] private TextMeshProUGUI fieldOfViewSliderText;
    
    [SerializeField] private Slider brightness;
    [SerializeField] private TextMeshProUGUI brightnessSliderText
        ;
    [SerializeField] private TMP_Dropdown quality;
    //[SerializeField]private TMP_Dropdown resolution;
    [SerializeField] private Toggle fullscreen;
    #endregion

    private void Awake()
    {
        
        musicSlider.onValueChanged.AddListener(newValue => {
            musicSlider.value = newValue;
            musicSliderValueText.text = ((int)(newValue)).ToString();
        });

        voiceSlider.onValueChanged.AddListener(newValue => {
            voiceSlider.value = newValue;
            voiceSliderText.text = ((int)(newValue)).ToString();
        });
        
        sfxSlider.onValueChanged.AddListener(newValue => {
            sfxSlider.value = newValue;
            sfxSliderText.text = ((int)(newValue)).ToString();
        });
        
        fieldOfView.onValueChanged.AddListener(newValue => {
            fieldOfView.value = newValue;
            fieldOfViewSliderText.text = ((int)(newValue)).ToString();
        });
        
        brightness.onValueChanged.AddListener(newValue => {
            brightness.value = newValue;
            brightnessSliderText.text = ((int)(newValue)).ToString();
        });       
    }
    
    
    
   

}

