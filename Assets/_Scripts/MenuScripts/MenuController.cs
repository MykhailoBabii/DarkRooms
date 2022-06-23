using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MenuController : MonoBehaviour
{
    public static Action startGame;

    public static Action changeSfxVolume;
    public static Action changeMusicVolume;

    bool isPlay = false;


    [Header("Start menu")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _optionsButton;

    //[SerializeField] private GameObject _loadingWindow;

    [SerializeField] private GameObject _startWindow;
    [SerializeField] private GameObject _optionWindow;

    //[SerializeField] private GameObject _soundsSettings;

    [Header("Options window")]
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _openMenuButton;
    [SerializeField] private Button _exitGame;
    private string messageValue;

    [Header("Graphic")]
    [SerializeField] private Dropdown _resolutionDropdown;
    [SerializeField] private Dropdown _qualityDropdown;

    [Header("Audio")]
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _soundVolumeSlider;

    [Header("Preferences")]
    [SerializeField] private Toggle _turnamentListSwitchToggle;
    [SerializeField] private Toggle _friendsSeeSwitchToggle;

    [Header("Links")]
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _videoButton;


    

    private void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
            Destroy(gameObject);
        else DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _startButton.onClick.AddListener(StartGameButton);
        _optionsButton.onClick.AddListener(OptionButton);
        _closeButton.onClick.AddListener(CloseOptionsButton);
        _openMenuButton.onClick.AddListener(OpenMenuButton);
        _exitGame.onClick.AddListener(ExitGameButton);

        _resolutionDropdown.onValueChanged.AddListener(ResolutionDropDownList);
        _qualityDropdown.onValueChanged.AddListener(QualityDropDownList);

        _musicVolumeSlider.onValueChanged.AddListener(MusicVolumeSlider);
        _soundVolumeSlider.onValueChanged.AddListener(SoundVolumeSlider);

        _turnamentListSwitchToggle.onValueChanged.AddListener(TurnamentToggle);
        _friendsSeeSwitchToggle.onValueChanged.AddListener(FriendsToggle);

        _creditsButton.onClick.AddListener(CreditsButton);
        _videoButton.onClick.AddListener(VideoButton);
    }

    
    void Update()
    {
        //_musicVolumeSlider.value = _soundsSettings.GetComponent<sounds>()._misicSource.volume * 100;
        //_soundVolumeSlider.value = _soundsSettings.GetComponent<sounds>()._sfxSource.volume * 100;


        if (isPlay == true)
            Show(_exitGame.gameObject);
    }

    private void RestartGame()
    {
        Hide(_startWindow);
        Hide(_optionWindow);
    }


    private void Hide(GameObject window)
    {
        window.SetActive(false);
    }


    private void Show(GameObject window)
    {
        window.SetActive(true);
    }


    private void StartGameButton()
    {
        Time.timeScale = 1;
        startGame?.Invoke();
        Hide(_startWindow);
    }


    private void OptionButton()
    {
        
        Show(_optionWindow);
    }


    private void CloseOptionsButton()
    {
        Hide(_optionWindow);
        Time.timeScale = 1;
    }


    private void OpenMenuButton()
    {
        Show(_optionWindow);
        Time.timeScale = 0;
    }


    private void ExitGameButton()
    {
        isPlay = false;
        Show(_startWindow);
        Hide(_exitGame.gameObject);
        Hide(_optionWindow);
        
        SceneManager.LoadScene(0);
    }


    private void ResolutionDropDownList(int resolutionIndex)
    {
        messageValue = _resolutionDropdown.options[resolutionIndex].text;

        Debug.Log($"Роздільна здатність: {messageValue}");
    }


    private void QualityDropDownList(int qualityIndex)
    {
        messageValue = _qualityDropdown.options[qualityIndex].text;

        Debug.Log($"Якість зображення: {messageValue}");
    }


    private void MusicVolumeSlider(float musicLevel)
    {
        changeMusicVolume?.Invoke();
        sounds._musicVolume = musicLevel / _musicVolumeSlider.maxValue;
        Debug.Log($"Гучність музики: {sounds._musicVolume}");
    }

    private void SoundVolumeSlider(float soundLevel)
    {
        changeSfxVolume?.Invoke();
        sounds._sfxVolume = soundLevel / _soundVolumeSlider.maxValue;
        Debug.Log($"Гучність звуків: {sounds._sfxVolume}");
    }


    private void TurnamentToggle(bool switchState)
    {
        Debug.Log($"Вкл. турнірну таблицю: {switchState}");
    }


    private void FriendsToggle(bool switchState)
    {
        Debug.Log($"Вкл. видимість для друзів: {switchState}");
    }


    private void CreditsButton()
    {
        Debug.Log($"Вікно з текстом");
    }


    private void VideoButton()
    {
        Debug.Log($"Вікно з відео");
    }
}
