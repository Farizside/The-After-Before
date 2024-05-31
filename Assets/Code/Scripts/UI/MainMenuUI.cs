using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private InputManager _input;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GamePlay()
    {
        // _input.SetGameplay();
        SceneManager.LoadScene(3);
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
