using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private InputManager _input;
    [SerializeField] private SceneLoader _sceneLoader;
    public Button BtnGamePlay;
    public Button BtnAbout;
    public Button BtnExit;
    public GameObject CanvasMenu;
    public GameObject CanvasAbout;

    public BackToMenu setting;
    // Start is called before the first frame update
    void Start()
    {
        _input.BackEvent += setting.Back;
        BtnGamePlay.onClick.AddListener(GamePlay);
        BtnAbout.onClick.AddListener(About);
        BtnExit.onClick.AddListener(Exit);
        AudioManager.Instance.PlayMusic("MainMenu"); //BGM

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GamePlay()
    {
        _sceneLoader.LoadScene(1);
        AudioManager.Instance.PlayMusic("GamePlay", 1f);//BGM
    }
    
    

    private void About()
    {
        CanvasMenu.SetActive(false);
        CanvasAbout.SetActive(true);
    }

    private void Exit()
    {
        Application.Quit();
    }
}
