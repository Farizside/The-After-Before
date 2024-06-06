using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BackToMenu : MonoBehaviour
{
    [SerializeField] private InputManager _input;
    [SerializeField] [CanBeNull] private GameObject _currCanvas;
    [SerializeField] [CanBeNull] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _loadingScreen;
    
    public Button BtnMenu;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (!_currCanvas)
        {
            BtnMenu.onClick.AddListener(Menu);
        }
        else
        {
            BtnMenu.onClick.AddListener(Back);
            _input.BackEvent += Back;
        }
    }
    private void Menu()
    {
        _loadingScreen = Instantiate(_loadingScreen);
        WaveManager.Instance.CurWaveTime = 100;
        _loadingScreen = Instantiate(_loadingScreen);
        GameManager.Instance.HandleResume();
        _loadingScreen.GetComponent<SceneLoader>().LoadScene(0);
    }

    public void Back()
    {
        if (_currCanvas != null) _currCanvas.SetActive(false);
        if (_mainMenuCanvas != null) _mainMenuCanvas.SetActive(true);
    }
}
